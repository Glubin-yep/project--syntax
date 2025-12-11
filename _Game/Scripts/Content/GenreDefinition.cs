using Godot;

namespace ProjectSyntax._Game.Scripts.Content
{
    // Represents a static configuration for a game genre
    [GlobalClass] // Allows creating this resource from the Editor (Right Click -> New Resource)
    public partial class GenreDefinition : Resource
    {
        [Export] public string DisplayName { get; set; } = "New Genre";
        [Export] public float DevelopmentCostMultiplier { get; set; } = 1.0f;
        [Export] public float SalesMultiplier { get; set; } = 1.0f;

        // We can add later: Good Combinations with Themes, specific algorithm weights, etc.
    }
}