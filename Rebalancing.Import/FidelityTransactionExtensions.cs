using System.Collections.Generic;
using System.Linq;
using Rebalancing.Core;

namespace Rebalancing.Import
{
    public static class FidelityTransactionExtensions
    {
        public static Transaction ToTransaction(this FidelityTransaction obj)
        {
            var transaction = new Transaction
            {
                Action = GetAction(obj.Action),
                TotalAmount = obj.Amount.GetValueOrDefault(0M),
                Quantity = obj.Quantity.GetValueOrDefault(0M),
                Description = obj.Action.Trim(),
                TransactionDate = obj.RunDate,
                SettlementDate = obj.SettlementDate,
                Symbol = obj.Symbol.Trim()
            };

            return transaction;
        }

        public static Core.Action GetAction(string actionVal)
        {
            List<string> buyStrings = new List<string> { "YOU BOUGHT", "REINVESTMENT" };
            List<string> sellStrings = new List<string> { "YOU SOLD" };

            Core.Action action = Core.Action.None;

            if (!string.IsNullOrEmpty(actionVal))
            {
                if (buyStrings.Any(x => actionVal.TrimStart().StartsWith(x)))
                {
                    action = Core.Action.Buy;
                }
                else if (sellStrings.Any(x => actionVal.TrimStart().StartsWith(x)))
                {
                    action = Core.Action.Sell;
                }
            }

            return action;
        }
    }
}
