using SortVisualizer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace SortVisualizer.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            collection.AddTransient<ISortItemGenerator, SortItemGeneratorService>();
            collection.AddTransient<MainViewModel>();
        }
    }
}
