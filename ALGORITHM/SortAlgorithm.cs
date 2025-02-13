using System;
using System.Diagnostics;

namespace AlgorithmTest
{
    public class SortAlgorithm
    {
        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="arr"></param>
        private static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }

        // 代码说明：
        // swapped 标志：用于检测内层循环是否发生了交换。如果没有发生交换，说明数组已经有序，可以提前退出循环，减少不必要的比较。
        // 内层循环优化：每次外层循环后，最大的元素会被移动到正确的位置，因此内层循环的次数可以减少 i 次。

        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="arr"></param>
        private static void SelectionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    int temp = arr[minIndex];
                    arr[minIndex] = arr[i];
                    arr[i] = temp;
                }
            }
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="arr"></param>
        private static void InsertionSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 1; i < n; i++)
            {
                int curr = arr[i]; // 当前插入的元素
                int j = i - 1;

                while (j >= 0 && arr[j] > curr)  // 往后移动
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = curr; //插入当前元素
            }
        }

        /// <summary>
        /// 希尔排序
        /// </summary>
        /// <param name="arr"></param>
        private static void ShellSort(int[] arr)
        {
            int n = arr.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = arr[i];
                    int j;

                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                    {
                        arr[j] = arr[j - gap];
                    }

                    arr[j] = temp;
                }
            }
        }

        /// <summary>
        /// 归并排序
        /// </summary>
        /// <param name="arr"></param>
        private static void MergeSort(int[] arr)
        {
            if (arr.Length <= 1)
                return;

            int mid = arr.Length / 2;

            // 分隔数组
            int[] left_arr = new int[mid];
            int[] right_arr = new int[arr.Length - mid];

            Array.Copy(arr, 0, left_arr, 0, mid);
            Array.Copy(arr, mid, right_arr, 0, arr.Length - mid);

            // 递归排序
            MergeSort(left_arr);
            MergeSort(right_arr);
            // 合并
            Merge(arr, left_arr, right_arr);
        }

        private static void Merge(int[] arr, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;
            // 合并两个有序数组
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            // 将数组剩余元素拷贝到数组中
            while (i < left.Length)
            {
                arr[k] = left[i];
                i++;
                k++;
            }
            while (j < right.Length)
            {
                arr[k] = right[j];
                j++;
                k++;
            }
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="n"></param>
        private static void TestSort(int n, Action<int[]> action)
        {
            // 创建一个随机数组
            Random rand = new();
            int[] arr = new int[n];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(1, n);
            }

            // 获取当前进程
            Process process = Process.GetCurrentProcess();
            // 记录初始内存占用
            long memoryBefore = process.WorkingSet64;

            // 创建 Stopwatch 实例
            Stopwatch stopwatch = new();
            // 开始计时
            stopwatch.Start();
            // 调用排序算法
            action(arr);
            // 停止计时
            stopwatch.Stop();

            // 记录排序后的内存占用
            long memoryAfter = process.WorkingSet64;

            Console.WriteLine($"排序完成，数组规模 = {n}");
            // 输出运行时间
            Console.WriteLine($"耗时: {stopwatch.ElapsedMilliseconds} 毫秒");
            // 输出内存占用
            Console.WriteLine($"排序前内存占用: {memoryBefore / 1024} KB");
            Console.WriteLine($"排序后内存占用: {memoryAfter / 1024} KB");
            Console.WriteLine($"内存增加: {(memoryAfter - memoryBefore) / 1024} KB");
        }

        /// <summary>
        /// 测试冒泡排序
        /// </summary>
        /// <param name="n">数组规模大小</param>
        public static void TestBubbleSort(int n)
        {
            TestSort(n, BubbleSort);
        }

        /// <summary>
        /// 测试选择排序
        /// </summary>
        /// <param name="n"></param>
        public static void TestSelectionSort(int n)
        {
            TestSort(n, SelectionSort);
        }

        /// <summary>
        /// 测试插入排序
        /// </summary>
        /// <param name="n"></param>
        public static void TestInsertionSort(int n)
        {
            TestSort(n, InsertionSort);
        }

        /// <summary>
        /// 测试希尔排序
        /// </summary>
        /// <param name="n"></param>
        public static void TestShellSort(int n)
        {
            TestSort(n, ShellSort);
        }

        /// <summary>
        /// 测试归并排序
        /// </summary>
        /// <param name="n"></param>
        public static void TestMergeSort(int n)
        {
            TestSort(n, MergeSort);
        }
    }
}