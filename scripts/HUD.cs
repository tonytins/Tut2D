using System.Security.Cryptography.X509Certificates;

public partial class HUD : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	public void ShowMessage(string text)
	{
		var msg = GetNode<Label>("Message");
		msg.Text = text;
		msg.Show();

		GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver()
	{
		ShowMessage("Gamer Over");

		var msgTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(msgTimer, Timer.SignalName.Timeout);

		var msg = GetNode<Label>("Message");
		msg.Text = "Dodge the\nCreeps!";
		msg.Show();

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}

	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}


	private void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
	}


	private void OnMessageTimerTimeOut()
	{
		GetNode<Label>("Message").Hide();
	}


	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
