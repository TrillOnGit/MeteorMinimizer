using Godot;
using System;

public partial class VictoryText : Label
{
	private Autoload _autoload;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoload = GetNode<Autoload>("/root/Autoload");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = $"You Win! Your Time Was: {_autoload.BestTime:F2} seconds";
	}
}
