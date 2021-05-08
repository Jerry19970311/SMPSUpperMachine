using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private ConcurrentQueue<string> messages = new ConcurrentQueue<string>();
        private void MQMonitor()
        {
            while (true)
            {
                if (mutex)
                {
                    if (0 != messages.Count)
                    {
                    }
                }
            }
        }
    }
}
