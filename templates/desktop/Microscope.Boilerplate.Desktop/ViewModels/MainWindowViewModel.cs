using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;

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
        new("Home", typeof(HomePageViewModel), MaterialIconKind.Home),
        new ("About", typeof(AboutPageViewModel), MaterialIconKind.Information),
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
