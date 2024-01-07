using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace Microscope.Boilerplate.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty] 
    private ViewModelBase _currentPage = new HomePageViewModel();
    
    [RelayCommand]
    public void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [ObservableProperty] 
    private SideMenuItem? _selectedSideMenuItem;
    
    partial void OnSelectedSideMenuItemChanged(SideMenuItem? value)
    {
        if (value is null) return;

        var instance = Activator.CreateInstance(value.ModelType);
    
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }
    
    public ObservableCollection<SideMenuItem> MenuItems { get; } = new ObservableCollection<SideMenuItem>()
    {
        new("Home", typeof(HomePageViewModel)),
        new ("About", typeof(AboutPageViewModel)),
    };
}

public class SideMenuItem(string label, Type type)
{
    public string Label { get; set; } = label;
    public Type ModelType { get; set; } = type;
}
