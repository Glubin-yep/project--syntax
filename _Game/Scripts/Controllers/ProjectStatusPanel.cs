using Godot;
using ProjectSyntax._Game.Scripts.Autoload;

namespace ProjectSyntax._Game.Scripts.Controllers
{
    public partial class ProjectStatusPanel : PanelContainer
    {
        [Export] public Label ProjectNameLabel;
        [Export] public ProgressBar ProgressBar;
        [Export] public Label StatsLabel;

        public override void _Ready()
        {
            GameService.Instance.OnProjectUpdated += RefreshUI;
            RefreshUI();
        }

        public override void _Process(double delta)
        {
            // Only show panel if a project is active
            Visible = GameService.Instance.State.CurrentProject != null;
        }

        private void RefreshUI()
        {
            var project = GameService.Instance.State.CurrentProject;
            if (project == null) return;

            if (ProjectNameLabel != null)
                ProjectNameLabel.Text = $"{project.Name} ({project.Genre.DisplayName})";

            if (ProgressBar != null)
                ProgressBar.Value = project.Progress * 100;

            if (StatsLabel != null)
            {
                StatsLabel.Text = $"Gameplay: {project.GameplayScore:F0}\n" +
                                  $"Graphics: {project.GraphicsScore:F0}\n" +
                                  $"Sound: {project.SoundScore:F0}\n" +
                                  $"Bugs: {project.BugsCount}";
            }
        }

        public override void _ExitTree()
        {
            if (GameService.Instance != null)
            {
                GameService.Instance.OnProjectUpdated -= RefreshUI;
            }
        }
    }
}