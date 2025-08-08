using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SortVisualizer.Services;
using SortVisualizer.Views;
using SortVisualizer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace SortVisualizer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {

        // If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        // Setup DI container
        var collection = new ServiceCollection();
        collection.AddCommonServices();

        // Register ViewModels
        collection.AddSingleton<MainViewModel>();

        var services = collection.BuildServiceProvider();

        var mainViewModel = services.GetRequiredService<MainViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel,
                Content = new MainView(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}