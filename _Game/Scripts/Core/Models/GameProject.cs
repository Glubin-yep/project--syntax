using ProjectSyntax._Game.Scripts.Content;
using System;

namespace ProjectSyntax._Game.Scripts.Core
{
    [Serializable]
    public class GameProject
    {
        public string Name { get; set; }
        public GenreDefinition Genre { get; set; }

        public float Progress { get; set; } // 0.0f to 1.0f

        public float GameplayScore { get; set; }
        public float GraphicsScore { get; set; }
        public float SoundScore { get; set; }

        public int BugsCount { get; set; }

        public bool IsFinished => Progress >= 1.0f;

        private const decimal BaseHourlyCost = 50m;

        public GameProject(string name, GenreDefinition genre)
        {
            Name = name;
            Genre = genre;
            Progress = 0f;
        }

        public void PerformWork(float workPower)
        {
            if (IsFinished) return;

            Progress += workPower;

            if (Progress >= 1.0f)
            {
                Progress = 1.0f;
            }

            // Simulate stat accumulation
            GenerateStats();
        }

        public decimal GetHourlyCost()
        {
            return BaseHourlyCost * (decimal)Genre.DevelopmentCostMultiplier;
        }

        private void GenerateStats()
        {
            // Placeholder logic for stat generation
            Random rnd = new Random();
            if (rnd.NextDouble() > 0.7) GameplayScore += 1;
            if (rnd.NextDouble() > 0.7) GraphicsScore += 1;
            if (rnd.NextDouble() > 0.7) SoundScore += 1;
            if (rnd.NextDouble() > 0.9) BugsCount++;
        }
    }
}