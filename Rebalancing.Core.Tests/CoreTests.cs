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

            _market.GetSecurities(_aSymbol).Returns( new[] { _aaa });
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
            portfolio.AddTransaction(buyAAA);
            portfolio.AddTransaction(buyBBB);
            portfolio.AddTransaction(sellBBB);

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
    }
}