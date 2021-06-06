using System;
namespace Rebalancing.Data.Entities
{
    public class PositionEntity: RebalancingEntity
    {
        public decimal PercentOfAccount { get; set; }

        public virtual SecurityEntity Security { get; set; }
    }
}
