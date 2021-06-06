using System;
using System.Collections.Generic;

namespace Rebalancing.Core
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? SettlementDate { get; set; }

        public string Symbol { get; set; }
        public Action Action { get; set; }

        public override string ToString()
        {
            return $"{Action} {Quantity} {Symbol}: {TotalAmount}";
        }
    }

    public class TransactionEqualityComparer : IEqualityComparer<Transaction>
    {
        public bool Equals(Transaction t1, Transaction t2)
        {
            if (t2 == null && t1 == null)
                return true;
            else if (t1 == null || t2 == null)
                return false;
            else if (t1.Quantity == t2.Quantity
                && t1.TotalAmount == t2.TotalAmount
                && t1.Description == t2.Description
                && t1.TransactionDate == t2.TransactionDate
                && t1.SettlementDate == t2.SettlementDate
                && t1.Symbol == t2.Symbol
                && t1.Action == t2.Action)
                return true;
            else
                return false;
        }

        public int GetHashCode(Transaction t)
        {
            int hCode = t.Quantity.GetHashCode()
                        ^ t.TotalAmount.GetHashCode()
                        ^ t.Description.GetHashCode()
                        ^ t.TransactionDate.GetHashCode()
                        ^ t.SettlementDate.GetHashCode()
                        ^ t.Symbol.GetHashCode()
                        ^ t.Action.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
