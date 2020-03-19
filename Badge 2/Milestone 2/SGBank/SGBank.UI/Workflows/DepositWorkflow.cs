using SGBank.BLL;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class DepositWorkflow
    {
        public void Execute()
        {
            AccountManager accountManager = AccountManagerFactory.Create();
            string accountNumber = ConsoleIO.GetAccountNumber();
            decimal amount = ConsoleIO.GetDecimalInput("Enter a deposit amount: ");

            AccountDepositResponse response = accountManager.Deposit(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("Deposit completed!");
                Console.WriteLine($"Account number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount deposited: {response.Amount:c}");
                Console.WriteLine($"New balance: {response.Account.Balance:c}");
            }
            else
            {
                Console.WriteLine("An error occured: ");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
