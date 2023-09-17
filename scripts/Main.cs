using Godot;

public partial class Main : Node
{

    [Export] public PackedScene MobScene { get; set; }

    int _score;

    public void GameOver()
    {
        GetNode<Godot.Timer>("MobTimer").Stop();
        GetNode<Godot.Timer>("ScoreTimer").Stop();
    }

    public void NewGame()
    {
        _score = 0;

        var player = GetNode<Player>("Player");
        var startPos = GetNode<Marker2D>("StartPosition");
        player.Start(startPos.Position);

        GetNode<Godot.Timer>("StartTimer").Start();
    }

    private void OnScoreTimerTimeout()
    {
        _score++;
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Godot.Timer>("MobTimer").Start();
        GetNode<Godot.Timer>("ScoreTimer").Start();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}


