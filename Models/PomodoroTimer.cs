using System.Threading;

namespace Pomodoro_Timer.Models
{
    public class PomodoroTimer
    {
        private CountDownTimer _timerForWork;
        private CountDownTimer _timerForShortRest;
        private CountDownTimer _timerForLongRest;
        private ulong[] _timerIntervals = new ulong[3] { 0, 0, 0 }; //work, short rest, long rest
        private CountDownTimer[] _arrayOfTimers;
        private byte[] _orderOfCallingTimers = { 0, 1, 0, 1, 0, 1, 0, 2 }; //take index by % 8 operation
        private const short _initNumberForCallingTimers = -1;
        private int _numberForCallingTimers = _initNumberForCallingTimers;
        private Thread _threadForSwitchingTimer = null;
        private bool _isTimerSwitcherOn = false;

        private bool _isTimerWorking = false;
        public bool IsTimerWorking
        {
            get
            {
                return _isTimerWorking;
            }
        }
        private bool _isTimerPaused = false;
        public bool IsTimerPaused
        {
            get
            {
                return _isTimerPaused;
            }
        }
        public PomodoroTimer()
        {
            _arrayOfTimers = new CountDownTimer[] { _timerForWork, _timerForShortRest, _timerForLongRest };
            UpdateTimer();
            _threadForSwitchingTimer = new Thread(SwitchTimer);
            ThreadsContainer.AddThreadInThreadsContainer(_threadForSwitchingTimer);
        }
        public void UpdateTimer()
        {
            TimerSettings.SetConfigsFromSettings();
            _timerIntervals[0] = TimerSettings.WorkTime;
            _timerIntervals[1] = TimerSettings.ShortRestTime;
            _timerIntervals[2] = TimerSettings.LongRestTime;
        }
        private void SwitchTimer()
        {
            while (!_arrayOfTimers[_orderOfCallingTimers[_numberForCallingTimers % 8]].IsTimerEndedItsWork) //THIS IS REALLY USEFUL!!!
            {
             
            }
            if (_isTimerSwitcherOn)
            {
                if (_numberForCallingTimers % 2 == 0)
                {
                    TimerMusicPlayerObjectContainer.timerMusicPlayer.PlayNotificationMusic(isRestMusicOn: true);
                    NotificationObjectContainer.notification.SetNotificationContent(isTimeToRest: true);
                }
                else
                {
                    TimerMusicPlayerObjectContainer.timerMusicPlayer.PlayNotificationMusic(isRestMusicOn: false);
                    NotificationObjectContainer.notification.SetNotificationContent(isTimeToRest: false);
                }
                SliderObjectContainer.slider.SetColorForMovingBackwardSlider();
                SliderObjectContainer.slider.SetSlidersCoordsForReverse();
                System.Threading.Tasks.Task.Run(() => {
                    DrawMovingBackwardSlider();
                });
                Thread.Sleep(_millisecondsForSleepBeforeMovingBackwardSliderHides);
                SliderObjectContainer.slider.SetColorForMovingForwardSlider();
                Start();
            }
        }
        private const int _millisecondsForSleepBeforeMovingBackwardSliderHides = 1960;
        private const byte _amountOfSectorsForMovingBackwardSlider = 16;
        private const byte _millisecondsForSleepBetweenSliderDrawing = 60;
        private const int _millisecondsForSleepBeforeSliderDrawing = 1000;
        private void DrawMovingBackwardSlider()
        {
            SliderObjectContainer.slider.SetColorForMovingBackwardSlider();
            SliderObjectContainer.slider.SetSlidersCoordsForReverse();
            Thread.Sleep(_millisecondsForSleepBeforeSliderDrawing);
            for (int i = 0; i < _amountOfSectorsForMovingBackwardSlider; i++)
            {
                SliderObjectContainer.slider.SetCoordsForSlider();
                Thread.Sleep(_millisecondsForSleepBetweenSliderDrawing);
            }
        }
        public void Start()
        {
            _numberForCallingTimers++;
            _arrayOfTimers[_orderOfCallingTimers[_numberForCallingTimers % 8]] = 
                new CountDownTimer(_timerIntervals[_orderOfCallingTimers[_numberForCallingTimers % 8]]);
            SliderObjectContainer.slider.CalculateSlidersCoordsForCurrentTimer(_timerIntervals[_orderOfCallingTimers[_numberForCallingTimers % 8]]);

            _arrayOfTimers[_orderOfCallingTimers[_numberForCallingTimers % 8]].Start();
            _isTimerWorking = true;
            _isTimerSwitcherOn = true;
            _isTimerPaused = false;
            _threadForSwitchingTimer = new Thread(SwitchTimer);
            ThreadsContainer.AddThreadInThreadsContainer(_threadForSwitchingTimer);
            _threadForSwitchingTimer.Start();
        }
        public void Pause()
        {
            _arrayOfTimers[_orderOfCallingTimers[_numberForCallingTimers % 8]].Pause();
            _isTimerPaused = true;
            _threadForSwitchingTimer.Abort();
        }
        public void Resume()
        {
            if (_arrayOfTimers[_orderOfCallingTimers[_numberForCallingTimers % 8]].IsTimerEndedItsWork)
            {
                Start();
            }
            else
            {
                _arrayOfTimers[_orderOfCallingTimers[_numberForCallingTimers % 8]].Resume();
                _isTimerPaused = false;
                _threadForSwitchingTimer = new Thread(SwitchTimer);
                ThreadsContainer.AddThreadInThreadsContainer(_threadForSwitchingTimer);
                _threadForSwitchingTimer.Start();
            }
        }
        public void Stop()
        {
            _isTimerSwitcherOn = false;
            _arrayOfTimers[_orderOfCallingTimers[_numberForCallingTimers % 8]].Stop();
            _isTimerWorking = false;
            _isTimerPaused = false;
            _numberForCallingTimers = _initNumberForCallingTimers;
            _threadForSwitchingTimer.Abort();
            SliderObjectContainer.slider.SetColorForMovingForwardSlider();
        }
        public string GetTimeLeft() => 
            _arrayOfTimers[_orderOfCallingTimers[(_numberForCallingTimers == -1? 0 : _numberForCallingTimers) % 8]]?.GetTimeLeft();
    }
}
