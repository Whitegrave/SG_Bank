using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Premium Account",
            Balance = 100.00M,
            AccountNumber = "11111",
            Type = AccountType.PREMIUM
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
