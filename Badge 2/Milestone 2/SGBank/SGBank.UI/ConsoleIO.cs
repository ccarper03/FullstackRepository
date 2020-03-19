using SGBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public class ConsoleIO
    {
        public static void DisplayAccountDetails(Account account)
        {
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Name: {account.Name}");
            Console.WriteLine($"Balance: {account.Balance:c}");
        }

        public static decimal GetDecimalInput(string prompt)
        {
            bool keepRunning = true;
            string input = "";
            decimal number = 0.0M;

            while (keepRunning)
            {
                Console.Clear();
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }
                else
                {
                    keepRunning = !decimal.TryParse(input, out number); ;
                }
            }
            return number;
        }

        public static string GetAccountNumber()
        {
            string accountNumString = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter an account number");
                accountNumString = Console.ReadLine();

                if (!string.IsNullOrEmpty(accountNumString) && accountNumString.Length == 5)
                {
                    return accountNumString;
                }
                else
                {
                    Console.WriteLine("That was not a valid, try again! Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
