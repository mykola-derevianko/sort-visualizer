using Microsoft.Extensions.DependencyInjection;
using SortVisualizer.Models;
using SortVisualizer.Sorting;
using SortVisualizer.ViewModels;
using System;

namespace SortVisualizer.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {

            collection.AddTransient<ISortCommandBuilder, BubbleSortCommandBuilder>();
            collection.AddTransient<ISortCommandBuilder, QuickSortCommandBuilder>();
            collection.AddTransient<ISortCommandBuilder, InsertionSortCommandBuilder>();
            collection.AddTransient<ISortCommandBuilder, SelectionSortCommandBuilder>();
            collection.AddTransient<SortCommandBuilderFactory>();

            collection.AddTransient<SortItemGeneratorService>();

            collection.AddSingleton<SortManagerService>();
            collection.AddTransient<SettingsPopupViewModel>();
            // Player factory
            collection.AddTransient<Func<SortAlgorithm, PlayerViewModel>>(provider =>
                algorithm => new PlayerViewModel(
                    provider.GetRequiredService<SortManagerService>(),
                    provider.GetRequiredService<SettingsPopupViewModel>(),
                    algorithm));

            // Visualization factory
            collection.AddTransient<Func<SortAlgorithm, VisualizationViewModel>>(provider =>
                algorithm => new VisualizationViewModel(
                    provider.GetRequiredService<Func<SortAlgorithm, PlayerViewModel>>(),
                    algorithm));
        }
    }
}
