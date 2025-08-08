using System;
using SortVisualizer.ViewModels;
using SortVisualizer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace SortVisualizer.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            collection.AddTransient<ISortItemGenerator, SortItemGeneratorService>();
            collection.AddSingleton<ISortManagerService, SortManagerService>();

            // Player factory
            collection.AddTransient<Func<SortAlgorithm, PlayerViewModel>>(provider =>
                algorithm => new PlayerViewModel(
                    provider.GetRequiredService<ISortManagerService>(),
                    algorithm));

            // Visualization factory
            collection.AddTransient<Func<SortAlgorithm, VisualizationViewModel>>(provider =>
                algorithm => new VisualizationViewModel(
                    provider.GetRequiredService<Func<SortAlgorithm, PlayerViewModel>>(),
                    algorithm));
        }
    }
}
