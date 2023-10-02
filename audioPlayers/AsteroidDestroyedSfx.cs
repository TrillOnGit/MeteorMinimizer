using Godot;
using System;

public partial class AsteroidDestroyedSfx : AudioStreamPlayer
{
	private Autoload _autoload;

	[Export] public int Divisions = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoload = GetNode<Autoload>("/root/Autoload");
		_autoload.AsteroidDestroyed += AutoloadOnAsteroidDestroyed;
	}

	private void AutoloadOnAsteroidDestroyed(asteroid asteroid)
	{
		if (asteroid.Divisions == Divisions)
		{
			Play();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
