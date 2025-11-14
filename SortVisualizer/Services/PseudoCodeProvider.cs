using SortVisualizer.Models;
using System.Collections.Generic;

namespace SortVisualizer.Services
{
    public static class PseudoCodeProvider
    {
        public static List<PseudoCodeLine> GetPseudoCode(SortAlgorithm algorithm)
        {
            return algorithm switch
            {
                SortAlgorithm.BubbleSort => GetBubbleSortPseudoCode(),
                SortAlgorithm.SelectionSort => GetSelectionSortPseudoCode(),
                SortAlgorithm.InsertionSort => GetInsertionSortPseudoCode(),
                SortAlgorithm.QuickSort => GetQuickSortPseudoCode(),
                _ => new List<PseudoCodeLine>()
            };
        }

        private static List<PseudoCodeLine> GetBubbleSortPseudoCode()
        {
            return new List<PseudoCodeLine>
            {
                new("for i = 0 to n-2", 0),           // Line 0
                new("swapped = false", 1),            // Line 1
                new("for j = 0 to n-i-2", 1),        // Line 2
                new("if arr[j] > arr[j+1]", 2),      // Line 3
                new("swap(arr[j], arr[j+1])", 3),    // Line 4
                new("swapped = true", 3),             // Line 5
                new("if not swapped", 1),             // Line 6
                new("break", 2)                       // Line 7
            };
        }

        private static List<PseudoCodeLine> GetSelectionSortPseudoCode()
        {
            return new List<PseudoCodeLine>
            {
                new("for i = 0 to n-2", 0),          // Line 0
                new("minIndex = i", 1),               // Line 1
                new("for j = i+1 to n-1", 1),        // Line 2
                new("if arr[j] < arr[minIndex]", 2), // Line 3
                new("minIndex = j", 3),               // Line 4
                new("if minIndex != i", 1),          // Line 5
                new("swap(arr[i], arr[minIndex])", 2) // Line 6
            };
        }

        private static List<PseudoCodeLine> GetInsertionSortPseudoCode()
        {
            return new List<PseudoCodeLine>
            {
                new("for i = 1 to n-1", 0),          // Line 0
                new("key = arr[i]", 1),               // Line 1
                new("j = i - 1", 1),                  // Line 2
                new("while j >= 0 and arr[j] > key", 1), // Line 3
                new("arr[j+1] = arr[j]", 2),          // Line 4
                new("j = j - 1", 2),                  // Line 5
                new("arr[j+1] = key", 1)              // Line 6
            };
        }

        private static List<PseudoCodeLine> GetQuickSortPseudoCode()
        {
            return new List<PseudoCodeLine>
            {
                new("if low < high", 0),              // Line 0
                new("pivot = partition(arr, low, high)", 1), // Line 1
                new("quickSort(arr, low, pivot-1)", 1),      // Line 2
                new("quickSort(arr, pivot+1, high)", 1),     // Line 3
                new("-- Partition --", 0),            // Line 4
                new("pivot = arr[high]", 1),          // Line 5
                new("i = low - 1", 1),                // Line 6
                new("for j = low to high-1", 1),      // Line 7
                new("if arr[j] <= pivot", 2),         // Line 8
                new("swap(arr[i], arr[j])", 3),       // Line 9
                new("swap(arr[i+1], arr[high])", 1),  // Line 10
                new("return i + 1", 1)                // Line 11
            };
        }
    }
}