public partial class Main : Node
{

    [Export]
    public PackedScene MobScene { get; set; }

    int _score;

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }

    public void NewGame()
    {
        _score = 0;

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

        var mobSpawnLoc = GetNode<PathFollow2D>("Path2D/PathFollow2D");
        mobSpawnLoc.ProgressRatio = GD.Randf();

        var direction = mobSpawnLoc.Rotation + Mathf.Pi / 2;
        mob.Position = mobSpawnLoc.Position;

        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        var velocity = new Vector2((float)GD.RandRange(150, 250), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        AddChild(mob);
    }

    public override void _Ready()
    {
        NewGame();
    }
}
