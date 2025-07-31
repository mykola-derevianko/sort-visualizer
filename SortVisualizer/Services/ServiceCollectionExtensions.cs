using SortVisualizer.Sorting;
using SortVisualizer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using SortVisualizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
