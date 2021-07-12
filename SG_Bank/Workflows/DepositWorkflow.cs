using SG_Bank.BLL;
using SG_Bank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Workflows
{
    public class DepositWorkflow
    {
        public void Execute()
        {
            AccountManager accountManager = AccountManagerFactory.Create();
            string accountNumber = ConsoleIO.GetStringFromUser("Enter an account number: ", 1, 10, false, false, true, false, false, false, true);

            decimal amount = ConsoleIO.GetDecimalFromUser("Enter a deposit amount: ", false, false, 0, 10000, true);

            AccountDepositResponse response = accountManager.Deposit(accountNumber, amount);

            if (response.Success)
            {
                ConsoleIO.DisplayToUser($"Deposited {response.Amount} to account {response.Account.AccountNumber}\n\n" +
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
