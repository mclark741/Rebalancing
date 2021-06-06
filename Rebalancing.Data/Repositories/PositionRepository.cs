using System;
using Rebalancing.Core;

namespace Rebalancing.Data.Repositories
{
    public interface IPositionRepository : IRepository<DesiredPosition> { }

    public class PositionRepository
        : EfCoreRepository<DesiredPosition, RebalancingDbContext>
        , IPositionRepository
    {
        public PositionRepository(RebalancingDbContext context) : base(context)
        { }
    }
}
