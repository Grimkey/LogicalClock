using System;
using System.Collections.Generic;
using System.Text;

namespace LogicalClock
{
    public static class Log
    {
        public static void Write(string name, string operation, long tick)
        {
            Console.WriteLine($"[{name}:{operation}] -- {tick} ({Environment.TickCount})");
        }
    }
}
