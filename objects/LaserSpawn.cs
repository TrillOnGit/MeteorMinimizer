using Godot;
using System;

public partial class LaserSpawn : Node2D
{
	[Export] public int BulletSpeed = 150;

	[Export] public int BulletDamage = 1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Area2D>("Laser").AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is not asteroid asteroid)
		{
			return;
		}

		asteroid.Velocity.Y -= (4 - asteroid.Divisions);
		asteroid.ProcessDamage(BulletDamage);
		QueueFree();
		Console.WriteLine("Asteroid hit by bullet");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var newPosition = Position;
		newPosition.Y -= BulletSpeed * (float)delta;
		Position = newPosition;

		if (!(newPosition.Y < 0)) return;
		Console.WriteLine("Bullet Deleted");
		QueueFree();
	}
}
