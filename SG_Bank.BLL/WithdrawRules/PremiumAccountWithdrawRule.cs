using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using SG_Bank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (account.Type != AccountType.PREMIUM)
            {
                response.Success = false;
                response.Message = "Error: mismatched account type, not premium";
                return response;
            }

            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative.";
                return response;
            }

            if (account.Balance + amount < -500)
            {
                response.Success = false;
                response.Message = "This amount will overdraft more than your $500 limit.";
                return response;
            }

            // Overdraft fee
            if (account.Balance + amount < 0)
                amount += -10;

            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
