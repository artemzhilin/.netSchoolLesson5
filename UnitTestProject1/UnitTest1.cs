using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using BankContext.Service;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInsertInitialData()
        {
            Card card = null;
            Client client = null;

            using (var bctx = new BankContext.Context.BankContext("bank_zhilnikov1"))
            {
                var bankService = new BankService(bctx);
                bankService.InsertInitialData();

                // There is no such card in initial inserted data
                card = bctx.Cards.SingleOrDefault(c => c.CardId == "453229933170000");
 
                client = bctx.Clients.SingleOrDefault(c => c.FirstName == "Jason");
            }

            Assert.IsNull(card);
            Assert.AreEqual("Statham", client.LastName);
        }
        
        [TestMethod]
        public void TestRecalculateBalance()
        {
            Card card = null;

            using (var bctx = new BankContext.Context.BankContext("bank_zhilnikov1"))
            {
                var bankService = new BankService(bctx);
                var cards = bctx.Cards.ToList();
                cards.ForEach(c => bankService.RecalculateCardBalance(c.CardId));

                var card1 = cards.SingleOrDefault(c => c.CardId == "4532299331784683");
                var card2 = cards.SingleOrDefault(c => c.CardId == "5324541716750139");
                Assert.AreEqual(1598.32M, card1.Balance);
                Assert.AreEqual(240M, card2.Balance);
            }
        }
    }
}
