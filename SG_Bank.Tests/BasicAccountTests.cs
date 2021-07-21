using NUnit.Framework;
using SG_Bank.BLL.DepositRules;
using SG_Bank.BLL.WithdrawRules;
using SG_Bank.Models;
using SG_Bank.Models.Interfaces;
using SG_Bank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Bank.Tests
{
    [TestFixture]
    public class BasicAccountTests
    {
        [TestCase("33333", "Basic Account", 100.00, AccountType.FREE, 250.00, false)]       // Fail, wrong account type
        [TestCase("33333", "Basic Account", 100.00, AccountType.BASIC, -100.00, false)]     // Fail, negative deposit   
        [TestCase("33333", "Basic Account", 100.00, AccountType.BASIC, 100.00, true)]       // Pass 
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit basicDeposit = new NoLimitDepositRule();
            Account account = new Account();
            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse depositResponse = basicDeposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, depositResponse.Success);
        }

        [TestCase("33333", "Basic Account", 1500.00, AccountType.BASIC, -1000.00, 1500.00, false)]       // Fail, too much withdrawn
        [TestCase("33333", "Basic Account", 100.00, AccountType.FREE, -100.00, 100.00, false)]           // Fail, mismatched account type
        [TestCase("33333", "Basic Account", 100.00, AccountType.BASIC, 100.00, 100.00, false)]           // Fail, positive withdraw amount 
        [TestCase("33333", "Basic Account", 150.00, AccountType.BASIC, -50.00, 100.00, true)]            // Pass
        [TestCase("33333", "Basic Account", 100.00, AccountType.BASIC, -150.00, -60.00, true)]           // Pass, includes overdraft fee
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw basicWithdraw = new BasicAccountWithdrawRule();
            Account account = new Account();
            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse withdrawResponse = basicWithdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, withdrawResponse.Success);
            if (withdrawResponse.Success)
            {
                Assert.AreEqual(newBalance, withdrawResponse.Account.Balance);
            }
        }
    }
}
