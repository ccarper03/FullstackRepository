using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGBank.Models.Interfaces
{
    public interface IDeposit
    {
        AccountDepositResponse Deposit(Account account, decimal amount);
    }
}
