using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummativeSums
{
    class Program
    {
        static void Main(string[] args)
        {
            // Populate arrays
            int[] arr1 = new int[] { 1, 90, -33, -55, 67, -16, 28, -55, 15 };
            int[] arr2 = new int[] { 999, -60, -77, 14, 160, 301 };
            int[] arr3 = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, -99 };

            // Calculate
            int sum1 = GetSum(arr1);
            int sum2 = GetSum(arr2);
            int sum3 = GetSum(arr3);

            // Display it
            Console.WriteLine("#1 Array Sum: " + sum1);
            Console.WriteLine("#2 Array Sum: " + sum2);
            Console.WriteLine("#3 Array Sum: " + sum3); 
        }

        private static int GetSum(int[] arr)
        {
            int num = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                num += arr[i];
            }
            return num;
        }
    }
}
