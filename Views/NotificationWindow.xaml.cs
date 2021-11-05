using System;
using System.Windows;
using System.Windows.Input;

namespace Pomodoro_Timer.Views
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow()
        {
            InitializeComponent();
            ViewModels.NotificationViewModel notificationViewModel = new ViewModels.NotificationViewModel();
            this.DataContext = notificationViewModel;
            if (notificationViewModel.CloseWindowAction == null)
                notificationViewModel.CloseWindowAction = new Action(this.Close);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
