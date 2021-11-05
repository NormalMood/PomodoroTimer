using System.Collections.Generic;
using System.Threading;

namespace Pomodoro_Timer.Models
{
    public static class ThreadsContainer
    {
        private static List<Thread> _listOfThreads = new List<Thread>();
        public static void AddThreadInThreadsContainer(Thread thread)
        {
            _listOfThreads.Add(thread);
        }
        public static void CloseAllThreads()
        {
            foreach(var thread in _listOfThreads)
            {
                thread.Abort();
            }
        }
    }
}
