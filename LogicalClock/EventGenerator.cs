using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicalClock
{
    public sealed class EventGenerator
    {
        private const int RandomWaits = 3;
        private readonly List<int> eventWaits;
        private readonly IClock clock;

        public EventGenerator(Random generator, IClock clock)
        {
            this.clock = clock;
            this.eventWaits = new List<int>(RandomWaits);
            this.eventWaits.AddRange(
                Enumerable.Range(0, RandomWaits)
                    .Select(noop => generator.Next(200, 1000)));
        }

        public async Task Start(string name, CancellationToken token)
        {
            var tick = 0;
            Func<int> taskDelay = () => this.eventWaits[++tick % this.eventWaits.Count];

            while(!token.IsCancellationRequested)
            {
                await Task.Delay(taskDelay());
                Log.Write(name, "concurrent", this.clock.Tick());
            }
        }
    }
}
