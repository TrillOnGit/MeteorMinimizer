using Godot;
using System;
using System.Net;

public partial class Autoload : Node
{
	public RandomNumberGenerator Rand = new();
	public int Score;
	public double BestTime;

	[Signal]
	public delegate void MenuItemFocusedEventHandler();

	[Signal]
	public delegate void MenuItemSelectedEventHandler();

	[Signal]
	public delegate void AsteroidDestroyedEventHandler(asteroid asteroid);

	[Signal]
	public delegate void AsteroidMissedEventHandler();
	
	public override void _Ready()
	{
		Score = 0;
		Rand.Randomize();
	}

	public void SpawnMeteor(int divisions, Vector2 position, Vector2 velocity)
	{
		PackedScene asteroidSpawned = divisions switch
		{
			0 => GD.Load<PackedScene>($"objects/asteroid.tscn"),
			_ => GD.Load<PackedScene>($"objects/Asteroid{divisions}.tscn")
		};

		if (asteroidSpawned == null) throw new ArgumentOutOfRangeException(nameof(divisions), "Asteroid size not found");

		if (asteroidSpawned.Instantiate() is not asteroid ast) throw new Exception("Scene was not of type asteroid");
		
		GetNode("/root/Node2D").AddChild(ast);

		var flipInt = Rand.RandiRange(0, 1);
		
		ast.Velocity = velocity;
		ast.GlobalPosition = position;
		ast.Divisions = divisions;
		if (divisions == 1)
		{
			ast.GetNode<Sprite2D>("Sprite2D").Frame = Rand.RandiRange(0, 1);
		}
		ast.Transform = ast.Transform.ScaledLocal(new Vector2(-1 + 2 * flipInt, 1));
		
		Console.WriteLine("Creating meteor");
	}

	public void PlayMenuSounds()
	{
		EmitSignal(SignalName.MenuItemFocused);
	}

	public void PlayMenuSelectSound()
	{
		EmitSignal(SignalName.MenuItemSelected);
	}

	public void EmitAsteroidDestroyed(asteroid asteroid)
	{
		EmitSignal(SignalName.AsteroidDestroyed, asteroid);
	}

	public void EmitAsteroidMissed()
	{
		EmitSignal(SignalName.AsteroidMissed);
	}

	public void Save(double time)
	{
		var configFile = new ConfigFile();
		Error err = configFile.Load("user://scores.cfg");

		if (err != Error.Ok)
		{
			configFile.SetValue("Score", "BestTime", time);
			configFile.Save("user://scores.cfg");
			Console.WriteLine($"Saved Best Time as {time}");
			return;
		}
		
		configFile.SetValue("Score", "BestTime", time);

		if ((int)configFile.GetValue("Score", "BestTime") > time)
		{
			configFile.SetValue("Score", "BestTime", time);
			configFile.Save("user://scores.cfg");
			Console.WriteLine($"Saved Best time as {time}");
		}
	}
}
