using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rebalancing.Core;

namespace Rebalancing.Data.Repositories
{
    public interface ISecurityRepository : IRepository<Security>
    {
        Security Get(string symbol);
        IEnumerable<Security> Get(IEnumerable<string> symbols);
    }

    public class SecurityRepository
        : EfCoreRepository<Security, RebalancingDbContext>
        , ISecurityRepository
    {
        public SecurityRepository(RebalancingDbContext context) : base(context)
        { }

        public Security Get(string symbol)
        {
            return Get(x => x.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public IEnumerable<Security> Get(IEnumerable<string> symbols)
        {
            return Get(x => symbols.Any(s => x.Symbol.Equals(s, StringComparison.OrdinalIgnoreCase)));
        }

        //private readonly List<Security> _marketData = new List<Security>
        //{
        //    new Security { Symbol = "FLPSX", Description = "Fidelity Low-Priced Stock Fund", Price = 51.41M, LastUpdateDate = DateTime.Now },
        //    new Security { Symbol = "FPADX", Description = "Fidelity Emerging Markets Index Fund", Price = 13.55M, LastUpdateDate = DateTime.Now },
        //    new Security { Symbol = "FRESX", Description = "Fidelity Real Estate Investment Portfolio", Price = 38.41M, LastUpdateDate = DateTime.Now },
        //    new Security { Symbol = "FSPSX", Description = "Fidelity International Index Fund", Price = 47.04M, LastUpdateDate = DateTime.Now},
        //    new Security { Symbol = "FSSNX", Description = "Fidelity Small Cap Index Fund", Price = 27.27M, LastUpdateDate = DateTime.Now },
        //    new Security { Symbol = "FSSNX", Description = "T Rowe Price Blue Chip Growth Fund", Price = 163.79M, LastUpdateDate = DateTime.Now },
        //    //new Security { Symbol = "CASH", Description = "CASH", Price = 1.00M }
        //};

        //var security = _marketData
        //            .Where(x => symbols
        //            .Any(s => x.Symbol.Trim().Equals(s.Trim(), StringComparison.OrdinalIgnoreCase)));
        //        return security;
    }
}