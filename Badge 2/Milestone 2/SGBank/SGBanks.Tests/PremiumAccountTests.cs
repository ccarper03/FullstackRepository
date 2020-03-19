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
    public class PremiumAccountTests
    {
        //[Test]
        //public void CanLoadPremiumAccountTestData()
        //{
        //    AccountManager manager = AccountManagerFactory.Create();
        //    AccountLookupResponse response = manager.LookupAccount("55555");

        //    Assert.IsNotNull(response.Account);
        //    Assert.IsTrue(response.Success);
        //    Assert.AreEqual("55555", response.Account.AccountNumber);
        //}

        [TestCase("55555", "Premium Account", 100, AccountType.Free, 250, false)]     // fail, wrong account type
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, -100, false)]   // fail, negative number deposited
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, 250, true)]     // success
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;
            account.Balance += amount;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("55555", "Premium Account", 100, AccountType.Free, -100, 100, false)]        // fail, not a Premium account type
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, 100, 100, false)]        // fail, positive number withdrawn
        [TestCase("55555", "Premium Account", 250, AccountType.Premium, -100, 150, true)]         // success
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, -650, -560, true)]        // success, overdraft fee (100 - 650 - 10) = -560
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success)
            {
                Assert.AreEqual(newBalance, account.Balance);
            }
        }
    }
}
