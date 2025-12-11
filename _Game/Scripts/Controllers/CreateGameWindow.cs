using Godot;
using ProjectSyntax._Game.Scripts.Autoload;
using ProjectSyntax._Game.Scripts.Content;
using ProjectSyntax._Game.Scripts.Core;
using ProjectSyntax._Game.Scripts.Utils;
using System.Collections.Generic;

namespace ProjectSyntax._Game.Scripts.Controllers
{
    public partial class CreateGameWindow : PanelContainer
    {
        [Export] public LineEdit NameInput;
        [Export] public OptionButton GenreSelector;
        [Export] public Button StartButton;
        [Export] public Button CancelButton;

        private List<GenreDefinition> _availableGenres;

        public override void _Ready()
        {
            LoadGenres();

            StartButton.Pressed += OnStartPressed;
            CancelButton.Pressed += OnCancelPressed;
        }

        private void LoadGenres()
        {
            // Load all genres from the Resources folder
            _availableGenres = ContentLoader.LoadAll<GenreDefinition>("res://_Game/Resources/Genres");

            GenreSelector.Clear();
            foreach (var genre in _availableGenres)
            {
                GenreSelector.AddItem(genre.DisplayName);
            }

            // Select first by default if available
            if (_availableGenres.Count > 0)
                GenreSelector.Selected = 0;
        }

        private void OnStartPressed()
        {
            string gameName = NameInput.Text.Trim();

            if (string.IsNullOrEmpty(gameName))
            {
                GD.PrintErr("Game name cannot be empty!");
                return; // Optionally show a dialog or red border
            }

            if (GenreSelector.Selected == -1) return;

            GenreDefinition selectedGenre = _availableGenres[GenreSelector.Selected];

            var newProject = new GameProject(gameName, selectedGenre);

            GameService.Instance.State.CurrentProject = newProject;
            GD.Print($"Started project: {gameName} ({selectedGenre.DisplayName})");

            QueueFree();
        }

        private void OnCancelPressed()
        {
            QueueFree();
        }
    }
}