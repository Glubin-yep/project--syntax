using Godot;
using ProjectSyntax._Game.Scripts.Autoload;

public partial class DebugUI : Control
{
	private Label _dateLabel;

	public override void _Ready()
	{
		// Створюємо лейбл кодом, щоб не возитися в редакторі (для тесту)
		_dateLabel = new Label();
		_dateLabel.Position = new Vector2(50, 50);
		AddChild(_dateLabel);

		// ПІДПИСУЄМОСЯ НА ПОДІЮ
		// Це ключовий момент! Ми не перевіряємо час у _Process.
		// Ми реагуємо тільки тоді, коли час реально змінився.
		GameService.Instance.TimeSystem.OnHourPassed += UpdateDateLabel;

		// Початкове оновлення тексту
		UpdateDateLabel(GameService.Instance.State.CurrentDate);
	}

	private void UpdateDateLabel(System.DateTime newDate)
	{
		// Оновлюємо текст
		_dateLabel.Text = $"Date: {newDate:yyyy-MM-dd HH:mm}\nMoney: {GameService.Instance.State.Money:C0}";
	}

	// Дуже важливо відписуватися від подій, коли об'єкт знищується, 
	// щоб уникнути витоку пам'яті.
	public override void _ExitTree()
	{
		if (GameService.Instance != null)
		{
			GameService.Instance.TimeSystem.OnHourPassed -= UpdateDateLabel;
		}
	}
}
