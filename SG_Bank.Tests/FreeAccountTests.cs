using NUnit.Framework;
using SG_Bank.BLL;
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
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Account.AccountNumber, "12345");
        }



        [TestCase("12345", "Free Account", 100.00, AccountType.FREE, 250.00, false)]
        [TestCase("12345", "Free Account", 100.00, AccountType.FREE, -100.00, false)]
        [TestCase("12345", "Basic Account", 100.00, AccountType.BASIC, 50.00, false)]
        [TestCase("12345", "Free Account", 100.00, AccountType.FREE, 50.00, true)]
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit freeDeposit = new FreeAccountDepositRule();
            Account account = new Account();
            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse depositResponse = freeDeposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, depositResponse.Success);
        }


        [TestCase("12345", "Free Account", 100.00, AccountType.FREE, 50.00, false)]
        [TestCase("12345", "Free Account", 10000.00, AccountType.FREE, -1000.00, false)]
        [TestCase("12345", "Free Account", 1000.00, AccountType.FREE, -500.00, false)]
        [TestCase("12345", "Basic Account", 100.00, AccountType.FREE, -500.00, false)]
        [TestCase("12345", "Free Account", 200.00, AccountType.BASIC, -100.00, false)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw freeWithdraw = new FreeAccountWithdrawRule();
            Account account = new Account();
            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse withdrawResponse = freeWithdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, withdrawResponse.Success);
        }
    }
}
