using System.Collections.Generic;

namespace Rebalancing.Core
{
    public interface IMarket
    {
        IEnumerable<Security> GetSecurities(params string[] symbol);
    }
}