using SG_Bank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Models.Interfaces
{
    public interface IDeposit
    {
        AccountDepositResponse Deposit(Account account, decimal amount);
    }
}
