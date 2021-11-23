using System;

namespace Pomodoro_Timer.ViewModels
{
    public class TimerSettingsViewModel : Base.BasePropertyChanged
    {
        public uint SettingsWindowWidth { get { return Models.TimerSettings.SettingsWindowWidth; } }
        public uint SettingsWindowHeight { get { return Models.TimerSettings.SettingsWindowHeight; } }
        public string SettingsWindowCloseImageSource { get { return Models.TopPanel.CloseImageFullPath; } }
        public Action CloseSettingsWindowAction { get; set; }
        //===============Border brushes for text boxes=============
        private const string _textBoxStandardBorderBrush = "#007ACC";
        private const string _textBoxErrorBorderBrush = "Red";
        private string _textBoxWorkHoursBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxWorkHoursBorderBrush 
        { 
            get
            {
                return _textBoxWorkHoursBorderBrush;
            }
            set
            {
                _textBoxWorkHoursBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxWorkMinBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxWorkMinBorderBrush
        {
            get
            {
                return _textBoxWorkMinBorderBrush;
            }
            set
            {
                _textBoxWorkMinBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxWorkSecBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxWorkSecBorderBrush
        {
            get
            {
                return _textBoxWorkSecBorderBrush;
            }
            set
            {
                _textBoxWorkSecBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxShortRestHoursBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxShortRestHoursBorderBrush
        {
            get
            {
                return _textBoxShortRestHoursBorderBrush;
            }
            set
            {
                _textBoxShortRestHoursBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxShortRestMinBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxShortRestMinBorderBrush
        {
            get
            {
                return _textBoxShortRestMinBorderBrush;
            }
            set
            {
                _textBoxShortRestMinBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxShortRestSecBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxShortRestSecBorderBrush
        {
            get
            {
                return _textBoxShortRestSecBorderBrush;
            }
            set
            {
                _textBoxShortRestSecBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxLongRestHoursBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxLongRestHoursBorderBrush
        {
            get
            {
                return _textBoxLongRestHoursBorderBrush;
            }
            set
            {
                _textBoxLongRestHoursBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxLongRestMinBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxLongRestMinBorderBrush
        {
            get
            {
                return _textBoxLongRestMinBorderBrush;
            }
            set
            {
                _textBoxLongRestMinBorderBrush = value;
                OnPropertyChanged();
            }
        }
        private string _textBoxLongRestSecBorderBrush = _textBoxStandardBorderBrush;
        public string TextBoxLongRestSecBorderBrush
        {
            get
            {
                return _textBoxLongRestSecBorderBrush;
            }
            set
            {
                _textBoxLongRestSecBorderBrush = value;
                OnPropertyChanged();
            }
        }
        //=========================================================
        private void ComplementToTwoSymbols(ref string timeInterval)
        {
            if (timeInterval.Length < 2)
                timeInterval = $"0{timeInterval}";
        }
        private bool IsIntervalNumber(string timeInterval)
        {
            for(int i = 0; i < timeInterval.Length; i++)
            {
                if (!char.IsDigit(timeInterval[i]))
                    return false;
            }
            return true;
        }
        private bool IsInputForTimerCorrect()
        {
            bool isInputCorrect = true;
            string[] arrayOfTimeIntervals =
                {
                    _workTimeHours, _workTimeMinutes, _workTimeSeconds,
                    _shortRestTimeHours, _shortRestTimeMinutes, _shortRestTimeSeconds,
                    _longRestTimeHours, _longRestTimeMinutes, _longRestTimeSeconds
                };
            for(int i = 0; i < arrayOfTimeIntervals.Length; i++)
            {
                if (!IsIntervalNumber(arrayOfTimeIntervals[i]))
                {
                    isInputCorrect = false;
                    SetBorderBrushForOneTextBox(i, _textBoxErrorBorderBrush);
                }
                else
                    SetBorderBrushForOneTextBox(i, _textBoxStandardBorderBrush);
            }
            return isInputCorrect;
        }
        private void SetBorderBrushForOneTextBox(int index, string borderBrushColor)
        {
            switch (index)
            {
                case 0:
                    TextBoxWorkHoursBorderBrush = borderBrushColor;
                    break;
                case 1:
                    TextBoxWorkMinBorderBrush = borderBrushColor;
                    break;
                case 2:
                    TextBoxWorkSecBorderBrush = borderBrushColor;
                    break;
                case 3:
                    TextBoxShortRestHoursBorderBrush = borderBrushColor;
                    break;
                case 4:
                    TextBoxShortRestMinBorderBrush = borderBrushColor;
                    break;
                case 5:
                    TextBoxShortRestSecBorderBrush = borderBrushColor;
                    break;
                case 6:
                    TextBoxLongRestHoursBorderBrush = borderBrushColor;
                    break;
                case 7:
                    TextBoxLongRestMinBorderBrush = borderBrushColor;
                    break;
                case 8:
                    TextBoxLongRestSecBorderBrush = borderBrushColor;
                    break;
            }
        }
        private void SetStandardBorderBrushForAllTextBoxes()
        {
            TextBoxWorkHoursBorderBrush = _textBoxStandardBorderBrush;
            TextBoxWorkMinBorderBrush = _textBoxStandardBorderBrush;
            TextBoxWorkSecBorderBrush = _textBoxStandardBorderBrush;
            TextBoxShortRestHoursBorderBrush = _textBoxStandardBorderBrush;
            TextBoxShortRestMinBorderBrush = _textBoxStandardBorderBrush;
            TextBoxShortRestSecBorderBrush = _textBoxStandardBorderBrush;
            TextBoxLongRestHoursBorderBrush = _textBoxStandardBorderBrush;
            TextBoxLongRestMinBorderBrush = _textBoxStandardBorderBrush;
            TextBoxLongRestSecBorderBrush = _textBoxStandardBorderBrush;
        }
        private void SetErrorBorderBrushForWorkTimeTextBoxes()
        {
            TextBoxWorkHoursBorderBrush = _textBoxErrorBorderBrush;
            TextBoxWorkMinBorderBrush = _textBoxErrorBorderBrush;
            TextBoxWorkSecBorderBrush = _textBoxErrorBorderBrush;
        }
        private void SetErrorBorderBrushForShortRestTimeTextBoxes()
        {
            TextBoxShortRestHoursBorderBrush = _textBoxErrorBorderBrush;
            TextBoxShortRestMinBorderBrush = _textBoxErrorBorderBrush;
            TextBoxShortRestSecBorderBrush = _textBoxErrorBorderBrush;
        }
        /*Timer intervals*/
        private string _workTimeHours;
        public string WorkTimeHours 
        {
            get 
            {
                ComplementToTwoSymbols(ref _workTimeHours);
                return _workTimeHours;
            }
            set
            {
                _workTimeHours = value;
                ComplementToTwoSymbols(ref _workTimeHours);
                OnPropertyChanged();
            }
        }
        private string _workTimeMinutes;
        public string WorkTimeMinutes
        {
            get
            {
                ComplementToTwoSymbols(ref _workTimeMinutes);
                return _workTimeMinutes;
            } 
            set 
            {
                _workTimeMinutes = value;
                ComplementToTwoSymbols(ref _workTimeMinutes);
                OnPropertyChanged();
            } 
        }
        private string _workTimeSeconds;
        public string WorkTimeSeconds 
        {
            get
            {
                ComplementToTwoSymbols(ref _workTimeSeconds);
                return _workTimeSeconds;
            }
            set
            {
                _workTimeSeconds = value;
                ComplementToTwoSymbols(ref _workTimeSeconds);
                OnPropertyChanged();
            }
        }


        private string _shortRestTimeHours;
        public string ShortRestTimeHours
        {
            get
            {
                ComplementToTwoSymbols(ref _shortRestTimeHours);
                return _shortRestTimeHours;
            }
            set
            {
                _shortRestTimeHours = value;
                ComplementToTwoSymbols(ref _shortRestTimeHours);
                OnPropertyChanged();
            }
        }
        private string _shortRestTimeMinutes;
        public string ShortRestTimeMinutes 
        {
            get
            {
                ComplementToTwoSymbols(ref _shortRestTimeMinutes);
                return _shortRestTimeMinutes;
            }
            set
            {
                _shortRestTimeMinutes = value;
                ComplementToTwoSymbols(ref _shortRestTimeMinutes);
                OnPropertyChanged();
            }
        }
        private string _shortRestTimeSeconds;
        public string ShortRestTimeSeconds
        {
            get
            {
                ComplementToTwoSymbols(ref _shortRestTimeSeconds);
                return _shortRestTimeSeconds;
            }
            set
            {
                _shortRestTimeSeconds = value;
                ComplementToTwoSymbols(ref _shortRestTimeSeconds);
                OnPropertyChanged();
            }
        }


        private string _longRestTimeHours;
        public string LongRestTimeHours 
        {
            get
            {
                ComplementToTwoSymbols(ref _longRestTimeHours);
                return _longRestTimeHours;
            }
            set
            {
                _longRestTimeHours = value;
                ComplementToTwoSymbols(ref _longRestTimeHours);
                OnPropertyChanged();
            }
        }
        private string _longRestTimeMinutes;
        public string LongRestTimeMinutes 
        {
            get
            {
                ComplementToTwoSymbols(ref _longRestTimeMinutes);
                return _longRestTimeMinutes;
            }
            set
            {
                _longRestTimeMinutes = value;
                ComplementToTwoSymbols(ref _longRestTimeMinutes);
                OnPropertyChanged();
            }
        }
        private string _longRestTimeSeconds;
        public string LongRestTimeSeconds 
        {
            get
            {
                ComplementToTwoSymbols(ref _longRestTimeSeconds);
                return _longRestTimeSeconds;
            }
            set
            {
                _longRestTimeSeconds = value;
                ComplementToTwoSymbols(ref _longRestTimeSeconds);
                OnPropertyChanged();
            }
        }
        private byte[] _arrayForTimeIntervals;
        private bool _isPlayingMusicAllowed = Models.TimerSettings.IsPlayingMusicAllowed;
        public bool IsPlayingMusicAllowed
        {
            get
            {
                return _isPlayingMusicAllowed;
            }
            set
            {
                _isPlayingMusicAllowed = value;
                OnPropertyChanged();
            }
        }
        /*--------------------------------------------*/
        public TimerSettingsViewModel()
        {
            SetTimeIntervalsInCellsFromSettingsFile();
        }
        public DelegateCommand CloseSettingsWindow
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    TimerHandlerViewModel.ShowedSettingsWindow = false;
                    CloseSettingsWindowAction();
                });
            }
        }
        public DelegateCommand SaveDefaultSettings
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    SetDefaultTimerSettings();
                    SetStandardBorderBrushForAllTextBoxes();
                    SetNewSettings();
                });
            }
        }
        public DelegateCommand SaveSettings
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    if (!IsInputForTimerCorrect())
                        System.Windows.MessageBox.Show("Временные интервалы должны содержать только неотрицательные числа");
                    else if (!Models.TimerSettings.IsWorkTimeIntervalInBoundaries(
                                                             byte.Parse(WorkTimeHours),
                                                             byte.Parse(WorkTimeMinutes),
                                                             byte.Parse(WorkTimeSeconds
                                                                                 )))
                    {
                        SetErrorBorderBrushForWorkTimeTextBoxes();
                        Models.TimerSettings.ShowCorrectWorkTimeMessage();
                    }
                    else if (!Models.TimerSettings.IsShortRestTimeInBoundaries(
                                                             byte.Parse(WorkTimeHours),
                                                             byte.Parse(WorkTimeMinutes),
                                                             byte.Parse(WorkTimeSeconds),
                                                             byte.Parse(ShortRestTimeHours),
                                                             byte.Parse(ShortRestTimeMinutes),
                                                             byte.Parse(ShortRestTimeSeconds)
                                                                              ))
                    {
                        SetErrorBorderBrushForShortRestTimeTextBoxes();
                        Models.TimerSettings.ShowCorrectShortRestTimeMessage();
                    }
                    else
                    {
                        SetStandardBorderBrushForAllTextBoxes();
                        SetNewSettings();
                    }
                });
            }
        }
        private void SetNewSettings()
        {
            Models.TimerSettings.SaveSettingsInFile(new byte[]
                    {
                        byte.Parse(WorkTimeHours), byte.Parse(WorkTimeMinutes), byte.Parse(WorkTimeSeconds),
                        byte.Parse(ShortRestTimeHours), byte.Parse(ShortRestTimeMinutes), byte.Parse(ShortRestTimeSeconds),
                        byte.Parse(LongRestTimeHours), byte.Parse(LongRestTimeMinutes), byte.Parse(LongRestTimeSeconds)
                    }, IsPlayingMusicAllowed);
            Models.WorkTimeDisplayerObjectContainer.newWorkTimeDisplayer.RefreshWorkTimeKeeperProperty();
            Models.TimerSettings.IsPlayingMusicAllowed = IsPlayingMusicAllowed;
        }
        private void SetDefaultTimerSettings()
        {
            _arrayForTimeIntervals = Models.TimerSettings.GetDefaultTimeSettings();
            SetTimeIntervalsForCellsInSettingsWindow();
            IsPlayingMusicAllowed = Models.TimerSettings.GetDefaultMusicSettings();
            Models.TimerSettings.IsPlayingMusicAllowed = IsPlayingMusicAllowed;
        }
        private void SetTimeIntervalsForCellsInSettingsWindow()
        {
            WorkTimeHours = _arrayForTimeIntervals[0].ToString();
            WorkTimeMinutes = _arrayForTimeIntervals[1].ToString();
            WorkTimeSeconds = _arrayForTimeIntervals[2].ToString();
            ShortRestTimeHours = _arrayForTimeIntervals[3].ToString();
            ShortRestTimeMinutes = _arrayForTimeIntervals[4].ToString();
            ShortRestTimeSeconds = _arrayForTimeIntervals[5].ToString();
            LongRestTimeHours = _arrayForTimeIntervals[6].ToString();
            LongRestTimeMinutes = _arrayForTimeIntervals[7].ToString();
            LongRestTimeSeconds = _arrayForTimeIntervals[8].ToString();
        }
        private void SetTimeIntervalsInCellsFromSettingsFile()
        {
            _arrayForTimeIntervals = new byte[9];
            Models.TimerSettings.SetConfigsFromSettings();
            ulong[] tempArray = new ulong[]
            {
                Models.TimerSettings.WorkTime,
                Models.TimerSettings.ShortRestTime,
                Models.TimerSettings.LongRestTime
            };
            byte tempHours = 0, tempMinutes = 0, tempSeconds = 0;
            for (int i = 0; i < 3; i++)
            {
                Models.TimeConverter.SplitMillisecondsToThreeTimeIntervals(tempArray[i],
                     ref tempHours, ref tempMinutes, ref tempSeconds);
                byte[] tempArrayOfIntervals = new byte[3] { tempHours, tempMinutes, tempSeconds };
                for (int j = i * 3; j < i * 3 + 3; j++)
                {
                    _arrayForTimeIntervals[j] = tempArrayOfIntervals[j % 3];
                }
            }
            SetTimeIntervalsForCellsInSettingsWindow();
        }
    }
}
