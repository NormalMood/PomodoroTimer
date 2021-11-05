using System.Windows;
using Pomodoro_Timer.Models;
using Pomodoro_Timer.Base;
using System.Threading;
using System.ComponentModel;

namespace Pomodoro_Timer.ViewModels
{
    public class TimerHandlerViewModel : BasePropertyChanged
    {
        private PomodoroTimer pomodoroTimer = new PomodoroTimer();
        public static bool ShowedSettingsWindow { get; set; } = false;
        private bool _isHandler_ArcEndCoords_PropertyChanged_Active = false;
        private bool _isHandler_SliderColor_PropertyChanged_Active = false;

        private string _timeLeft = WorkTimeDisplayerObjectContainer.newWorkTimeDisplayer.WorkTimeKeeper;    
        public string TimeLeft
        {
            get
            {
                return _timeLeft;
            }
            set
            {
                _timeLeft = value;
                OnPropertyChanged();
            }
        }
        //*******************************************************
        public uint TimerWindowWidth { get { return TimerMainWindowSize.TimerMainWindowWidth; } }
        public uint TimerWindowHeight { get { return TimerMainWindowSize.TimerMainWindowHeight; } }
        public string PomodoroTimerIconImageSource { get { return TopPanel.PomodoroTimerIconImageFullPath; } }
        public string MinimizeImageSource { get { return TopPanel.MinimizeImageFullPath; } }
        public string TimerMainWindowCloseImageSource { get { return TopPanel.CloseImageFullPath; } }
        //*******************************************************
        private string _endXY_Arc1Point = "100, 0";
        public string EndXY_Arc1Point { get { return _endXY_Arc1Point; } set { _endXY_Arc1Point = value; OnPropertyChanged(); } }
        private string _endXY_Arc2Point = "200, 100";
        public string EndXY_Arc2Point { get { return _endXY_Arc2Point; } set { _endXY_Arc2Point = value; OnPropertyChanged(); } }
        private string _endXY_Arc3Point = "100, 200";
        public string EndXY_Arc3Point { get { return _endXY_Arc3Point; } set { _endXY_Arc3Point = value; OnPropertyChanged(); } }
        private string _endXY_Arc4Point = "0, 100";
        public string EndXY_Arc4Point { get { return _endXY_Arc4Point; } set { _endXY_Arc4Point = value; OnPropertyChanged(); } }
        private string _arcColor = SliderObjectContainer.slider.SliderColor;
        public string ArcColor 
        { 
            get 
            { 
                return _arcColor; 
            } 
            set 
            {
                _arcColor = value;
                OnPropertyChanged();
            }
        }
        //*******************************************************

        private Thread timeLeftThread;
        private void RefreshTimeLeftLabel()
        {
            while (pomodoroTimer.IsTimerWorking)
            {
                Thread.Sleep(500);
                TimeLeft = pomodoroTimer.GetTimeLeft(); 
            }
        }
        /*const for colors of buttons*/
        private const string _buttonUnavailableBackground = "#F4F4F4";
        private const string _buttonUnavailableBorderBrush = "#858585";
        private const string _buttonUnavailableForeground = "#858585";

        private const string _startButtonAvailableBackground = "#48CF59";
        private const string _startButtonAvailableBorderBrush = "Black";
        private const string _startButtonAvailableForeground = "White";

        private const string _pauseButtonAvailableBackground = "#0095D6";
        private const string _pauseButtonAvailableBorderBrush = "Black";
        private const string _pauseButtonAvailableForeground = "White";

