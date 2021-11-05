using System;
using System.Windows;
using System.Windows.Input;

namespace Pomodoro_Timer.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            ViewModels.TimerSettingsViewModel timerSettingsViewModel = new ViewModels.TimerSettingsViewModel();
            this.DataContext = timerSettingsViewModel;
            if (timerSettingsViewModel.CloseSettingsWindowAction == null)
                timerSettingsViewModel.CloseSettingsWindowAction = new Action(this.Close);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
