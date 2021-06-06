using System;
namespace Rebalancing.Data.Entities
{
    public class TransactionEntity : RebalancingEntity
    {
        public decimal Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime SettlementDate { get; set; }

        public virtual SecurityEntity Security { get; set; }
        public virtual ActionEntity Action { get; set; }
    }
}
