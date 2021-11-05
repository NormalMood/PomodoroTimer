using System;

namespace Pomodoro_Timer.ViewModels
{
    public class NotificationViewModel : Base.BasePropertyChanged
    {
       
        public string TextBlockContent { get { return Models.NotificationObjectContainer.notification.TextBlockContent; } }
        public string ImageContentSource { get { return Models.NotificationObjectContainer.notification.ImageSource; } }
        public string BackgroundColor { get { return Models.NotificationObjectContainer.notification.BackgroundColor; } }
        public uint NotificationWindowWidth { get { return Models.NotificationObjectContainer.notification.NotificationWindowWidth; } set { } }
        public uint NotificationWindowHeight { get { return Models.NotificationObjectContainer.notification.NotificationWindowHeight; } set { } }
        public string NotificationWindowCloseImageSource { get { return Models.TopPanel.CloseImageFullPath; } }
        public Action CloseWindowAction { get; set; }
        public DelegateCommand CloseNotificationWindow
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    CloseWindowAction();
                });
            }
        }
    }
}
