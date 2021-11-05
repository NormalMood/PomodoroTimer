using System.IO;

namespace Pomodoro_Timer.Models
{
    public static class TopPanel
    {
        private const string _minimizeImagePath = "images//minimize.jpg";
        private static readonly string _minimizeImageFullPath = Path.GetFullPath(_minimizeImagePath);
        public static string MinimizeImageFullPath { get { return _minimizeImageFullPath; } }

        private const string _closeImagePath = "images//close.jpg";
        private static readonly string _closeImageFullPath = Path.GetFullPath(_closeImagePath);
        public static string CloseImageFullPath { get { return _closeImageFullPath; } }

        private const string _pomodoroTimerIconImagePath = "images//pomodoro_icon.png";
        private static readonly string _pomodoroTimerIconImageFullPath = Path.GetFullPath(_pomodoroTimerIconImagePath);
        public static string PomodoroTimerIconImageFullPath { get { return _pomodoroTimerIconImageFullPath; } }
    }
}
