using ProjectSyntax._Game.Scripts.Content;
using System;

namespace ProjectSyntax._Game.Scripts.Core
{
    // A concrete instance of a game being developed by the player
    [Serializable]
    public class GameProject
    {
        public string Name { get; set; }
        public GenreDefinition Genre { get; set; }

        // Progress: 0.0f to 1.0f (100%)
        public float Progress { get; set; }

        // Quality metrics accumulated during development
        public float GameplayScore { get; set; }
        public float GraphicsScore { get; set; }
        public float SoundScore { get; set; }

        public int BugsCount { get; set; }

        public bool IsFinished => Progress >= 1.0f;

        public GameProject(string name, GenreDefinition genre)
        {
            Name = name;
            Genre = genre;
            Progress = 0f;
        }
    }
}