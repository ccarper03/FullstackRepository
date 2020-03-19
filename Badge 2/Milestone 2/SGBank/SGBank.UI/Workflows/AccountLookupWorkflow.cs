using SGBank.BLL;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class AccountLookupWorkflow
    {
        public void Execute() 
        {
            AccountManager manager = AccountManagerFactory.Create();
            string accountNumber = ConsoleIO.GetAccountNumber();

            AccountLookupResponse response = manager.LookupAccount(accountNumber);

            if (response.Success)
            {
                ConsoleIO.DisplayAccountDetails(response.Account);
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
