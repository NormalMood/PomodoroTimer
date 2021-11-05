using System;
using System.IO;

namespace Pomodoro_Timer.Models
{
    public static class TimerSettings
    {
        private const string _settingsFileName = "settings.set";
        private const uint _settingsWindowHeight = 250;
        public static uint SettingsWindowHeight { get { return _settingsWindowHeight; } }
        private const uint _settingsWindowWidth = 430;
        public static uint SettingsWindowWidth { get { return _settingsWindowWidth; } }
        private static class DefaultSettings
        {
            public static byte workTimeHours = 0;
            public static byte workTimeMinutes = 25;
            public static byte workTimeSeconds = 0;

            public static byte shortRestTimeHours = 0;
            public static byte shortRestTimeMinutes = 5;
            public static byte shortRestTimeSeconds = 0;

            public static byte longRestTimeHours = 0;
            public static byte longRestTimeMinutes = 20;
            public static byte longRestTimeSeconds = 0;

            public static bool playMusicIsOn = true;
        }
        private static class TimeBoundaries
        {
            public const byte minimumWorkTimeHours = 0;
            public const byte minimumWorkTimeMinutes = 10;
            public const byte minimumWorkTimeSeconds = 0;
            public const byte maximumWorkTimeHours = 1;
            public const byte maximumWorkTimeMinutes = 40;
            public const byte maximumWorkTimeSeconds = 0;
            public static ulong LowestBoundaryForWorkTime
            {
                get
                {
                    return TimeConverter.ConvertToMilliSeconds(
                                                              minimumWorkTimeHours,
                                                              minimumWorkTimeMinutes,
                                                              minimumWorkTimeSeconds
                                                              );
                }
            }
            public static ulong HighestBoundaryForWorkTime
            {
                get
                {
                    return TimeConverter.ConvertToMilliSeconds(
                                                              maximumWorkTimeHours,
                                                              maximumWorkTimeMinutes,
                                                              maximumWorkTimeSeconds
                                                              );
                }
            }
            public static string LowestBoundaryForWorkTimeString
            {
                get
                {
                    return minimumWorkTimeHours.ToString().PadLeft(2, '0') + ":" +
                           minimumWorkTimeMinutes.ToString().PadLeft(2, '0') + ":" +
                           minimumWorkTimeSeconds.ToString().PadLeft(2, '0');
                }
            }
            public static string HighestBoundaryForWorkTimeString
            {
                get
                {
                    return maximumWorkTimeHours.ToString().PadLeft(2, '0') + ":" + 
                           maximumWorkTimeMinutes.ToString().PadLeft(2, '0') + ":" + 
                           maximumWorkTimeSeconds.ToString().PadLeft(2, '0');
                }
            }
            public static ulong CurrentCorrectLowShortRestTime { get; set; }
            public static ulong CurrentCorrectHighShortRestTime { get; set; }
            public const double minShortRestTimePercentage = 10;
            public const double maximumShortRestTimePercentage = 30;

        }
        private static ulong _workTime;
        public static ulong WorkTime { get { return _workTime; } }
        private static ulong _shortRestTime;
        public static ulong ShortRestTime { get { return _shortRestTime; } }
        private static ulong _longRestTime;
        public static ulong LongRestTime { get { return _longRestTime; } }
        private static bool _isPlayingMusicAllowed;
        public static bool IsPlayingMusicAllowed
        {
            get
            {
                return _isPlayingMusicAllowed;
            }
            set
            {
                _isPlayingMusicAllowed = value;
            }
        }
        public static void SetConfigsFromSettings()
        {
            StreamReader settingsFile = new StreamReader(_settingsFileName);
            _workTime = ulong.Parse(settingsFile.ReadLine());
            _shortRestTime = ulong.Parse(settingsFile.ReadLine());
            _longRestTime = ulong.Parse(settingsFile.ReadLine());
            _isPlayingMusicAllowed = bool.Parse(settingsFile.ReadLine());
            settingsFile.Close();
        }
        public static ulong GetWorkTimeInMillisecondsFromSettings()
        {
            StreamReader settingsFile = new StreamReader(_settingsFileName);
            ulong milliseconds = ulong.Parse(settingsFile.ReadLine());
            settingsFile.Close();
            return milliseconds;
        }
        public static void SaveSettingsInFile(byte[] timeIntervals, bool isMusicOn) //(hours, minutes, seconds) X 3
        {
            StreamWriter settingsFile = new StreamWriter(_settingsFileName);
            settingsFile.WriteLine(TimeConverter.ConvertToMilliSeconds(timeIntervals[0], timeIntervals[1], timeIntervals[2]));
            settingsFile.WriteLine(TimeConverter.ConvertToMilliSeconds(timeIntervals[3], timeIntervals[4], timeIntervals[5]));
            settingsFile.WriteLine(TimeConverter.ConvertToMilliSeconds(timeIntervals[6], timeIntervals[7], timeIntervals[8]));
            settingsFile.WriteLine(isMusicOn);
            settingsFile.Close();
        }
        public static bool IsWorkTimeIntervalInBoundaries(byte workHours, byte workMinutes, byte workSeconds)
        {
            ulong typedWorkTime = TimeConverter.ConvertToMilliSeconds(workHours, workMinutes, workSeconds);
            return (typedWorkTime >= TimeBoundaries.LowestBoundaryForWorkTime) && 
                    (typedWorkTime <= TimeBoundaries.HighestBoundaryForWorkTime);
        }
        public static void ShowCorrectWorkTimeMessage()
        {
            System.Windows.MessageBox.Show("Длительность одного \"помидора\" должна находиться в интервале\n" +
                $"от {TimeBoundaries.LowestBoundaryForWorkTimeString} до {TimeBoundaries.HighestBoundaryForWorkTimeString}");
        }
        public static bool IsShortRestTimeInBoundaries(byte workHours, byte workMinutes, byte workSeconds,
                                                       byte restHours, byte restMinutes, byte restSeconds)
        {
            ulong lowestShortRestTimeForCurrentWorkTime = 
                (ulong)Math.Ceiling(TimeConverter.ConvertToMilliSeconds(workHours, workMinutes, workSeconds) * 
                (TimeBoundaries.minShortRestTimePercentage / 100));
            TimeBoundaries.CurrentCorrectLowShortRestTime = lowestShortRestTimeForCurrentWorkTime;
            ulong highestShortRestTimeForCurrentWorkTime =
                (ulong)Math.Ceiling(TimeConverter.ConvertToMilliSeconds(workHours, workMinutes, workSeconds) *
                (TimeBoundaries.maximumShortRestTimePercentage / 100));
            TimeBoundaries.CurrentCorrectHighShortRestTime = highestShortRestTimeForCurrentWorkTime;
            ulong currentRestTime = TimeConverter.ConvertToMilliSeconds(restHours, restMinutes, restSeconds);
            return (currentRestTime >= lowestShortRestTimeForCurrentWorkTime) && 
                (currentRestTime <= highestShortRestTimeForCurrentWorkTime);
        }
        private static string GetLowShortRestInterval()
        {
            byte restHours = 0, restMinutes = 0, restSeconds = 0;
            TimeConverter.SplitMillisecondsToThreeTimeIntervals(TimeBoundaries.CurrentCorrectLowShortRestTime,
                ref restHours, ref restMinutes, ref restSeconds);
            return restHours.ToString().PadLeft(2, '0') + ":" +
                   restMinutes.ToString().PadLeft(2, '0') + ":" +
                   restSeconds.ToString().PadLeft(2, '0');
        }
        private static string GetHighShortRestInterval()
        {
            byte restHours = 0, restMinutes = 0, restSeconds = 0;
            TimeConverter.SplitMillisecondsToThreeTimeIntervals(TimeBoundaries.CurrentCorrectHighShortRestTime,
                ref restHours, ref restMinutes, ref restSeconds);
            return restHours.ToString().PadLeft(2, '0') + ":" +
                   restMinutes.ToString().PadLeft(2, '0') + ":" +
                   restSeconds.ToString().PadLeft(2, '0');
        }
        public static void ShowCorrectShortRestTimeMessage()
        {
            System.Windows.MessageBox.Show("При текущей длительности \"помидора\" время короткого отдыха должно находиться в интервале\n" + 
                $"от  {GetLowShortRestInterval()} до {GetHighShortRestInterval()}");
        }
        public static byte[] GetDefaultTimeSettings()
        {
            return new byte[] 
            {
                DefaultSettings.workTimeHours, DefaultSettings.workTimeMinutes, DefaultSettings.workTimeSeconds,
                DefaultSettings.shortRestTimeHours, DefaultSettings.shortRestTimeMinutes, DefaultSettings.shortRestTimeSeconds,
                DefaultSettings.longRestTimeHours, DefaultSettings.longRestTimeMinutes, DefaultSettings.longRestTimeSeconds
            };
        }
        public static bool GetDefaultMusicSettings() =>
            DefaultSettings.playMusicIsOn;
    }
}
