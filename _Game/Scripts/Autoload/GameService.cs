using Godot;
using ProjectSyntax._Game.Scripts.Core;

namespace ProjectSyntax._Game.Scripts.Autoload
{
    public partial class GameService : Node
    {
        // (Singleton)
        public static GameService Instance { get; private set; }

        public GameState State { get; private set; }
        public TimeSystem TimeSystem { get; private set; }

        public override void _EnterTree()
        {
            if (Instance != null)
            {
                QueueFree();
                return;
            }
            Instance = this;
        }

        public override void _Ready()
        {
            State = new GameState();

            TimeSystem = new TimeSystem(State);

            GD.Print("GameService успішно ініціалізовано. Дата: " + State.CurrentDate);
        }

        public override void _Process(double delta)
        {
            TimeSystem?.Update(delta);
        }
    }
}