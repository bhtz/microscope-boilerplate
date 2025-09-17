using TickerQ.Utilities.Base;

namespace Microscope.Boilerplate.Scheduler.Jobs;

public class SampleCronJob
{
    /// <summary>
    /// Execute every minute
    /// </summary>
    [TickerFunction(nameof(MyCronFunction),"*/1 * * * *")]
    public void MyCronFunction()
    {
        // Your background job logic goes here...
        Console.WriteLine("Hello cron function !");
    }
}