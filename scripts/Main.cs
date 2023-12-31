public partial class Main : Node
{

    [Export]
    public PackedScene MobScene { get; set; }

    int _score;

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
        GetNode<HUD>("HUD").ShowGameOver();

    }

    public void NewGame()
    {
        _score = 0;

        GetTree().CallGroup("mobs", Node.MethodName.QueueFree);

        var hud = GetNode<HUD>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");

        var player = GetNode<Player>("Player");
        var startPos = GetNode<Marker2D>("StartPosition");
        player.Start(startPos.Position);

        GetNode<Timer>("StartTimer").Start();
    }

    private void OnScoreTimerTimeout()
    {
        _score++;
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    private void OnMobTimerTimeout()
    {
        var mob = MobScene.Instantiate<Mob>();

        GetNode<HUD>("HUD").UpdateScore(_score);

        var mobSpawnLoc = GetNode<PathFollow2D>("Path2D/PathFollow2D");
        mobSpawnLoc.ProgressRatio = GD.Randf();

        var direction = mobSpawnLoc.Rotation + Mathf.Pi / 2;
        mob.Position = mobSpawnLoc.Position;

        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        var velocity = new Vector2(GD.RandRange(150, 250), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        AddChild(mob);
    }

    public override void _Ready()
    {
        NewGame();
    }
}
