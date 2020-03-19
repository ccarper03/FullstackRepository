using NUnit.Framework;
using SGBank.BLL;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using SGBanks.BLL.DepositRules;
using SGBanks.BLL.WithdrawRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBanks.Tests
{
    [TestFixture]
    public class FreeAccountTests
    {
        //[Test]
        //public void CanLoadFreeAccountTestData()
        //{
        //    AccountManager manager = AccountManagerFactory.Create();
        //    AccountLookupResponse response = manager.LookupAccount("12345");

        //    Assert.IsNotNull(response.Account);
        //    Assert.IsTrue(response.Success);
        //    Assert.AreEqual("12345",response.Account.AccountNumber); 
        //}

        [TestCase("12345", "Free Account", 100, AccountType.Free,  250,  false)]   // fail, too much deposited
        [TestCase("12345", "Free Account", 100, AccountType.Free,  -100, false)]   // fail, negative number deposited
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50,   false)]   // fail, not a free account type
        [TestCase("12345", "Free Account", 100, AccountType.Free,  50,   true)]    // success
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new FreeAccountDepositRule();
            Account account = new Account();
            
            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;
            account.Balance += amount;

            AccountDepositResponse response = deposit.Deposit(account,amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("12345", "Free Account", 100, AccountType.Free, -101, false)]   // fail, too much withdrawn
        [TestCase("12345", "Free Account", 50, AccountType.Free, -51, false)]     // fail, cannot overdraft
        [TestCase("12345", "Free Account", 100, AccountType.Free, 100, false)]    // fail, positive number withdrawn
        [TestCase("12345", "Free Account", 100, AccountType.Basic, -100, false)]  // fail, not a free account type
        [TestCase("12345", "Free Account", 100, AccountType.Free, -50, true)]     // success
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw withdraw = new FreeAccountWithdrawRule();
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;
            account.Balance += amount;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
