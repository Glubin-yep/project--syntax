using Godot;

namespace ProjectSyntax._Game.Scripts.Controllers
{
    public partial class MainSceneController : Control
    {
        [ExportGroup("UI Components")]
        [Export] public Button NewGameButton;

        [ExportGroup("Scene References")]
        [Export] public PackedScene CreateGameWindowScene;

        public override void _Ready()
        {
            if (NewGameButton != null)
            {
                NewGameButton.Pressed += OnNewGamePressed;
            }
            else
            {
                GD.PrintErr("MainSceneController: NewGameButton is not assigned in Inspector!");
            }
        }

        private void OnNewGamePressed()
        {
            // 1. Check if window is already open to prevent duplicates
            if (GetNodeOrNull("CreateGameWindow") != null) return;

            // 2. Safety check
            if (CreateGameWindowScene == null)
            {
                GD.PrintErr("CreateGameWindowScene is not assigned! Please drag .tscn file to the Inspector.");
                return;
            }

            // 3. Instantiate and show window
            var windowInstance = CreateGameWindowScene.Instantiate<Control>();

            // Optional: Set name explicitly to find it later easily
            windowInstance.Name = "CreateGameWindow";

            AddChild(windowInstance);

            // Ensure it's centered (if anchors in the scene file aren't set correctly)
            windowInstance.SetAnchorsPreset(LayoutPreset.Center);
        }
    }
}