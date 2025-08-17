using SortVisualizer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SortVisualizer.Services
{
    public static class AlgorithmInfoProvider
    {
        private static List<AlgorithmInfo>? _allAlgorithms;
        private static readonly string _path = Path.Combine(AppContext.BaseDirectory, "Assets", "Data", "algorithm_info.json");

        static AlgorithmInfoProvider()
        {
            LoadFromFile();
        }

        private static void LoadFromFile()
        {
            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };

                _allAlgorithms = JsonSerializer.Deserialize<List<AlgorithmInfo>>(json, options);
            }
            else
            {
                _allAlgorithms = new List<AlgorithmInfo>();
            }
        }

        public static AlgorithmInfo Get(string id)
        {
            return _allAlgorithms?.Find(a => a.Id == id) ?? new AlgorithmInfo { Id = "Unknown" };
        }

        public static IEnumerable<AlgorithmInfo> GetAll()
        {
            return _allAlgorithms ?? new List<AlgorithmInfo>();
        }
    }
}
