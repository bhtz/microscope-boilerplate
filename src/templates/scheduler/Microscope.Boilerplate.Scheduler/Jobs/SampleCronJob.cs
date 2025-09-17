using TickerQ.Utilities.Base;

namespace Microscope.Boilerplate.Scheduler.Jobs;

public class SampleJob
{
    [TickerFunction(nameof(HelloWorld),"*/1 * * * *")]
    public void HelloWorld()
    {
        // Your background job logic goes here...
        Console.WriteLine("Hello World!");
    }
}