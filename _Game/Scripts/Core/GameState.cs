using System;

namespace ProjectSyntax._Game.Scripts.Core
{
    // Модель даних 
    [Serializable]
    public class GameState
    {
        public string CompanyName { get; set; } = "Indie Studio";
        public decimal Money { get; set; } = 10000m;
        public DateTime CurrentDate { get; set; }

        public GameState()
        {
            CurrentDate = new DateTime(1985, 1, 1);
        }
    }


}