using Godot;
using ProjectSyntax._Game.Scripts.Core;
using System;

namespace ProjectSyntax._Game.Scripts.Autoload
{
    public partial class GameService : Node
    {
        public static GameService Instance { get; private set; }

        public GameState State { get; private set; }
        public TimeSystem TimeSystem { get; private set; }

        public event Action OnProjectUpdated;
        public event Action OnProjectFinished;

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

            TimeSystem.OnHourPassed += OnGameHourPassed;
        }

        public override void _Process(double delta)
        {
            TimeSystem?.Update(delta);
        }

        private void OnGameHourPassed(DateTime currentDate)
        {
            ProcessDevelopment();
        }

        private void ProcessDevelopment()
        {
            var project = State.CurrentProject;

            if (project == null || project.IsFinished) return;

            // Placeholder work power (will be replaced by employee stats later)
            float workPower = 0.01f;

            project.PerformWork(workPower);

            decimal cost = project.GetHourlyCost();
            State.Money -= cost;

            OnProjectUpdated?.Invoke();

            if (project.IsFinished)
            {
                OnProjectFinished?.Invoke();
                TimeSystem.TimeScale = 0; // Pause game on finish
            }
        }
    }
}