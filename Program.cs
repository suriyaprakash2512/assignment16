using System.Diagnostics;
using System;
using System;
using System.Diagnostics;

class SortingAlgorithms
{
    static void Main()
    {
        Console.Write("Enter the array elements (comma-separated): ");
        int[] arrayToSort = Array.ConvertAll(Console.ReadLine().Split(','), int.Parse);

        Console.WriteLine("Original Array: " + string.Join(", ", arrayToSort));

        // Quicksort
        Stopwatch quickSortWatch = Stopwatch.StartNew();
        QuickSort(arrayToSort, 0, arrayToSort.Length - 1);
        quickSortWatch.Stop();

        Console.WriteLine("Sorted Array (Quicksort): " + string.Join(", ", arrayToSort));
        Console.WriteLine("Is Sorted (Quicksort): " + IsSorted(arrayToSort));
        Console.WriteLine($"Quicksort Time: {quickSortWatch.ElapsedMilliseconds.ToString("f4")} ms");

        // Merge Sort
        Console.Write("Enter another set of array elements (comma-separated): ");
        arrayToSort = Array.ConvertAll(Console.ReadLine().Split(','), int.Parse);
        Stopwatch mergeSortWatch = Stopwatch.StartNew();
        MergeSort(arrayToSort, 0, arrayToSort.Length - 1);
        mergeSortWatch.Stop();

        Console.WriteLine("Sorted Array (Merge Sort): " + string.Join(", ", arrayToSort));
        Console.WriteLine("Is Sorted (Merge Sort): " + IsSorted(arrayToSort));
        Console.WriteLine($"Merge Sort Time: {mergeSortWatch.ElapsedMilliseconds.ToString("f4")} ms");

        // Performance Analysis
        Console.WriteLine("\nPerformance Analysis:");
        PerformanceAnalysis();
        Console.ReadKey();
    }

    static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);
            QuickSort(arr, low, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, high);
        }
    }

    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    static void MergeSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int mid = (low + high) / 2;
            MergeSort(arr, low, mid);
            MergeSort(arr, mid + 1, high);
            Merge(arr, low, mid, high);
        }
    }

    static void Merge(int[] arr, int low, int mid, int high)
    {
        int n1 = mid - low + 1;
        int n2 = high - mid;

        int[] leftArray = new int[n1];
        int[] rightArray = new int[n2];

        Array.Copy(arr, low, leftArray, 0, n1);
        Array.Copy(arr, mid + 1, rightArray, 0, n2);

        int i = 0, j = 0, k = low;

        while (i < n1 && j < n2)
        {
            if (leftArray[i] <= rightArray[j])
            {
                arr[k] = leftArray[i];
                i++;
            }
            else
            {
                arr[k] = rightArray[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            arr[k] = leftArray[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            arr[k] = rightArray[j];
            j++;
            k++;
        }
    }

    static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    static bool IsSorted(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < arr[i - 1])
            {
                return false;
            }
        }
        return true;
    }

    static void PerformanceAnalysis()
    {
        int[] sizes = { 10000,30000,50000 };

        foreach (int size in sizes)
        {
            double totalElapsedTime = 0;

            for (int i = 0; i < 10; i++) // Run each sorting algorithm multiple times
            {
                int[] randomArray = GenerateRandomArray(size);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // Replace the sorting algorithm with the one you want to test (Quicksort or Merge Sort)
                QuickSort(randomArray, 0, randomArray.Length - 1);
                // or MergeSort(randomArray);

                stopwatch.Stop();
                totalElapsedTime += stopwatch.Elapsed.TotalMilliseconds;
            }

            double averageTime = totalElapsedTime / 10; // Calculate average time over multiple runs

            Console.WriteLine($"Array size: {size}, Average Time taken: {averageTime} ms");
        }
    }




    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        int[] randomArray = new int[size];
        for (int i = 0; i < size; i++)
        {
            randomArray[i] = random.Next(1, 2000); // Adjust the range as needed
        }
        return randomArray;
    }
}

