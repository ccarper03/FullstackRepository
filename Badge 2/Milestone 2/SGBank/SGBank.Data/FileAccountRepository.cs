using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private const string path = @"C:\Users\ccarp\My Cloud\tsg bootcamp\online-net-ccarper03\Summatives\Badge 2\Milestone 2\SGBank\SGBank.Data\Accounts.txt";
        private List<Account> accounts = new List<Account>();
        
        public Account LoadAccount(string accountNumber)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Account account = new Account();
                    string[] columns = line.Split(',');
                    account.AccountNumber = columns[0];
                    account.Name = columns[1];
                    account.Balance = decimal.Parse(columns[2]);
                    switch (columns[3].ToLower())
                    {
                        case "f":
                            account.Type = AccountType.Free;
                            break;
                        case "b":
                            account.Type = AccountType.Basic;
                            break;
                        case "p":
                            account.Type = AccountType.Premium;
                            break;
                        default:
                            throw new Exception($"There is no {columns[3]} account type.");
                    }
                    accounts.Add(account);
                }

                foreach (var _account in accounts)
                {
                    if (accountNumber == _account.AccountNumber)
                    {
                        return _account;
                    }
                }
            }
            return null;
        }
        public void SaveAccount(Account account)
        {
            string typeString = "";

            accounts[accounts.FindIndex(x => x.AccountNumber.Equals(account.AccountNumber))].Balance = account.Balance;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter sr = new StreamWriter(path))
            {
                sr.WriteLine("AccountNumber,Name,Balance,Type");
                foreach (var ac in accounts)
                {
                    switch (ac.Type)
                    {
                        case AccountType.Free:
                            typeString = "F";
                            break;
                        case AccountType.Basic:
                            typeString ="B";
                            break;
                        case AccountType.Premium:
                            typeString = "P";
                            break;
                    }
                    sr.WriteLine(string.Format("{0},{1},{2},{3}", ac.AccountNumber, ac.Name, ac.Balance, typeString));
                }
            }
        }
    }
}
