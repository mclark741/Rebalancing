using System;
namespace Rebalancing.Core
{
    public class DesiredPosition
    {
        public int PositionId { get; set; }
        public decimal PercentOfAccount { get; set; }
        public string Symbol { get; set; }

        public override string ToString()
        {
            return $"{Symbol}: {PercentOfAccount:P3}";
        }
    }

    public class CurrentPosition : DesiredPosition
    {
        public decimal Quantity { get; set; }

        public decimal CurrentValue => Math.Round(Security.Price * Quantity, 2);
        public decimal PercentOfAccountRounded => Math.Round(PercentOfAccount * 100, 2);
        public new string Symbol => Security?.Symbol;

        public virtual Security Security { get; set; }

        public override string ToString()
        {
            return $"{Security.Symbol}: {Quantity}; {CurrentValue:C}; {PercentOfAccount:P3}";
        }
    }
}
