using TickerQ.Utilities.Base;

namespace Microscope.Boilerplate.Scheduler.Jobs;

public class SampleTimeJob
{
    [TickerFunction(nameof(SampleTimeJob))]
    public void MyTimeFunction()
    {
        Console.WriteLine("Hello time function !");
    }
}