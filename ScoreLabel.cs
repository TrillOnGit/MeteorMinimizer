using Godot;
using System;

public partial class ScoreLabel : Label
{
	private Autoload _autoLoad;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoLoad = GetNode<Autoload>("/root/Autoload");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var score = _autoLoad.Score;
		Text = $"{score}";
	}
}
