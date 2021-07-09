using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using SG_Bank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.BLL.DepositRules
{
    public class FreeAccountDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            if (account.Type != AccountType.FREE)
            {
                response.Success = false;
                response.Message = "Error: mismatched account type, not free";
                return response;
            }

            if (amount > 100)
            {
                response.Success = false;
                response.Message = "Free accounts cannot deposite more than $100.00 in a single transaction";
                return response;
            }

            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amount must be greater than $0.00";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
