using Godot;
using System;

public partial class AsteroidBarrier : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public enum BounceDirection
	{
		UpAndDown,
		LeftAndRight
	}
	
	[Export]
	public BounceDirection Direct = BounceDirection.LeftAndRight;
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
