using Godot;
using System;

public partial class Menu : Control
{
	//private AudioStreamPlayer2D _selectAudio;
	
	public override void _Ready()
	{
		//_selectAudio = GetNode<AudioStreamPlayer2D>("VBoxContainer/SelectSound");
	}
	
	public override void _Process(double delta)
	{
		
	}
	private void _OnStartPressed()
	{
		//_selectAudio.Play();
		GetTree().ChangeSceneToFile("res://node_2d.tscn");
	}
	
	private void _OnScoreboardPressed()
	{
		//_selectAudio.Play();
		// Replace with function body.
	}
	
	private void _OnExitPressed()
	{
		GetTree().Quit();
	}
}


