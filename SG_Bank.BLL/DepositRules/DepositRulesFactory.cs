using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.BLL.DepositRules
{
    public class DepositRulesFactory
    {
        public static IDeposit Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.FREE:
                    return new FreeAccountDepositRule();
                case AccountType.BASIC:
                    return new NoLimitDepositRule();
                case AccountType.PREMIUM:
                    return new NoLimitDepositRule();

            }

            throw new Exception("Account type was not supported.");
        }
    }
}
