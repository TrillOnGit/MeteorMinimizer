using Godot;
using System;

public partial class node_2d : Node2D
{
	private Autoload _autoload;
	private double _savedTime;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoload = GetNode<Autoload>("/root/Autoload");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_autoload.Score >= 1000)
		{
			foreach (var i in GetChildren(true))
			{
				i.QueueFree();
			}
			
			GetTree().ChangeSceneToFile("res://EndScreen.tscn");
			_savedTime = GetNode<Clock>("UiControl/Clock").time;
			_autoload.BestTime = _savedTime;
			_autoload.Score = 0;
			
			_autoload.Save(GetNode<Clock>("UiControl/Clock").time);
			GetNode<Clock>("UiControl/Clock").time = 0;
			
			
		}
	}
}
