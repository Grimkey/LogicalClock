using System;
using System.Threading;
using System.Threading.Tasks;

namespace LogicalClock
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait(5000);
        }

        static async Task MainAsync(string[] args)
        {
            var logicalClock = new LogicalClock();
            var eventGenerator = new EventGenerator(new Random(), logicalClock);

            var cancellation = new CancellationToken();

            await eventGenerator.Start("Test", cancellation);
        }
    }
}