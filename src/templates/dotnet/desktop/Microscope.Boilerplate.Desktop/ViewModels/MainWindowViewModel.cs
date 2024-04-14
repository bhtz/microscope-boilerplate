using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty] 
    private ViewModelBase _currentPage;

    [ObservableProperty] 
    private SideMenuItem? _selectedSideMenuItem;

    public MainWindowViewModel()
    {
        _currentPage = App.Current.Services.GetRequiredService<HomePageViewModel>();
    }
    
    [RelayCommand]
    public void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    partial void OnSelectedSideMenuItemChanged(SideMenuItem? value)
    {
        if (value is null) return;
        
        var instance = App.Current.Services.GetService(value.ModelType);
    
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }
    
    public ObservableCollection<SideMenuItem> MenuItems { get; } = new ObservableCollection<SideMenuItem>()
    {
        new("Home", typeof(HomePageViewModel), MaterialIconKind.Home),
        new ("Counter", typeof(CounterPageViewModel), MaterialIconKind.Add),
    };
}

public class SideMenuItem
{
    public SideMenuItem(string label, Type type, Material.Icons.MaterialIconKind iconKind)
    {
        Label = label;
        ModelType = type;
        IconKind = iconKind;
    }

    public string Label { get; set; }
    public Type ModelType { get; set; }
    public MaterialIconKind IconKind { get; set; }
}
