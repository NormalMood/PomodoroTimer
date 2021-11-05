using System;
using System.Windows.Media;

namespace Pomodoro_Timer.Models
{
    public class TimerMusicPlayer : Base.BasePropertyChanged
    {
        private const string _musicFolderPath = "music\\";
        private const string _restMusicName = "rest_time.mp3";
        private const string _workMusicName = "work_time.mp3";
        private MediaPlayer mediaPlayer;


        public void PlayNotificationMusic(bool isRestMusicOn)
        {
            if (TimerSettings.IsPlayingMusicAllowed)
            {
                mediaPlayer = new MediaPlayer();
                if (isRestMusicOn)
                    mediaPlayer.Open(new Uri(_musicFolderPath + _restMusicName, UriKind.Relative));
                else
                    mediaPlayer.Open(new Uri(_musicFolderPath + _workMusicName, UriKind.Relative));
                mediaPlayer.Play();
            }
        }
    }
}
