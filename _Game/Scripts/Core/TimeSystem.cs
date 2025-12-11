using System;

namespace ProjectSyntax._Game.Scripts.Core
{
    // Система часу
    public class TimeSystem
    {
        public event Action<DateTime> OnHourPassed;
        public event Action<DateTime> OnDayPassed;
        public event Action<DateTime> OnMonthPassed;

        private GameState _state;
        private double _accumulatedTime;

        // Налаштування балансу: 2 реальні секунди = 1 ігрова година
        private const double RealSecondsPerGameHour = 2.0;

        public float TimeScale { get; set; } = 1.0f; // 0 = пауза

        public TimeSystem(GameState state)
        {
            _state = state;
        }

        public void Update(double delta)
        {
            if (TimeScale <= 0) return;

            _accumulatedTime += delta * TimeScale;

            while (_accumulatedTime >= RealSecondsPerGameHour)
            {
                _accumulatedTime -= RealSecondsPerGameHour;
                AdvanceHour();
            }
        }

        private void AdvanceHour()
        {
            // Зберігаємо попередні значення для перевірки зміни дня/місяця
            int oldDay = _state.CurrentDate.Day;
            int oldMonth = _state.CurrentDate.Month;

            _state.CurrentDate = _state.CurrentDate.AddHours(1);

            // Викликаємо подію години
            OnHourPassed?.Invoke(_state.CurrentDate);

            if (_state.CurrentDate.Day != oldDay)
            {
                OnDayPassed?.Invoke(_state.CurrentDate);
            }

            if (_state.CurrentDate.Month != oldMonth)
            {
                OnMonthPassed?.Invoke(_state.CurrentDate);
            }
        }
    }
}
