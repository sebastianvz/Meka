using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Kiosko.Helpers
{
    public class Timeout
    {
        private int _time = 0;
        private int timeoutToFinish = 1;
        public Timeout(int time)
        {
            timeoutToFinish = time;
            Thread timeOut = new Thread(Start);
            timeOut.Start();
        }

        private void Start()
        {
            while (!HasFinished())
            {
                System.Threading.Thread.Sleep(1000);
                _time += 1000;
            }

        }

        public int GetCurrent()
        {
            return _time;
        }

        public bool HasFinished()
        {
            return GetCurrent() >= timeoutToFinish;
        }
    }
}