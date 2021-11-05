
namespace Pomodoro_Timer.Models
{
    public class WorkTimeDisplayer : Base.BasePropertyChanged   //For showing work time inside the circle on the main window
    {
        private string _workTimeKeeper = $"{GetWorkTime()}";
        public string WorkTimeKeeper
        {
            get
            {
                return _workTimeKeeper;
            }
            set
            {
                _workTimeKeeper = value;
                OnPropertyChanged();
            }
        }
        public void RefreshWorkTimeKeeperProperty()
        {
            WorkTimeKeeper = GetWorkTime();
        }
        private static string GetWorkTime()
        {
            byte hours = 0;
            byte minutes = 0;
            byte seconds = 0;
            TimeConverter.SplitMillisecondsToThreeTimeIntervals(TimerSettings.GetWorkTimeInMillisecondsFromSettings(),
                ref hours, ref minutes, ref seconds);
            return $"0{hours}:{(minutes < 10 ? $"0{minutes}" : $"{minutes}")}:{(seconds < 10 ? $"0{seconds}" : $"{seconds}")}";
        }

    }
}
