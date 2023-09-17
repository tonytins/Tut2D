using Godot;

public partial class Player : Area2D
{

    [Export]
    public int Speed { get; set; } = 400;

    public Vector2 ScreenSize;

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }

    public override void _Process(double delta)
    {
        var velocity = Vector2.Zero; // Player's movement
        var animateSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        if (Input.IsActionPressed("move_right"))
            velocity.X += Speed;

        if (Input.IsActionPressed("move_left"))
            velocity.X -= Speed;

        if (Input.IsActionPressed("move_down"))
            velocity.Y += Speed;

        if (Input.IsActionPressed("move_up"))
            velocity.Y -= Speed;

        if (velocity.X != 0)
        {
            animateSprite.Animation = "walk";
            animateSprite.FlipV = false;
            animateSprite.FlipH = velocity.X < 0;
        }
        else if (velocity.Y != 0)
        {
            animateSprite.Animation = "up";
            animateSprite.FlipV = velocity.Y > 0;
        }

        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );

    }
}
