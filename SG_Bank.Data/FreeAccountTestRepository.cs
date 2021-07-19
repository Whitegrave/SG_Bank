using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_Bank.Models;
using SG_Bank.Models.Interfaces;

namespace SG_Bank.Data
{
    public class FreeAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Free Account",
            Balance = 100.00M,
            AccountNumber = "12345",
            Type = AccountType.FREE
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
