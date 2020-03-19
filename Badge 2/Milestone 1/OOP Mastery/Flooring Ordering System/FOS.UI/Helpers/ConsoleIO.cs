using Flooring_Ordering_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FOS.Models.Helpers
{
    public class ConsoleIO
    {
        public static int GetIntNumber(string prompt)
        {
            int number;
            while (true)
            {
                Console.WriteLine(prompt);
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

        public static decimal GetDecimalNumber()
        {
            decimal number;
            while (true)
            {
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out number))
                {
                    break; // output has value 
                }
                else
                {
                    Console.WriteLine("That was not a decimal number!");
                }
            }
            return number;
        }

        public static DateTime GetListDate(string prompt)
        {
            DateTime today = DateTime.Now;
            DateTime date;
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out date))
                {
                    break; // output has value 
                }
                else
                {
                    Console.WriteLine($"Error: not a correct Date format, Ex: {today.AddDays(1).ToString("MMM dd, yyyy")}");
                }
            }
            return date;
        }
        public static DateTime GetDate(string prompt)
        {
            DateTime today = DateTime.Now;
            DateTime date;
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out date) && date > today)
                {
                    break; // output has value 
                }
                else
                {
                    Console.WriteLine($"Error: not a correct Date format or date is not in the future. Ex: {today.AddDays(1).ToString("MMM dd, yyyy")}");
                }
            }
            return date;
        }

        public static string GetCustomerName(string prompt)
        {
            Regex r = new Regex("a-zA-Z0-9");
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)&& !r.IsMatch(input))
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static string GetState(TaxRepository taxRepo, string prompt)
        {
            bool isValidState = false;
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine().ToUpper();
                foreach (var tax in taxRepo.taxes)
                {
                    if (tax.StateAbbreviation == input)
                    {
                        isValidState = true;
                    }
                }
                if (string.IsNullOrEmpty(input) || (input.Length < 2|| input.Length > 2) || !isValidState)
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static string GetProductType(ProductRepository prodRepo, string prompt)
        {
            bool isValidProduct = false;
            string input; 
            string[] prodTypesArr = new string[prodRepo.products.Count];
            
            while (true)
            {
                int count = 0;
                Console.Clear();
                foreach (var prod in prodRepo.products)
                {
                    Console.WriteLine("Product Type: " + prod.ProductType + "\t" + "LaborCostPerSqFoot: " + prod.LaborCostPerSquareFoot + "\t" + "Cost Per Sq Foot: " + prod.CostPerSquareFoot);
                    prodTypesArr[count] = prod.ProductType.ToLower().ToString();
                    count++;
                }
                Console.WriteLine(prompt);
                input = Console.ReadLine().ToLower();
                if (prodTypesArr.Contains(input.ToLower()))
                {
                    isValidProduct = true;
                }
                if (string.IsNullOrEmpty(input) || !isValidProduct)
                {
                    Console.WriteLine("You must enter valid text.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }
        public static decimal GetDecimalArea(string prompt)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            decimal number;
            while (true)
            {
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out number) && number >= 100.00M)
                {
                    break; // output has value 
                }
                else
                {
                    Console.WriteLine("Must be a decimal number and a minimum of 100 SqFt!");
                }
            }
            return number;
        }
    }
}
