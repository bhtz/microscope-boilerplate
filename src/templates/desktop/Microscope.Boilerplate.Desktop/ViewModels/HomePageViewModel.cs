using CommunityToolkit.Mvvm.ComponentModel;
using Microscope.Boilerplate.Desktop.Services;

namespace Microscope.Boilerplate.Desktop.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    private readonly ISampleService _sampleService;
    
    [ObservableProperty] private string _version;
    
    public HomePageViewModel(ISampleService sampleService)
    {
        _sampleService = sampleService;
        Version = sampleService.GetVersion();
    }
}