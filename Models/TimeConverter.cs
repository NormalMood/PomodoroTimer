
namespace Pomodoro_Timer.Models
{
    public static class TimeConverter
    {
        private const uint _millisecondsInOneSecond = 1000;
        public static ulong ConvertToMilliSeconds(byte hours, byte minutes, byte seconds) =>
            (ulong)(hours * 60 * 60 + minutes * 60 + seconds) * 1000;
        public static void SplitMillisecondsToThreeTimeIntervals(ulong milliseconds, ref byte hours, ref byte minutes, ref byte seconds)
        {
            hours = (byte)(milliseconds / _millisecondsInOneSecond / 60 / 60);
            minutes = (byte)(((milliseconds / _millisecondsInOneSecond) % (60 * 60)) / 60);
            seconds = (byte)((milliseconds / _millisecondsInOneSecond) % (60 * 60) % 60);
        }
    }
}
