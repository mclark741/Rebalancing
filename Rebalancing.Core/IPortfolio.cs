using System.Collections.Generic;

namespace Rebalancing.Core
{
    public interface IPortfolio
    {
        decimal TotalValue { get; }
        List<CurrentPosition> Positions { get; }

        void AddTransaction(Transaction transaction);
        void AddTransactions(IEnumerable<Transaction> transaction);
        decimal GetCurrentValue();
        decimal GetCurrentValue(string symbol);
        CurrentPosition GetPosition(Security security);
        CurrentPosition GetPosition(string symbol);
        decimal GetQuantity(string symbol);
        IEnumerable<Transaction> Rebalance(IEnumerable<DesiredPosition> desiredPositions, decimal additionalInvestment = 0);
    }
}