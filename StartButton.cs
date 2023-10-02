using Godot;
using System;

public partial class StartButton : Button
{
	private Autoload _autoload;

	public override void _Ready()
	{
		_autoload = GetNode<Autoload>("/root/Autoload");
		
		GrabFocus();
		FocusEntered += OnFocusEntered;
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		_autoload.PlayMenuSelectSound();
	}

	private void OnFocusEntered()
	{
		_autoload.PlayMenuSounds();
	}
	
	public override void _Process(double delta)
	{
	}
}
