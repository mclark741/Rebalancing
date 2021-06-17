using System;
using System.Collections.Generic;
using System.Linq;

namespace Rebalancing.Core
{
    public class Portfolio : IPortfolio
    {
        private readonly IMarket _market;

        private readonly List<CurrentPosition> _positions = new List<CurrentPosition>();

        public decimal TotalValue => _positions.Sum(x => x.CurrentValue);
        public List<CurrentPosition> Positions => _positions.OrderBy(x => x.Security.Symbol).ToList();

        public Portfolio(IMarket market)
        {
            _market = market;
        }

        // 1. get transaction history
        // -- calculate owned quantity of each security

        // 2. calculate portfolio
        // -- calculate current total $ 
        // -- calculate current total %

        // 3. get desired stock %
        // calculate difference in current and desired % and $

        public void AddTransaction(Transaction transaction)
        {
            AddTransactions(new[] { transaction });
        }

        public void AddTransactions(IEnumerable<Transaction> transactions)
        {
            var validTransactions = transactions.Where(IsValid).ToList();
            bool anyValidTransactions = validTransactions.Any();

            if (!anyValidTransactions)
            {
                return;
            }

            validTransactions.ForEach(ExecuteTransaction);

            // reset the percentages for each position
            _positions.ForEach(x => x.PercentOfAccount = x.CurrentValue / TotalValue);
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            // does the current security already exist?
            var position = GetPosition(transaction.Symbol);

            // determine if we are buying or selling and add or subtract the quantity
            // use the absolute value to rule out positive and negative values
            switch (transaction.Action)
            {
                case Action.Buy:
                    position.Quantity += Math.Abs(transaction.Quantity);
                    break;
                case Action.Sell:
                    position.Quantity -= Math.Abs(transaction.Quantity);
                    break;
                case Action.None:
                default:
                    break;
            }
        }

        /// <summary>
        /// Get a list of transactions to rebalance the portfololio to the desired positions
        /// </summary>
        /// <param name="desiredPositions">List containing the desired percentage allocation of each security</param>
        /// <returns></returns>
        public IEnumerable<Transaction> Rebalance(IEnumerable<DesiredPosition> desiredPositions, decimal additionalInvestment = 0)
        {
            foreach (var desiredPosition in desiredPositions)
            {
                var currentPosition = GetPosition(desiredPosition.Symbol);

                // calculate the desired dollar value for the position
                var desiredValue = desiredPosition.PercentOfAccount * (TotalValue + additionalInvestment);

                // calculate the difference to rebalance this position
                var difference = desiredValue - currentPosition.CurrentValue;

                // build out the rebalance transaction
                Transaction transaction = new Transaction
                {
                    Symbol = desiredPosition.Symbol,
                    Action = difference > 0 ? Action.Buy : Action.Sell,
                    TotalAmount = Math.Round(Math.Abs(difference), 2)
                };
                yield return transaction;
            }
        }

        /// <summary>
        /// Gets the Quantity of the symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public decimal GetQuantity(string symbol)
        {
            return GetPosition(symbol)?.Quantity ?? 0;
        }

        /// <summary>
        /// Gets the Total Portfolio value
        /// </summary>
        /// <returns></returns>
        public decimal GetCurrentValue()
        {
            return TotalValue;
        }

        /// <summary>
        /// Gets the Total value for the symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public decimal GetCurrentValue(string symbol)
        {
            return GetPosition(symbol)?.CurrentValue ?? 0;
        }

        public CurrentPosition GetPosition(Security security)
        {
            return GetPosition(security.Symbol);
        }

        /// <summary>
        /// Gets the Position by the Symbol. If the Position doesn't exist, this will create a new one.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public CurrentPosition GetPosition(string symbol)
        {
            var position = _positions.FirstOrDefault(x => x.Security.Symbol.Trim().Equals(symbol.Trim(), StringComparison.OrdinalIgnoreCase));

            // create a new position if it doesn't already exist
            if (position == null)
            {
                // create a new position for the current transaction
                position = new CurrentPosition
                {
                    Security = _market.GetSecurities(symbol).FirstOrDefault()
                };

                _positions.Add(position);
            }

            return position;
        }

        private bool IsValid(Transaction transaction)
        {
            return !string.IsNullOrWhiteSpace(transaction.Symbol) && transaction.Action != Action.None;
        }


        public IEnumerable<TransactionExchange> Format(IEnumerable<Transaction> originalTransactions)
        {
            var transactions = new List<TransactionExchange>();

            var buyTransactions = originalTransactions
                                        .Where(x => x.Action == Action.Buy)
                                        .OrderByDescending(x => Math.Abs(x.TotalAmount))
                                        .ToList();
            var sellTransactions = originalTransactions
                                        .Where(x => x.Action == Action.Sell)
                                        .OrderByDescending(x => Math.Abs(x.TotalAmount))
                                        .ToList();

            transactions.AddRange(GetTransactions(buyTransactions, sellTransactions));
            transactions.AddRange(GetTransactions(sellTransactions, buyTransactions));

            //transactions.AddRange(GetTransactions2(originalTransactions.OrderByDescending(x => Math.Abs(x.TotalAmount)).ToList()));

            return transactions;
        }

