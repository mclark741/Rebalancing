using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Rebalancing.Core.Tests
{
    public class CoreTests
    {
        private IMarket _market;
        private const string _aSymbol = "AAA";
        private const string _bSymbol = "BBB";

        private readonly Security _aaa = new Security
        {
            Symbol = _aSymbol,
            Price = 25
        };

        private readonly Security _bbb = new Security
        {
            Symbol = _bSymbol,
            Price = 25
        };

        [SetUp]
        public void Setup()
        {
            _market = Substitute.For<IMarket>();

            _market.GetSecurities(_aSymbol).Returns(new[] { _aaa });
            _market.GetSecurities(_bSymbol).Returns(new[] { _bbb });
        }

        [Test]
        public void TestRebalance()
        {
            // Arrange
            var buyAAA = new Transaction
            {
                Action = Action.Buy,
                Symbol = _aaa.Symbol,
                Quantity = 100,
                TotalAmount = _aaa.Price * 100
            };
            var buyBBB = new Transaction
            {
                Action = Action.Buy,
                Symbol = _bbb.Symbol,
                Quantity = 200,
                TotalAmount = _bbb.Price * 200
            };
            var sellBBB = new Transaction
            {
                Action = Action.Sell,
                Symbol = _bbb.Symbol,
                Quantity = 100,
                TotalAmount = _bbb.Price * 100
            };


            Portfolio portfolio = new Portfolio(_market);

            // Act
            portfolio.AddTransaction(buyAAA);
            portfolio.AddTransaction(buyBBB);
            portfolio.AddTransaction(sellBBB);

            var desiredPositions = new List<DesiredPosition>
            {
                new DesiredPosition
                {
                    Symbol = _aaa.Symbol,
                    PercentOfAccount = .75M
                },
                new DesiredPosition
                {
                    Symbol = _bbb.Symbol,
                    PercentOfAccount = .25M
                }
            };

            IEnumerable<Transaction> transactions = portfolio.Rebalance(desiredPositions);

            // Assert
            var expectedBuyTransaction = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 1250,
                Symbol = _aaa.Symbol
            };
            var expectedSellTransaction = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 1250,
                Symbol = _bbb.Symbol
            };

            var actualBuyTransaction = transactions.First(x => x.Action == Action.Buy);
            var actualSellTransaction = transactions.First(x => x.Action == Action.Sell);

            Assert.AreEqual(2, transactions.Count());

            Assert.AreEqual(expectedBuyTransaction.Symbol, actualBuyTransaction.Symbol);
            Assert.AreEqual(expectedBuyTransaction.TotalAmount, actualBuyTransaction.TotalAmount);

            Assert.AreEqual(expectedSellTransaction.Symbol, actualSellTransaction.Symbol);
            Assert.AreEqual(expectedSellTransaction.TotalAmount, actualSellTransaction.TotalAmount);
        }


        [Test]
        public void TestRebalanceWithAdditionalInvestment()
        {
            // Arrange
            var buyAAA = new Transaction
            {
                Action = Action.Buy,
                Symbol = _aaa.Symbol,
                Quantity = 100,
                TotalAmount = _aaa.Price * 100
            };
            var buyBBB = new Transaction
            {
                Action = Action.Buy,
                Symbol = _bbb.Symbol,
                Quantity = 200,
                TotalAmount = _bbb.Price * 200
            };
            var sellBBB = new Transaction
            {
                Action = Action.Sell,
                Symbol = _bbb.Symbol,
                Quantity = 100,
                TotalAmount = _bbb.Price * 100
            };


            Portfolio portfolio = new Portfolio(_market);

            // Act
            portfolio.AddTransaction(buyAAA);
            portfolio.AddTransaction(buyBBB);
            portfolio.AddTransaction(sellBBB);

            var desiredPositions = new List<DesiredPosition>
            {
                new DesiredPosition
                {
                    Symbol = _aaa.Symbol,
                    PercentOfAccount = .75M
                },
                new DesiredPosition
                {
                    Symbol = _bbb.Symbol,
                    PercentOfAccount = .25M
                }
            };

            IEnumerable<Transaction> transactions = portfolio.Rebalance(desiredPositions, 1000);

            // Assert
            var expectedBuyTransaction = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 2000,
                Symbol = _aaa.Symbol
            };
            var expectedSellTransaction = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 1000,
                Symbol = _bbb.Symbol
            };

            var actualBuyTransaction = transactions.First(x => x.Action == Action.Buy);
            var actualSellTransaction = transactions.First(x => x.Action == Action.Sell);

            Assert.AreEqual(2, transactions.Count());

            Assert.AreEqual(expectedBuyTransaction.Symbol, actualBuyTransaction.Symbol);
            Assert.AreEqual(expectedBuyTransaction.TotalAmount, actualBuyTransaction.TotalAmount);

            Assert.AreEqual(expectedSellTransaction.Symbol, actualSellTransaction.Symbol);
            Assert.AreEqual(expectedSellTransaction.TotalAmount, actualSellTransaction.TotalAmount);
        }


        [Test]
        public void TestQuantity()
        {
            // Arrange
            var buyAAA = new Transaction
            {
                Action = Action.Buy,
                Symbol = _aaa.Symbol,
                Quantity = 100
            };
            var buyBBB = new Transaction
            {
                Action = Action.Buy,
                Symbol = _bbb.Symbol,
                Quantity = 200
            };
            var sellBBB = new Transaction
            {
                Action = Action.Sell,
                Symbol = _bbb.Symbol,
                Quantity = 100
            };

            Portfolio portfolio = new Portfolio(_market);

            // Act

            portfolio.AddTransactions(new[] { buyAAA, buyBBB, sellBBB });

            // Assert
            decimal expectedAAAQuantity = 100;
            decimal actualAAAQuantity = portfolio.GetQuantity(_aSymbol);
            Assert.AreEqual(expectedAAAQuantity, actualAAAQuantity, "quantities of AAA should match");

            decimal expectedBBBQuantity = 100;
            decimal actualBBBQuantity = portfolio.GetQuantity(_bSymbol);
            Assert.AreEqual(expectedBBBQuantity, actualBBBQuantity, "quantities of BBB should match");


            decimal expectedAAAPercentage = .5M;
            decimal actualAAAPercentage = portfolio.GetPosition(_bSymbol).PercentOfAccount;
            Assert.AreEqual(expectedAAAPercentage, actualAAAPercentage, "portfolio percent of AAA should match");

            decimal expectedBBBPercentage = .5M;
            decimal actualBBBPercentage = portfolio.GetPosition(_bSymbol).PercentOfAccount;
            Assert.AreEqual(expectedBBBPercentage, actualBBBPercentage, "portfolio percent of BBB should match");
        }

        [Test]
        public void TestRebalanceExchangeSimple()
        {
            // Arrange
            var buyAAA = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 2000,
                Symbol = _aaa.Symbol
            };
            var sellBBB = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 1000,
                Symbol = _bbb.Symbol
            };

            Portfolio portfolio = new Portfolio(_market);

            // Act
            IEnumerable<TransactionExchange> transactions = portfolio.Format(new[] { buyAAA, sellBBB });

            // Assert
            var expectedTransactionWrapper0 = new TransactionExchange
            {
                SellSymbol = _bbb.Symbol,
                BuySymbol = _aaa.Symbol,
                TotalAmount = 1000
            };

            var expectedTransactionWrapper1 = new TransactionExchange
            {
                SellSymbol = null,
                BuySymbol = _aaa.Symbol,
                TotalAmount = 1000
            };

            Assert.AreEqual(2, transactions.Count());

            Assert.AreEqual(expectedTransactionWrapper0.SellSymbol, transactions.First().SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper0.BuySymbol, transactions.First().BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper0.TotalAmount, transactions.First().TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper1.SellSymbol, transactions.Last().SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper1.BuySymbol, transactions.Last().BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper1.TotalAmount, transactions.Last().TotalAmount);
        }

        [Test]
        public void TestRebalanceExchangeMoreSymbols()
        {
            // Arrange
            var sellFLPSX = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 31.63m,
                Symbol = "FLPSX"
            };
            var buyFPADX = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 76.50m,
                Symbol = "FPADX"
            };
            var sellFRESX = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 22.65m,
                Symbol = "FRESX"
            };
            var buyFSPSX = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 19.93m,
                Symbol = "FSPSX"
            };
            var buyFSSNX = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 90.25m,
                Symbol = "FSSNX"
            };
            var buyTBCIX = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 158.29m,
                Symbol = "TBCIX"
            };

            Portfolio portfolio = new Portfolio(_market);

            // Act
            IEnumerable<TransactionExchange> transactions = portfolio.Format(new[] { buyFPADX, buyFSPSX, buyFSSNX, buyTBCIX, sellFLPSX, sellFRESX });

            // Assert
            var expectedTransactionWrapper0 = new TransactionExchange
            {
                SellSymbol = sellFLPSX.Symbol,
                BuySymbol = buyTBCIX.Symbol,
                TotalAmount = sellFLPSX.TotalAmount
            };

            var expectedTransactionWrapper1 = new TransactionExchange
            {
                SellSymbol = sellFRESX.Symbol,
                BuySymbol = buyTBCIX.Symbol,
                TotalAmount = sellFRESX.TotalAmount
            };

            var expectedTransactionWrapper2 = new TransactionExchange
            {
                SellSymbol = null,
                BuySymbol = buyTBCIX.Symbol,
                TotalAmount = buyTBCIX.TotalAmount - sellFLPSX.TotalAmount - sellFRESX.TotalAmount
            };

            var expectedTransactionWrapper3 = new TransactionExchange
            {
                SellSymbol = null,
                BuySymbol = buyFSSNX.Symbol,
                TotalAmount = buyFSSNX.TotalAmount
            };

            var expectedTransactionWrapper4 = new TransactionExchange
            {
                SellSymbol = null,
                BuySymbol = buyFPADX.Symbol,
                TotalAmount = buyFPADX.TotalAmount
            };
            var expectedTransactionWrapper5 = new TransactionExchange
            {
                SellSymbol = null,
                BuySymbol = buyFSPSX.Symbol,
                TotalAmount = buyFSPSX.TotalAmount
            };

            var transactionArray = transactions.ToArray();

            Assert.AreEqual(6, transactions.Count());

            Assert.AreEqual(expectedTransactionWrapper0.SellSymbol, transactionArray[0].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper0.BuySymbol, transactionArray[0].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper0.TotalAmount, transactionArray[0].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper1.SellSymbol, transactionArray[1].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper1.BuySymbol, transactionArray[1].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper1.TotalAmount, transactionArray[1].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper2.SellSymbol, transactionArray[2].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper2.BuySymbol, transactionArray[2].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper2.TotalAmount, transactionArray[2].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper3.SellSymbol, transactionArray[3].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper3.BuySymbol, transactionArray[3].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper3.TotalAmount, transactionArray[3].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper4.SellSymbol, transactionArray[4].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper4.BuySymbol, transactionArray[4].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper4.TotalAmount, transactionArray[4].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper5.SellSymbol, transactionArray[5].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper5.BuySymbol, transactionArray[5].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper5.TotalAmount, transactionArray[5].TotalAmount);
        }


        [Test]
        public void TestRebalanceExchangeMoreSymbols_2()
        {
            // Arrange
            var buyFLPSX = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 118.99m,
                Symbol = "FLPSX"
            };
            var sellFPADX = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 23.00m,
                Symbol = "FPADX"
            };
            var sellFRESX = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 61.98m,
                Symbol = "FRESX"
            };
            var buyFSPSX = new Transaction
            {
                Action = Action.Buy,
                TotalAmount = 15.88m,
                Symbol = "FSPSX"
            };
            var sellFSSNX = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 7.55m,
                Symbol = "FSSNX"
            };
            var sellTBCIX = new Transaction
            {
                Action = Action.Sell,
                TotalAmount = 42.33m,
                Symbol = "TBCIX"
            };

            Portfolio portfolio = new Portfolio(_market);

            // Act
            IEnumerable<TransactionExchange> transactions = portfolio.Format(new[] { sellFPADX, buyFSPSX, sellFSSNX, sellTBCIX, buyFLPSX, sellFRESX });

            // Assert
            var expectedTransactionWrapper0 = new TransactionExchange
            {
                SellSymbol = sellFRESX.Symbol,
                BuySymbol = buyFLPSX.Symbol,
                TotalAmount = sellFRESX.TotalAmount
            };

            var expectedTransactionWrapper1 = new TransactionExchange
            {
                SellSymbol = sellTBCIX.Symbol,
                BuySymbol = buyFLPSX.Symbol,
                TotalAmount = sellTBCIX.TotalAmount
            };

            var expectedTransactionWrapper2 = new TransactionExchange
            {
                SellSymbol = sellFSSNX.Symbol,
                BuySymbol = buyFLPSX.Symbol,
                TotalAmount = sellFSSNX.TotalAmount
            };

            var expectedTransactionWrapper3 = new TransactionExchange
            {
                SellSymbol = null,
                BuySymbol = buyFLPSX.Symbol,
                TotalAmount = buyFLPSX.TotalAmount - sellFRESX.TotalAmount - sellTBCIX.TotalAmount - sellFSSNX.TotalAmount
            };

            var expectedTransactionWrapper4 = new TransactionExchange
            {
                SellSymbol = null,
                BuySymbol = buyFSPSX.Symbol,
                TotalAmount = buyFSPSX.TotalAmount
            };
            var expectedTransactionWrapper5 = new TransactionExchange
            {
                SellSymbol = sellFPADX.Symbol,
                BuySymbol = null,
                TotalAmount = sellFPADX.TotalAmount
            };

            var transactionArray = transactions.ToArray();

            Assert.AreEqual(6, transactions.Count());

            Assert.AreEqual(expectedTransactionWrapper0.SellSymbol, transactionArray[0].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper0.BuySymbol, transactionArray[0].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper0.TotalAmount, transactionArray[0].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper1.SellSymbol, transactionArray[1].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper1.BuySymbol, transactionArray[1].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper1.TotalAmount, transactionArray[1].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper2.SellSymbol, transactionArray[2].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper2.BuySymbol, transactionArray[2].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper2.TotalAmount, transactionArray[2].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper3.SellSymbol, transactionArray[3].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper3.BuySymbol, transactionArray[3].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper3.TotalAmount, transactionArray[3].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper4.SellSymbol, transactionArray[4].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper4.BuySymbol, transactionArray[4].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper4.TotalAmount, transactionArray[4].TotalAmount);

            Assert.AreEqual(expectedTransactionWrapper5.SellSymbol, transactionArray[5].SellSymbol);
            Assert.AreEqual(expectedTransactionWrapper5.BuySymbol, transactionArray[5].BuySymbol);
            Assert.AreEqual(expectedTransactionWrapper5.TotalAmount, transactionArray[5].TotalAmount);
        }
    }
}