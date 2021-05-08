using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WindowsFormsApp1
{
    class MessageQueue
    {
        private ConcurrentQueue<string> messages = new ConcurrentQueue<string>();
        private bool mutex2 = true;
        private SerialPort serialPort;
        public MessageQueue(SerialPort serialPort)
        {
            this.serialPort = serialPort;
        }
        private void monitor()
        {
            while (true)
            {
                if (mutex2)
                {
                }
            }
        }
    }
}
