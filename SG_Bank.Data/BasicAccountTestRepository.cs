using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Data
{
    public class BasicAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Basic Account",
            Balance = 100.00M,
            AccountNumber = "33333",
            Type = AccountType.BASIC
        };
        public Account LoadAccount(string AccountNumber)
        {
            if (AccountNumber != _account.AccountNumber)
                return null;

            return _account;
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}
