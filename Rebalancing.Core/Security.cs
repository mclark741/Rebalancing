using System;
namespace Rebalancing.Core
{
    public class Security
    {
        public int SecurityId { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public override string ToString()
        {
            return $"{Symbol} - {Description}";
        }
    }
}
