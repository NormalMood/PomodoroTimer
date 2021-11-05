using System.IO;

namespace Pomodoro_Timer.Models
{
    public class Notification : Base.BasePropertyChanged
    {
        private const uint _notificationWindowWidth = 400;
        public uint NotificationWindowWidth { get { return _notificationWindowWidth; } }
        private const uint _notificationWindowHeight = 250;
        public uint NotificationWindowHeight { get { return _notificationWindowHeight; } }

        private const string _timeToRestMessage = "Пора отдыхать";
        private const string _timeToWorkMessage = "Пора работать";
        private const string _imagesFolderName = "images";
        private const string _timeToRestImageName = "time_to_rest.jpg";
        private const string _timeToWorkImageName = "time_to_work.jpg";
        private readonly string _timeToRestImagePath = Path.GetFullPath(_imagesFolderName + "/" + _timeToRestImageName);
        private readonly string _timeToWorkImagePath = Path.GetFullPath(_imagesFolderName + "/" + _timeToWorkImageName);
        private const string _timeToRestNotificationBackgroundColor = "#53BAFF";
        private const string _timeToWorkNotificationBackgroundColor = "#FF8307";

        private string _textBlockContent;
        public string TextBlockContent
        {
            get
            {
                return _textBlockContent;
            }
            set
            {
                _textBlockContent = value;
                OnPropertyChanged();
            }
        }
        public string ImageSource { get; set; }
        public string BackgroundColor { get; set; }
        public void SetNotificationContent(bool isTimeToRest)
        {
            if (isTimeToRest)
            {
                BackgroundColor = _timeToRestNotificationBackgroundColor;
                ImageSource = _timeToRestImagePath;
                TextBlockContent = _timeToRestMessage;
            }
            else
            {
                BackgroundColor = _timeToWorkNotificationBackgroundColor;
                ImageSource = _timeToWorkImagePath;
                TextBlockContent = _timeToWorkMessage;
            }
        }
    }
}
