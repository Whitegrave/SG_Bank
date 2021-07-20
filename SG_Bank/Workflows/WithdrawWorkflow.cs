using SG_Bank.BLL;
using SG_Bank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Workflows
{
    public class WithdrawWorkflow
    {
        public void Execute()
        {
            AccountManager accountManager = AccountManagerFactory.Create();
            string accountNumber = ConsoleIO.GetStringFromUser("Enter an account number: ", 1, 10, false, false, true, false, false, false, true);

            decimal amount = ConsoleIO.GetDecimalFromUser("Enter a withdrawal amount: ", false, true, -10000000, 10000000, true);
            // convert positives to negative
            if (amount > 0)
                amount = 0 - amount;

            AccountWithdrawResponse response = accountManager.Withdraw(accountNumber, amount);

            if (response.Success)
            {
                ConsoleIO.DisplayToUser($"Withdrew {response.Amount} from account {response.Account.AccountNumber}\n\n" +
                                        $"Original balance: {response.OldBalance}\n" +
                                        $"New balance: {response.Account.Balance}\n\n");
            }
            else
            {
                ConsoleIO.DisplayToUser($"An error occurred: {response.Message}");
            }

            ConsoleIO.DisplayToUser("Press any key to continue...", true);
        }
    }
}
