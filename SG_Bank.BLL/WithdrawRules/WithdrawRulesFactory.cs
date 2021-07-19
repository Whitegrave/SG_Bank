using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.BLL.WithdrawRules
{
    class WithdrawRulesFactory
    {
        public static IWithdraw Create(AccountType accountType)
        {
            throw new Exception("Not implemented");
        }
    }
}
