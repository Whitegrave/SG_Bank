using SG_Bank.BLL;
using SG_Bank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Workflows
{
    public class AccountLookupWorkflow
    {
        public void Execute()
        {
            AccountManager manager = AccountManagerFactory.Create();

            string accountNumber = ConsoleIO.GetStringFromUser("Lookup an Account\n" +
                                                               "-----------------------------\n\n" +
                                                               "Enter an account number: ", 1, 10, false, false, true, false, false, false, true);
            AccountLookupResponse response = manager.LookupAccount(accountNumber);

            if (response.Success)
            {
                ConsoleIO.DisplayAccountDetails(response.Account);
            }
            else
            {
                ConsoleIO.DisplayToUser("The following error occurred: " + response.Message, false);
            }
            ConsoleIO.DisplayToUser("\nPress any key to continue...", true);
        }
    }
}
