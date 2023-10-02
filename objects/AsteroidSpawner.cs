using Godot;
using System;

public partial class AsteroidSpawner : Area2D
{
	[Export] private int _secPerSpawn = 6;

	[Export] private int _spawnProgRate = 1;

	private Autoload _autoload;
	private PackedScene _asteroidLoad = GD.Load<PackedScene>("res://objects/asteroid.tscn");

	public Timer SpawnCooldown;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoload = GetNode<Autoload>("/root/Autoload");
		
		SpawnCooldown = GetNode<Timer>("SpawnTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (SpawnCooldown.TimeLeft != 0) return;
		var div = 1;
		var randSpawn = _autoload.Rand.RandiRange(0, 10);
		div = randSpawn switch
		{
			0 => 2,
			>= 5 => 0,
			_ => div
		};
		Vector2 pos = new Vector2(_autoload.Rand.RandiRange(20, 140), _autoload.Rand.RandiRange(-16, -125));
		Vector2 vel = new Vector2(_autoload.Rand.RandiRange(-10, 10), _autoload.Rand.RandiRange(0, 10));
		
		if (_autoload.Score <= 0)
		{
			pos.Y = _autoload.Rand.RandiRange(-16, -30);
		}
		
		_autoload.SpawnMeteor(div, pos, vel);
		SpawnCooldown.Start(_secPerSpawn - div);
	}
}
