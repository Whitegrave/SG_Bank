using NUnit.Framework;
using SG_Bank.BLL;
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

            AccountLookupResponse response = manager.LookupAccount("x");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Account.AccountNumber, "12345");
        }
    }
}
