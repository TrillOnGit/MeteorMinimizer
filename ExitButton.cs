using Godot;
using System;

public partial class ExitButton : Button
{
	private Autoload _autoload;

	public override void _Ready()
	{
		_autoload = GetNode<Autoload>("/root/Autoload");
		
		FocusEntered += OnFocusEntered;
		
	}

	private void OnFocusEntered()
	{
		_autoload.PlayMenuSounds();
	}
	
	public override void _Process(double delta)
	{
	}
}
