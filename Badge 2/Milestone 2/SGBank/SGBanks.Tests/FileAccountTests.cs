using NUnit.Framework;
using SGBank.BLL;
using SGBank.Models;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBanks.Tests
{
    [TestFixture]
    public class FileAccountTests
    {
        [Test]
        public void CanLoadFileFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();
            AccountLookupResponse response = manager.LookupAccount("11111");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("11111", response.Account.AccountNumber);
        }
       
        [Test]
        public void CanLoadFileBasicAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();
            AccountLookupResponse response = manager.LookupAccount("22222");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("22222", response.Account.AccountNumber);
        }

        [Test]
        public void CanLoadFilePremiumAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();
            AccountLookupResponse response = manager.LookupAccount("33333");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("33333", response.Account.AccountNumber);
        }
    }
}
