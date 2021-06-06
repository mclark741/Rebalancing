using System;
using CsvHelper.Configuration;

namespace Rebalancing.Import
{
    public class FidelityTransaction
    {
        public DateTime RunDate { get; set; }
        public string Action { get; set; }
        public string Symbol { get; set; }
        public string SecurityDescription { get; set; }
        public string SecurityType { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Commission { get; set; }
        public string Fees { get; set; }
        public string AccruedInterest { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? SettlementDate { get; set; }
    }

    public class FidelityTransactionMap : ClassMap<FidelityTransaction>
    {
        public FidelityTransactionMap()
        {
            Map(m => m.RunDate).Name("Run Date");
            Map(m => m.Action).Name("Action");
            Map(m => m.Symbol).Name("Symbol");
            Map(m => m.SecurityDescription).Name("Security Description");
            Map(m => m.SecurityType).Name("Security Type");
            Map(m => m.Quantity).Name("Quantity");
            Map(m => m.Price).Name("Price ($)");
            Map(m => m.Commission).Name("Commission ($)");
            Map(m => m.Fees).Name("Fees ($)");
            Map(m => m.AccruedInterest).Name("Accrued Interest ($)");
            Map(m => m.Amount).Name("Amount ($)");
            Map(m => m.SettlementDate).Name("Settlement Date");
        }
    }
}
