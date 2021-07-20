using SG_Bank.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank
{
    public static class Menu
    {
        public static object DespositWorkflow { get; private set; }

        public static void Start()
        {
            while (true)
            {
                string userChoice = ConsoleIO.GetStringFromUser("SG Bank Application\n" + 
                                                                "----------------------------\n" +
                                                                "1. Lookup an Account\n" +
                                                                "2. Deposit\n" +
                                                                "3. Withdrawal\n" +
                                                                "Q to quit\n\n" +
                                                                "Enter selection: ", 1, 1, false, false, true, false, false, false, true).ToUpper();

                switch (userChoice)
                {
                    case "1":
                        AccountLookupWorkflow lookupWorkflow = new AccountLookupWorkflow();
                        lookupWorkflow.Execute();
                        break;
                    case "2":
                        DepositWorkflow depositWorkflow = new DepositWorkflow();
                        depositWorkflow.Execute();
                        break;
                    case "3":
                        WithdrawWorkflow withdrawWorkflow = new WithdrawWorkflow();
                        withdrawWorkflow.Execute();
                        break;
                    case "Q":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}
