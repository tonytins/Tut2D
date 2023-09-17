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
        // player.Start(startPos.Position);

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
}


