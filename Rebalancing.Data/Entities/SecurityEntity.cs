using System;
namespace Rebalancing.Data.Entities
{
    public class SecurityEntity : RebalancingEntity
    {
        public string Symbol { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
