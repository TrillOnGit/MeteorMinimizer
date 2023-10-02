using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed = 100;

	[Export] 
	public int BulletPositionY = 7;
	
	private Vector2 _screenSize;
	private Timer _shotCooldown;
	private PackedScene _laserScene = GD.Load<PackedScene>("res://objects/laser_weapon.tscn");
	private Sprite2D _sprite;
	private AudioStreamPlayer2D _laserSfx;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
		_shotCooldown = GetNode<Timer>("ShotTimer");
		_sprite = GetNode<Sprite2D>("PlayerSprite");
		_laserSfx = GetNode<AudioStreamPlayer2D>("LaserSoundFx");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = new Vector2(0, 0);
		
		if (Input.IsActionPressed("Right"))
		{
			_sprite.Frame = 1;
			_sprite.FlipH = false;
			velocity.X += 1;
			Console.WriteLine($"X Position: {Position.X}");
		}

		if (Input.IsActionPressed("Left"))
		{
			_sprite.Frame = 1;
			_sprite.FlipH = true;
			velocity.X -= 1;
			Console.WriteLine($"X Position: {Position.X}");
		}

		if (Input.IsActionPressed("Right") == Input.IsActionPressed("Left"))
		{
			_sprite.Frame = 0;
			_sprite.FlipH = false;
		}

		if (Input.IsActionPressed("Fire1") && _shotCooldown.TimeLeft == 0)
		{
			_laserSfx.Play();
			_shotCooldown.Start(1);
			Shoot();
		}

		if (Input.IsActionPressed("Fire2") && _shotCooldown.TimeLeft == 0)
		{
			Console.WriteLine("Fire 2");
			_shotCooldown.Start(2);
		}
		
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}
		
		var newPosition = Position;
		
		newPosition.X += velocity.X * (float)delta;
		newPosition.X = Math.Clamp(newPosition.X, 8, _screenSize.X - 8);
		
		Position = newPosition;
		
	}

	private void Shoot()
	{
		if (_laserScene.Instantiate() is not Node2D laser) throw new InvalidOperationException("Root node of Laser must inherit Node2D");
		GetNode("/root").AddChild(laser);
		var offset = new Vector2(0, -1 * BulletPositionY);
		laser.GlobalPosition = GlobalPosition + offset;
	}
}
