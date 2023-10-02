using Godot;
using System;

public partial class Clock : Label
{
	public double time = 0;
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
		time += delta;
		Text = (Math.Round(time, 2)).ToString("F1");
	}
}
