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
        public static IWithdraw Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.FREE:
                    return new FreeAccountWithdrawRule();
                case AccountType.BASIC:
                    return new BasicAccountWithdrawRule();
                case AccountType.PREMIUM:
                    return new PremiumAccountWithdrawRule();
                default:
                    throw new Exception("Account type was not supported.");
            }          
        }
    }
}
