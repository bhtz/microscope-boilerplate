using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Microscope.Boilerplate.Desktop.ViewModels;

public partial class CounterPageViewModel : ViewModelBase
{
    [ObservableProperty] private int _counter = 0;

    [RelayCommand]
    public void Increment()
    {
        Counter = Counter + 1;
    }
}