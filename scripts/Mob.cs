public partial class Mob : RigidBody2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var animateSprite = GetNode<AnimatedSprite3D>("AnimatedSprite2D");
        var mobTypes = animateSprite.SpriteFrames.GetAnimationNames();
        animateSprite.Play(mobTypes[GD.Randi() % mobTypes.Length]);

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        QueueFree();
    }


}
