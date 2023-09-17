public partial class Mob : RigidBody2D
{
    public override void _Ready()
    {
        var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        var mobTypes = animatedSprite.SpriteFrames.GetAnimationNames();
        animatedSprite.Play(mobTypes[GD.Randi() % mobTypes.Length]);
    }

    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        QueueFree();
    }

}
