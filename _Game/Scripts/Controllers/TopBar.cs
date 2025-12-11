using Godot;
using ProjectSyntax._Game.Scripts.Autoload;
using System;

public partial class TopBar : Panel
{
    [Export] public Label MoneyLabel;
    [Export] public Label DateLabel;
    [Export] public Label TimeLabel;

    [Export] public Button PauseButton;

    [Export] public Button BtnSpeed1;
    [Export] public Button BtnSpeed2;
    [Export] public Button BtnSpeed4;

    // Зберігаємо останню швидкість, щоб повертатися до неї після паузи
    private float _lastSpeed = 1.0f;

    public override void _Ready()
    {
        var ts = GameService.Instance.TimeSystem;

        ts.OnHourPassed += UpdateTimeDisplay;

        // Підписки на кнопки
        PauseButton.Pressed += OnPausePressed;
        BtnSpeed1.Pressed += () => SetSpeed(1.0f);
        BtnSpeed2.Pressed += () => SetSpeed(4.0f);
        BtnSpeed4.Pressed += () => SetSpeed(8.0f);

        BtnSpeed1.ButtonPressed = true;

        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateTimeDisplay(GameService.Instance.State.CurrentDate);
        MoneyLabel.Text = GameService.Instance.State.Money.ToString("C0");
    }

    private void UpdateTimeDisplay(DateTime date)
    {
        DateLabel.Text = date.ToString("dd MMM yyyy");
        TimeLabel.Text = date.ToString("HH:mm");
    }

    private void SetSpeed(float newSpeed)
    {
        _lastSpeed = newSpeed;
        GameService.Instance.TimeSystem.TimeScale = newSpeed;

        PauseButton.Text = "||";
    }

    private void OnPausePressed()
    {
        var ts = GameService.Instance.TimeSystem;

        if (ts.TimeScale > 0)
        {
            ts.TimeScale = 0;
            PauseButton.Text = "▶";
        }
        else
        {
            ts.TimeScale = _lastSpeed;
            PauseButton.Text = "||";

            // Відновлюємо натиснутий стан кнопки швидкості
            if (_lastSpeed == 1.0f) BtnSpeed1.ButtonPressed = true;
            if (_lastSpeed == 2.0f) BtnSpeed2.ButtonPressed = true;
            if (_lastSpeed >= 4.0f) BtnSpeed4.ButtonPressed = true;
        }
    }

    public override void _ExitTree()
    {
        if (GameService.Instance != null)
        {
            GameService.Instance.TimeSystem.OnHourPassed -= UpdateTimeDisplay;
        }
    }
}