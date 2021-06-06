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
    }
}
