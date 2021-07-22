using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private static string _path = @".\Accounts.txt";

        // Method to return a list object of Accounts for Load/Save actions
        public List<Account> GetAccountsFromFile()
        {
            // Get account data
            string[] rows = File.ReadAllLines(_path);
            // Remove header row
            rows = rows.Skip(0).ToArray();

            // Create list for Accounts
            List<Account> accounts = new List<Account>();

            for (int i = 1; i < rows.Length; i++)
            {
                // Parse delimited lines into items
                string[] fields = rows[i].Split(',');
                Account x = new Account();

                // Populate Account fields
                try
                {
                    x.AccountNumber = fields[0];
                    x.Name = fields[1];
                    x.Balance = decimal.Parse(fields[2]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error was encountered parsing accound file data: \n");
                    Console.WriteLine(String.Concat(e.Message, e.StackTrace));
                    Console.WriteLine("\n\n The repository was reset to default values.");
                    CreateLiveRepo();
                }

                switch (fields[3])
                {
                    case "F":
                        x.Type = AccountType.FREE;
                        break;
                    case "B":
                        x.Type = AccountType.BASIC;
                        break;
                    case "P":
                        x.Type = AccountType.PREMIUM;
                        break;
                    default:
                        throw new Exception("An invalid account type was parsed from account file repository.");
                }

                // Add Account to list
                accounts.Add(x);
            }
            return accounts.ToList();
        }

        public Account LoadAccount(string AccountNumber)
        {
            List<Account> accounts = GetAccountsFromFile();

            // search through accounts for a matching account number
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].AccountNumber == AccountNumber)
                    return accounts[i];
            }

            return null;
        }

        public void SaveAccount(Account account)
        {
            List<Account> accounts = GetAccountsFromFile();
            string accountNumber = account.AccountNumber;
            int accountIndex = 0;
            // search through accounts for a matching account number
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].AccountNumber == accountNumber)
                    accountIndex = i + 1;
            }
            // dummy array to repopulate file after edit
            string[] rows = File.ReadAllLines(_path);

            // Populate accountIndex line with converted account data
            if (account.Type == AccountType.FREE)
            {
                rows[accountIndex] = account.AccountNumber + "," + account.Name + "," + account.Balance.ToString() + "," + "F";
            }
            else if (account.Type == AccountType.BASIC)
            {
                rows[accountIndex] = account.AccountNumber + "," + account.Name + "," + account.Balance.ToString() + "," + "B";
            }
            else if (account.Type == AccountType.PREMIUM)
            {
                rows[accountIndex] = account.AccountNumber + "," + account.Name + "," + account.Balance.ToString() + "," + "P";
            }

            // Write to file
            using (StreamWriter writer = File.CreateText(_path))
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    writer.WriteLine(rows[i]);
                }
            }


        }

        // init live file repo
        public static void InitializeFileRepo()
        {
            if (!GetDoesLiveRepoExist())
            {
                CreateLiveRepo();
            }
        }

        // Method to check if hard repo exists
        public static bool GetDoesLiveRepoExist()
        {
            if (File.Exists(_path))
                return true;
            return false;
        }

        // Method to create a new hard repo if desired or in the event of external tampering
        public static void CreateLiveRepo()
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                writer.WriteLine("AccountNumber,Name,Balance,Type");
                writer.WriteLine("11111,Free Customer,100,F");
                writer.WriteLine("22222,Basic Customer,500,B");
                writer.WriteLine("33333,Premium Customer,1000,P");
            }
        }
    }
}
