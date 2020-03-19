using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGBank.Models.Interfaces
{
    public interface IWithdraw
    {
        AccountWithdrawResponse Withdraw(Account account, decimal amount);   
    }
}
