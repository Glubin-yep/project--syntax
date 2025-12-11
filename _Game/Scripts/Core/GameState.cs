using System;
using System.Collections.Generic;

namespace ProjectSyntax._Game.Scripts.Core
{
    [Serializable]
    public class GameState
    {
        public string CompanyName { get; set; } = "Indie Studio";
        public decimal Money { get; set; } = 10000m;
        public DateTime CurrentDate { get; set; }

        // The project currently currently in development (null if idle)
        public GameProject CurrentProject { get; set; }

        // History of released games
        public List<GameProject> ReleasedGames { get; set; } = new List<GameProject>();

        public GameState()
        {
            CurrentDate = new DateTime(1985, 1, 1);
        }
    }
}