using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankContext.Service
{
    public class BankService
    {
        private readonly BankContext.Context.BankContext bankContext;  

        public BankService (BankContext.Context.BankContext bankContext)
        {
            this.bankContext = bankContext;
        }


        public void InsertInitialData()
        {
            var initialData = new List<Client> {
                new Client
                {
                    FirstName = "Bankomat",
                    LastName = "Terminalovich",
                    Birthday = new DateTime(1990, 1, 1),
                    Phone = "",
                    Cards =  new List<Card>
                    {
                        new Card
                        {
                            CardId = "0000000000000000",
                            Balance = 0,
                            PinCode = "0000",
                            Operations_Out = new List<Operation>
                            {
                                new Operation
                                {
                                    InId = "4532299331784683",
                                    Amount = 1698.32M
                                },
                                new Operation
                                {
                                    InId = "5324541716750139",
                                    Amount = 340.0M
                                }
                            }
                        }
                    }
                },
                new Client
                {
                    FirstName = "Artem",
                    LastName = "Zhilin",
                    Phone = "+380123451234",
                    Birthday = new DateTime(1996, 6, 24),
                    Address = new Address
                    {
                        City = "Kherson",
                        State = "",
                        Country = "Ukraine",
                        Address_ = "Ushakova 96"
                    },
                    Cards =  new List<Card>
                    {
                        new Card
                        {
                            CardId = "4532299331784683",
                            Balance = 0,
                            PinCode = "1885",
                            Operations_Out = new List<Operation>
                            {
                                new Operation
                                {
                                    InId = "5324541716750139",
                                    Amount = 100M
                                }
                            }
                        }
                    }
                },
                new Client
                {
                    FirstName = "Jason",
                    LastName = "Statham",
                    Phone = "+441632960968",
                    Birthday = new DateTime(1967, 7, 26),
                    Address = new Address
                    {
                        Country = "England",
                        State = "Derbyshire",
                        City = "Shirebrook",
                        Address_ = "38 Garden Ave"
                    },
                    Cards =  new List<Card>
                    {
                        new Card
                        {
                            CardId = "5324541716750139",
                            Balance = 0,
                            PinCode = "4390",
                            Operations_Out = new List<Operation>
                            {
                                new Operation
                                {
                                    InId = "0000000000000000",
                                    Amount = 200M
                                }
                            }
                        }
                    }
                }
            };
            this.bankContext.Clients.AddRange(initialData);
            this.bankContext.SaveChanges();


        }

        public void RecalculateCardBalance(string cardId)
        {
            var card = this.bankContext.Cards.SingleOrDefault(c => c.CardId == cardId);
            if (card != null) 
            {
                card.Balance = card.Operations_In.Sum(o => o.Amount) - card.Operations_Out.Sum(o => o.Amount);
                this.bankContext.SaveChanges();
            }
        }
    }
}
