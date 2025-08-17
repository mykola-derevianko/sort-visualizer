using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Models
{
    public class AdditionalInfo
    {
        public string Description { get; set; } = "";
        public Complexity Complexity { get; set; } = new();
    }
}