        private List<string> expiredSymbols = new List<string>();

        private IEnumerable<TransactionExchange> GetTransactions(List<Transaction> primaryTransactions, List<Transaction> secondaryTransaction)
        {
            var transactions = new List<TransactionExchange>();
            var primaryAction = primaryTransactions.FirstOrDefault()?.Action;

            foreach (var primaryTransaction in primaryTransactions.Where(t => expiredSymbols.All(e => t.Symbol != e)))
            {
                var totalAmount = primaryTransaction.TotalAmount;

                do
                {
                    var matchingSecondaryTransaction = secondaryTransaction.Where(t => expiredSymbols.All(e => t.Symbol != e))
                                                                           .FirstOrDefault(x => Math.Abs(x.TotalAmount) <= totalAmount);
                    var exchange = new TransactionExchange
                    {
                        SellSymbol = primaryAction == Action.Sell ? primaryTransaction.Symbol : matchingSecondaryTransaction?.Symbol,
                        BuySymbol = primaryAction == Action.Buy ? primaryTransaction.Symbol : matchingSecondaryTransaction?.Symbol,
                        TotalAmount = matchingSecondaryTransaction?.TotalAmount ?? totalAmount
                    };

                    totalAmount -= exchange.TotalAmount;

                    transactions.Add(exchange);
                    expiredSymbols.Add(matchingSecondaryTransaction?.Symbol);

                } while (totalAmount > 0);

                expiredSymbols.Add(primaryTransaction.Symbol);
            }

            return transactions;
        }

        private IEnumerable<TransactionExchange> GetTransactions2(List<Transaction> transactions)
        {
            /*
             * 
             * loop through transactions
             * 
             * find secondary transaction that is 
             * * not expired (used previousy)
             * * total amount <= current transaction
             * * not the same symbol as the current transaction
             * 
             * subtract the secondary transaction total amount from the primary transaction amount
             * * use primary transaction action to figure out buy or sell
             * secondary transaction has been used, expire secondary transaction
             * 
             * 
             * keep subtracting secondary transaction amounts until primary transaction total value is 0
             * primary transaction has been used, expire primary transaction
             * if no more secondary transactions exist, buy/sell primary transaction with remaining total value
             * 
             * 
             */

            // loop through transactions
            // find transaction
            // 


            var retVal = new List<TransactionExchange>();
            var primaryAction = transactions.FirstOrDefault()?.Action;

            foreach (var currentTransaction in transactions.Where(t => expiredSymbols.All(e => t.Symbol != e)))
            {
                if (expiredSymbols.Any(eS => eS == currentTransaction.Symbol))
                {
                    continue;
                }

                var totalAmount = currentTransaction.TotalAmount;

                do
                {
                    var matchingSecondaryTransaction = transactions.FirstOrDefault(x => expiredSymbols.All(eS => x.Symbol != eS)
                                                                               && x.Symbol != currentTransaction.Symbol
                                                                               && x.Action != currentTransaction.Action);
                    //.FirstOrDefault(x => Math.Abs(x.TotalAmount) <= totalAmount);

                    var exchange = currentTransaction.Action == Action.Buy ?
                     new TransactionExchange
                     {
                         SellSymbol = matchingSecondaryTransaction?.Symbol,
                         BuySymbol = currentTransaction.Symbol,
                         TotalAmount = Math.Min(matchingSecondaryTransaction?.TotalAmount ?? totalAmount, totalAmount)
                     } :
                     new TransactionExchange
                     {
                         SellSymbol = currentTransaction.Symbol,
                         BuySymbol = matchingSecondaryTransaction?.Symbol,
                         TotalAmount = Math.Min(matchingSecondaryTransaction?.TotalAmount ?? totalAmount, totalAmount)
                     };

                    totalAmount -= exchange.TotalAmount;

                    retVal.Add(exchange);

                    var remainder = (matchingSecondaryTransaction?.TotalAmount).GetValueOrDefault() - exchange.TotalAmount;

                    if (remainder != 0)
                    {
                        var exchangeRemainder = currentTransaction.Action == Action.Buy ?
                            new TransactionExchange
                            {
                                SellSymbol = matchingSecondaryTransaction?.Symbol,
                                BuySymbol = currentTransaction.Symbol,
                                TotalAmount = remainder
                            } :
                            new TransactionExchange
                            {
                                SellSymbol = currentTransaction.Symbol,
                                BuySymbol = matchingSecondaryTransaction?.Symbol,
                                TotalAmount = remainder
                            };

                        retVal.Add(exchangeRemainder);
                    }

                    expiredSymbols.Add(matchingSecondaryTransaction?.Symbol);

                } while (totalAmount > 0);

                expiredSymbols.Add(currentTransaction.Symbol);
            }

            return retVal;
        }
    }
}
