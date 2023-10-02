using Godot;
using System;

public partial class FocusSound : AudioStreamPlayer2D
{
	private Autoload _autoload;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoload = GetNode<Autoload>("/root/Autoload");
		_autoload.MenuItemFocused += AutoloadOnMenuItemFocused;
	}

	private void AutoloadOnMenuItemFocused()
	{
		Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