        private const string _stopButtonAvailableBackground = "#F71802";
        private const string _stopButtonAvailableBorderBrush = "Black";
        private const string _stopButtonAvailableForeground = "White";
        /*-----------------------*/
        private string _startButtonBackground = _startButtonAvailableBackground;
        public string StartButtonBackground
        {
            get
            {
                return _startButtonBackground;
            }
            set
            {
                _startButtonBackground = value;
                OnPropertyChanged();
            }
        }
        private string _startButtonBorderBrush = _startButtonAvailableBorderBrush;
        public string StartButtonBorderBrush
        {
            get
            {
                return _startButtonBorderBrush;
            }
            set
            {
                _startButtonBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _startButtonForeground = _startButtonAvailableForeground;
        public string StartButtonForeground
        {
            get
            {
                return _startButtonForeground;
            }
            set
            {
                _startButtonForeground = value;
                OnPropertyChanged();
            }
        }
        private string _pauseButtonBackground = _buttonUnavailableBackground;
        public string PauseButtonBackground
        {
            get
            {
                return _pauseButtonBackground;
            }
            set
            {
                _pauseButtonBackground = value;
                OnPropertyChanged();
            }
        }
        private string _pauseButtonBorderBrush = _buttonUnavailableBorderBrush;
        public string PauseButtonBorderBrush
        {
            get
            {
                return _pauseButtonBorderBrush;
            }
            set
            {
                _pauseButtonBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _pauseButtonForeground = _buttonUnavailableForeground;
        public string PauseButtonForeground
        {
            get
            {
                return _pauseButtonForeground;
            }
            set
            {
                _pauseButtonForeground = value;
                OnPropertyChanged();
            }
        }
        private string _stopButtonBackground = _buttonUnavailableBackground;
        public string StopButtonBackground
        {
            get
            {
                return _stopButtonBackground;
            }
            set
            {
                _stopButtonBackground = value;
                OnPropertyChanged();
            }
        }
        private string _stopButtonBorderBrush = _buttonUnavailableBorderBrush;
        public string StopButtonBorderBrush
        {
            get
            {
                return _stopButtonBorderBrush;
            }
            set
            {
                _stopButtonBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _stopButtonForeground = _buttonUnavailableForeground;
        public string StopButtonForeground
        {
            get
            {
                return _stopButtonForeground;
            }
            set
            {
                _stopButtonForeground = value;
                OnPropertyChanged();
            }
        }
        public DelegateCommand StartTimer
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    DrawStartButtonUnavailable();
                    DrawPauseButtonAvailable();
                    DrawStopButtonAvailable();


                    NotificationObjectContainer.notification.PropertyChanged += NotificationContent_PropertyChanged;
                    if (!_isHandler_ArcEndCoords_PropertyChanged_Active)
                    {
                        SliderObjectContainer.slider.PropertyChanged += ArcEndCoords_PropertyChanged;
                        _isHandler_ArcEndCoords_PropertyChanged_Active = true;
                    }
                    if (!_isHandler_SliderColor_PropertyChanged_Active)
                    {
                        SliderObjectContainer.slider.PropertyChanged += SliderColor_PropertyChanged;
                        _isHandler_SliderColor_PropertyChanged_Active = true;
                    }
                    if (ShowedSettingsWindow)
                    {
                        WorkTimeDisplayerObjectContainer.newWorkTimeDisplayer.PropertyChanged -= WorkTimeKeeper_PropertyChanged;
                        ShowedSettingsWindow = false;
                    }
                    if (pomodoroTimer.IsTimerWorking)
                    {
                        pomodoroTimer.Resume();
                    }
                    else
                    {
                        pomodoroTimer.UpdateTimer();
                        pomodoroTimer.Start();
                    }
                    timeLeftThread = new Thread(RefreshTimeLeftLabel);
                    ThreadsContainer.AddThreadInThreadsContainer(timeLeftThread);
                    timeLeftThread.Start();
                    
                }, (obj) => (!pomodoroTimer.IsTimerWorking || pomodoroTimer.IsTimerPaused) && !ShowedSettingsWindow);
            }
        }
        public DelegateCommand PauseTimer
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    DrawStartButtonAvailable();
                    DrawPauseButtonUnavailable();

                   
                    pomodoroTimer.Pause();
                    NotificationObjectContainer.notification.PropertyChanged -= NotificationContent_PropertyChanged;
                    timeLeftThread.Abort();
                }, (obj) => pomodoroTimer.IsTimerWorking && !pomodoroTimer.IsTimerPaused);
            }
        }
        public DelegateCommand StopTimer
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    DrawStartButtonAvailable();
                    DrawPauseButtonUnavailable();
                    DrawStopButtonUnavailable();


                    pomodoroTimer.Stop();
                    NotificationObjectContainer.notification.PropertyChanged -= NotificationContent_PropertyChanged;
                    SliderObjectContainer.slider.HideSlider();
                    SliderObjectContainer.slider.PropertyChanged -= SliderColor_PropertyChanged;
                    _isHandler_SliderColor_PropertyChanged_Active = false;
                    SliderObjectContainer.slider.PropertyChanged -= ArcEndCoords_PropertyChanged;
                    _isHandler_ArcEndCoords_PropertyChanged_Active = false;
                    TimeLeft = WorkTimeDisplayerObjectContainer.newWorkTimeDisplayer.WorkTimeKeeper;
                    timeLeftThread.Abort();
                }, (obj) => pomodoroTimer.IsTimerWorking);
            }
        }
        public DelegateCommand OpenSettings
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    Views.SettingsWindow settingsWindow = new Views.SettingsWindow();
                    settingsWindow.Show();
                    ShowedSettingsWindow = true;
                    System.Threading.Tasks.Task.Run(() => 
                    {
                        while (ShowedSettingsWindow) { };
                        DrawStartButtonAvailable();
                    });
                    DrawStartButtonUnavailable();
                    WorkTimeDisplayerObjectContainer.newWorkTimeDisplayer.PropertyChanged += WorkTimeKeeper_PropertyChanged;
                }, (obj) => !pomodoroTimer.IsTimerWorking && !ShowedSettingsWindow);
            }
        }
        public DelegateCommand CloseWindow
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    NotificationObjectContainer.notification.PropertyChanged -= NotificationContent_PropertyChanged;
                    WorkTimeDisplayerObjectContainer.newWorkTimeDisplayer.PropertyChanged -= WorkTimeKeeper_PropertyChanged;
                    SliderObjectContainer.slider.PropertyChanged -= SliderColor_PropertyChanged;
                    SliderObjectContainer.slider.PropertyChanged -= ArcEndCoords_PropertyChanged;
                    ThreadsContainer.CloseAllThreads();
                    Application.Current.Shutdown();
                });
            }
        }
        public DelegateCommand MinimizeWindow
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    TimerWindowState = "Minimized";
                });
            }
        }
        private const string _initTimerWindowState = "Normal";
        private string _timerWindowState = _initTimerWindowState;
        public string TimerWindowState
        {
            get
            {
                return _timerWindowState;
            }
            set
            {
                _timerWindowState = value;
                OnPropertyChanged();
            }
        }
        private void WorkTimeKeeper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WorkTimeKeeper")
            {
                TimeLeft = WorkTimeDisplayerObjectContainer.newWorkTimeDisplayer.WorkTimeKeeper;
            }
        }
        private void NotificationContent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TextBlockContent")
            {
                Application.Current.Dispatcher?.Invoke(() =>
                {
                    Views.NotificationWindow notificationWindow = new Views.NotificationWindow();
                    if (notificationWindow != null)
                    {
                        notificationWindow.Top = SystemParameters.WorkArea.Height - NotificationObjectContainer.notification.NotificationWindowHeight;
                        notificationWindow.Left = SystemParameters.WorkArea.Width - NotificationObjectContainer.notification.NotificationWindowWidth;
                        notificationWindow.Show();
                    }
                });
            }
        }
        private void ArcEndCoords_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EndX_Arc1")
            {
                EndXY_Arc1Point = SliderObjectContainer.slider.EndX_Arc1.ToString().Replace(",", ".") + ", " +
                      SliderObjectContainer.slider.EndY_Arc1.ToString().Replace(",", ".");
            }
            else if (e.PropertyName == "EndX_Arc2")
            {
                EndXY_Arc2Point = SliderObjectContainer.slider.EndX_Arc2.ToString().Replace(",", ".") + ", " +
                      SliderObjectContainer.slider.EndY_Arc2.ToString().Replace(",", ".");
            }
            else if (e.PropertyName == "EndX_Arc3")
            {
                EndXY_Arc3Point = SliderObjectContainer.slider.EndX_Arc3.ToString().Replace(",", ".") + ", " +
                      SliderObjectContainer.slider.EndY_Arc3.ToString().Replace(",", ".");
            }
            else if (e.PropertyName == "EndX_Arc4")
            {
                EndXY_Arc4Point = SliderObjectContainer.slider.EndX_Arc4.ToString().Replace(",", ".") + ", " +
                      SliderObjectContainer.slider.EndY_Arc4.ToString().Replace(",", ".");
            }
        }
       public void SliderColor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SliderColor")
            {
                ArcColor = SliderObjectContainer.slider.SliderColor;
            }
        }
        private void DrawStartButtonAvailable()
        {
            StartButtonBackground = _startButtonAvailableBackground;
            StartButtonBorderBrush = _startButtonAvailableBorderBrush;
            StartButtonForeground = _startButtonAvailableForeground;
        }
        private void DrawStartButtonUnavailable()
        {
            StartButtonBackground = _buttonUnavailableBackground;
            StartButtonBorderBrush = _buttonUnavailableBorderBrush;
            StartButtonForeground = _buttonUnavailableForeground;
        }
        private void DrawPauseButtonAvailable()
        {
            PauseButtonBackground = _pauseButtonAvailableBackground;
            PauseButtonBorderBrush = _pauseButtonAvailableBorderBrush;
            PauseButtonForeground = _pauseButtonAvailableForeground;
        }
        private void DrawPauseButtonUnavailable()
        {
            PauseButtonBackground = _buttonUnavailableBackground;
            PauseButtonBorderBrush = _buttonUnavailableBorderBrush;
            PauseButtonForeground = _buttonUnavailableForeground;
        }
        private void DrawStopButtonAvailable()
        {
            StopButtonBackground = _stopButtonAvailableBackground;
            StopButtonBorderBrush = _stopButtonAvailableBorderBrush;
            StopButtonForeground = _stopButtonAvailableForeground;
        }
        private void DrawStopButtonUnavailable()
        {
            StopButtonBackground = _buttonUnavailableBackground;
            StopButtonBorderBrush = _buttonUnavailableBorderBrush;
            StopButtonForeground = _buttonUnavailableForeground;
        }
    }
}
