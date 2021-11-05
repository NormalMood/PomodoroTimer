using System.Timers;
using Pomodoro_Timer.Base;

namespace Pomodoro_Timer.Models
{
    public class CountDownTimer : BasePropertyChanged
    {
        private ulong _milliseconds; 
        private Timer _mainTimer;
        private Timer _timerForSubtractingOneSecond;
        private const ulong _millisecondsInOneSecond = 1000;
        private void SetUpTimersAfterInit()
        {
            _mainTimer = new Timer();
            _mainTimer.Interval = _milliseconds;
            _mainTimer.Elapsed += Stop;

            _timerForSubtractingOneSecond = new Timer();
            _timerForSubtractingOneSecond.Interval = _millisecondsInOneSecond;
            _timerForSubtractingOneSecond.Elapsed += SubtractOneSecond;
        }
        public CountDownTimer(ulong milliSeconds)
        {
            _milliseconds = milliSeconds;
        }
        public void Start()
        {
            SetUpTimersAfterInit();
            _mainTimer.Start();
            _isTimerPaused = false;
            _timerForSubtractingOneSecond.Start();
        }
        public void Pause()
        {
            _mainTimer.Stop();
            _isTimerPaused = true;
            _timerForSubtractingOneSecond.Stop();
        }
        public void Resume()
        {
            SetUpTimersAfterInit();
            Start();
        }
        private void Stop(object obj, ElapsedEventArgs e)
        {
            Stop();
        }
        public void Stop()
        {
            Pause();
            _milliseconds = 0;
            SliderObjectContainer.slider.SetCoordsForAllArcs();
            _isTimerEndedItsWork = true;
        }
        private void SubtractOneSecond(object obj, ElapsedEventArgs e)
        {
            _milliseconds -= _millisecondsInOneSecond;
            SliderObjectContainer.slider.SetCoordsForSlider();
        }
        private byte _hoursLeft;
        private byte _minutesLeft;
        private byte _secondsLeft;

        private bool _isTimerPaused = false;
        public bool IsTimerPaused { get { return _isTimerPaused; } }
        private bool _isTimerEndedItsWork = false;
        public bool IsTimerEndedItsWork { get { return _isTimerEndedItsWork; } }
        public string GetTimeLeft()
        {
            TimeConverter.SplitMillisecondsToThreeTimeIntervals(_milliseconds, ref _hoursLeft, ref _minutesLeft, ref _secondsLeft);
            return _hoursLeft.ToString().PadLeft(2, '0') + ':' + _minutesLeft.ToString().PadLeft(2, '0') +
                    ':' + _secondsLeft.ToString().PadLeft(2, '0');
        }
    }
}
