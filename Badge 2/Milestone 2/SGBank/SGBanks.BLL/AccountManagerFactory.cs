using SGBank.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SGBank.BLL
{
    public static class AccountManagerFactory
    {
        public static AccountManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString(); // AppSettings is a Dictionary

            switch (mode)
            {
                case "FreeTest":
                    return new AccountManager(new FreeAccountTestRepository());
                case "BasicTest":
                    return new AccountManager(new BasicAccountTestRepository());
                case "PremiumTest":
                    return new AccountManager(new PremiumAccountTestRepository());
                case "FileTest":
                    return new AccountManager(new FileAccountRepository());
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
