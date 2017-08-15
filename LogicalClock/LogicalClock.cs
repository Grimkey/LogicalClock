namespace LogicalClock
{
    using System.Threading;
    
    public interface IClock
    {
        long Tick();
        long Sync(long syncTick);
    }

    public sealed class LogicalClock : IClock
    {
        private readonly object counterSync = new object();
        private long counter = 1;

        public long Tick()
        {
            lock (counterSync)
            {
                return Interlocked.Increment(ref counter);
            }
        }

        public long Sync(long syncTick)
        {
            lock (counterSync)
            {
                return counter >= syncTick
                    ? Interlocked.Exchange(ref counter, syncTick)
                    : counter;
            }
        }
    }
}