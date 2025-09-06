using System.Threading.Tasks;

namespace Microscope.Boilerplate.Desktop.Services;

public class SampleService : ISampleService
{
    public string GetVersion()
    {
        return "1.0.0";
    }
}