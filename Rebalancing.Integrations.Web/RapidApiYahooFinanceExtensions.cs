using System;
using System.Collections.Generic;
using System.Linq;
using Rebalancing.Core;

namespace Rebalancing.Integrations
{
    public static class RapidApiYahooFinanceExtensions
    {
        public static Security ToSecurity(this Result result)
        {
            var security = new Security
            {
                Symbol = result.Symbol,
                Description = result.LongName,
                Price = Convert.ToDecimal(result.RegularMarketPrice),
                LastUpdateDate = DateTime.Now
            };

            return security;
        }

        public static IEnumerable<Security> GetSecurities(this RapidApiYahooFinanceResponse response)
        {
            return response.QuoteResponse.Result.Select(x => x.ToSecurity());
        }
    }
}
