public partial class Player : Area2D
{

	[Export]
	public int Speed { get; set; } = 400;

	[Signal]
	public delegate void HitEventHandler();

	public Vector2 ScreenSize;

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();
	}

	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // Player's movement
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (Input.IsActionPressed("move_right"))
			velocity.X += Speed;

		if (Input.IsActionPressed("move_left"))
			velocity.X -= Speed;

		if (Input.IsActionPressed("move_down"))
			velocity.Y += Speed;

		if (Input.IsActionPressed("move_up"))
			velocity.Y -= Speed;

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		if (velocity.X != 0)
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0)
		{
			animatedSprite2D.Animation = "up";
			animatedSprite2D.FlipV = velocity.Y > 0;
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);

	}

	private void OnBodyEntered(PhysicsBody2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
