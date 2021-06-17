namespace Rebalancing.Core
{
    public class TransactionExchange
    {
        public string SellSymbol { get; set; }
        public string BuySymbol { get; set; }
        public decimal TotalAmount { get; set; }

        public override string ToString()
        {
            return $"Exchange {SellSymbol} for {BuySymbol}: {TotalAmount}";
        }
    }
}
