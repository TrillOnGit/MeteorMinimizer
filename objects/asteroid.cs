using Godot;
using System;

public partial class asteroid : Area2D
{
	public int Divisions = 0;
	[Export] public int Hp = 2;
	[Export] public int MaxVelocityX = 20;
	[Export] public int MaxVelocityY = 8;
	public Vector2 Velocity = new Vector2(0, 0);
	[Export] public int Acceleration = 5;
	[Export] public int ScoreValue = 100;
	
	private Autoload _autoLoad;
	
	public override void _Ready()
	{
		_autoLoad = GetNode<Autoload>("/root/Autoload");
		
		AreaEntered += OnAreaEntered;
	}
	
	private void OnAreaEntered(Area2D area)
	{
		
		if (area is AsteroidBarrier asteroidBarrier)
		{
			OnBarrierEntered(asteroidBarrier);
		}
		else if (area is asteroid asteroid)
		{
			OnAsteroidEntered(asteroid);
		}
	}

	private void OnAsteroidEntered(asteroid asteroid)
	{
		Hp = 0;
	}

	private void OnBarrierEntered(AsteroidBarrier asteroidBarrier)
	{
		switch (asteroidBarrier.Direct)
		{
			case AsteroidBarrier.BounceDirection.LeftAndRight:
				Velocity.X *= -1;
				break;
			case AsteroidBarrier.BounceDirection.UpAndDown:
				Velocity.Y *= -1;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		Console.WriteLine("Asteroid bounced");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (Hp <= 0)
		{
			if (Position.Y > 0)
			{
				_autoLoad.EmitAsteroidDestroyed(this);
				_autoLoad.Score += ScoreValue;
			}
			Console.WriteLine(_autoLoad.Score);
			if (Divisions <= 1)
			{
				Split();
			}
			else
			{
				QueueFree();
			}
		}

		if (Math.Abs(Velocity.X) > MaxVelocityX)
		{
			if (Velocity.X > 0)
			{
				Velocity.X -= 1;
			}
			else
			{
				Velocity.X += 1;
			}
		}

		if (Velocity.Y < MaxVelocityY)
		{
			Velocity.Y += (float)delta * Acceleration;
		}
		
		var newPosition = Position;
		
		newPosition.X += Velocity.X * (float)delta;
		newPosition.Y += Velocity.Y * (float)delta;
		
		Position = newPosition;

		if (newPosition.X is < 0 or > 160)
		{
			Console.WriteLine("Deleting Out of Bounds Object");
			QueueFree();
		}

		if (!(newPosition.Y > 150)) return;
		
		_autoLoad.EmitAsteroidMissed();
		
		if (Divisions > 0)
		{
			_autoLoad.Score -= 100 / Divisions;
		}
		else
		{
			_autoLoad.Score -= 200;
		}
		QueueFree();
	}

	public void ProcessDamage(int damage)
	{
		Hp -= damage;
	}

	private void Split()
	{
		for(var i = -1; i <= 1; i += 2)
		{
			var newPosition = GlobalPosition + new Vector2((_autoLoad.Rand.RandiRange(-7, -12) * i), _autoLoad.Rand.RandiRange(-5, -15));
			var newVelocity = Velocity + new Vector2(_autoLoad.Rand.RandiRange(-10, -40) * i, _autoLoad.Rand.RandiRange(5, -25));
			
			_autoLoad.SpawnMeteor(Divisions + 1, newPosition, newVelocity);
		}
		QueueFree();
	}
}
