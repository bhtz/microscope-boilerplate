using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microscope.Boilerplate.Desktop.Services;
using Microscope.Boilerplate.Desktop.ViewModels;
using Microscope.Boilerplate.Desktop.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Microscope.Boilerplate.Desktop;

public partial class App : Application
{
    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;
    
    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; private set; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public void ConfigureServices()
    {
        // If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        collection.AddScoped<ISampleService, SampleService>();
        collection.AddScoped<MainWindowViewModel>();
        collection.AddScoped<HomePageViewModel>();
        collection.AddScoped<CounterPageViewModel>();
        
        Services = collection.BuildServiceProvider();
            
        Ioc.Default.ConfigureServices(Services);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ConfigureServices();
            var vm = Ioc.Default.GetService<MainWindowViewModel>();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}