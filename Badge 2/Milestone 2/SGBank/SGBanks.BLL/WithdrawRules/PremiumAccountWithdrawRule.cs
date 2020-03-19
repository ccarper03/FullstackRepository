using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBanks.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        AccountWithdrawResponse response = new AccountWithdrawResponse();
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: a non-premium account hit the Premium Withdraw Rule. Contact IT”.";
                return response;
            }

            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative.";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.Amount = amount;
            if (account.Balance < -500) // Remove 10 for overdraft fee
            {
                account.Balance += -10;
            }
            response.Success = true;
            return response;
        }
    }
}
