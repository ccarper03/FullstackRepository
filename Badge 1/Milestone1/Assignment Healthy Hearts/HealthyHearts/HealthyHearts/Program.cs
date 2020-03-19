using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyHearts
{
    class Program
    {
        static void Main(string[] args)
        {
            // Collect age
            Console.Write("How older are you? ");
            int number = GetNumber();

            // Calculate
            int maxHeartRate = 220 - number;
            double tgtHRMin = .5 * maxHeartRate;
            double tgtHRMax = .85 * maxHeartRate;

            // Results
            Console.WriteLine("Your maximum heart rate should be {0} beats per minute", maxHeartRate);
            Console.WriteLine($"Your target HR Zone is {tgtHRMin} - {tgtHRMax} beats per minute");
        }
        private static int GetNumber()
        {
            int number;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out number))
                {
                    break; // output has value 
                }
                else
                {
                    Console.WriteLine("That was not a whole number!");
                }
            }
            return number;
        }
    }
}
