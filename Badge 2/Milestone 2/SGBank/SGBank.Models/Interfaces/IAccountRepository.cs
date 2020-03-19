using System;
using System.Collections.Generic;
using System.Text;

namespace SGBank.Models.Interfaces
{
    public interface IAccountRepository
    {
        Account LoadAccount(string accountNumber);
        void SaveAccount(Account account);
    }
}
