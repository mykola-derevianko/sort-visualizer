using System.Collections.Generic;

namespace SortVisualizer.Models
{
    public class AlgorithmInfo
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CodeSnippet { get; set; } = string.Empty;
        public List<string> Pros { get; set; } = new List<string>();
        public List<string> Cons { get; set; } = new List<string>();
        public AdditionalInfo AdditionalInfo { get; set; } = new();

    }
}
