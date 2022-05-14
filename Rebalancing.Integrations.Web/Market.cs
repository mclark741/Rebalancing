using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rebalancing.Core;
using Rebalancing.Data.Repositories;

namespace Rebalancing.Integrations
{
    public class Market : IMarket
    {
        private readonly ISecurityRepository _securityRepository;
        private readonly RapidApiYahooFinanceClient _client;
        private readonly bool _makeMarketWebCalls = false;

        public Market(ISecurityRepository securityRepository,
                      RapidApiYahooFinanceClient client,
                      IConfiguration config)
        {
            _securityRepository = securityRepository;
            _client = client;

            bool.TryParse(config["MakeMarketWebCalls"], out _makeMarketWebCalls);
        }

        public IEnumerable<Security> GetSecurities(params string[] symbols)
        {
            var securities = GetOrCreateOrUpdate(symbols, () =>
            {
                var request = new RapidApiYahooFinanceRequest(symbols);
                var quote = _client.GetQuote(request);
                var securities = quote.Result.GetSecurities();

                //var strResponse = "[{\"securityId\": 0, \"symbol\": \"FLPSX\", \"description\": \"Fidelity Low-Priced Stock Fund\", \"price\": 52.17, \"lastUpdateDate\": \"2021-02-14T08:41:44.655561-06:00\"}, {\"securityId\": 0, \"symbol\": \"FPADX\", \"description\": \"Fidelity Emerging Markets Index Fund\", \"price\": 14.2, \"lastUpdateDate\": \"2021-02-14T08:41:44.655588-06:00\"}, {\"securityId\": 0, \"symbol\": \"FRESX\", \"description\": \"Fidelity Real Estate Investment Portfolio\", \"price\": 41.03, \"lastUpdateDate\": \"2021-02-14T08:41:44.655592-06:00\"}, {\"securityId\": 0, \"symbol\": \"FSPSX\", \"description\": \"Fidelity International Index Fund\", \"price\": 47.5, \"lastUpdateDate\": \"2021-02-14T08:41:44.655597-06:00\"}, {\"securityId\": 0, \"symbol\": \"FSSNX\", \"description\": \"Fidelity Small Cap Index Fund\", \"price\": 28.98, \"lastUpdateDate\": \"2021-02-14T08:41:44.6556-06:00\"}, {\"securityId\": 0, \"symbol\": \"TBCIX\", \"description\": \"T. Rowe Price Blue Chip Growth Fund I Class\", \"price\": 176.6, \"lastUpdateDate\": \"2021-02-14T08:41:44.655605-06:00\"} ]";

                //var securities = JsonConvert.DeserializeObject<IEnumerable<Security>>(strResponse);


                return securities;
            });

            return securities;
        }


        private IEnumerable<Security> GetOrCreateOrUpdate(string[] symbols, Func<IEnumerable<Security>> createSecurity)
        {
            var databaseSecurities = _securityRepository.Get(symbols);

            var isExpired = databaseSecurities.Any(db => db.LastUpdateDate < DateTime.Today);

            List<Security> securities = databaseSecurities.ToList();

            var previouslyDownloadedAllSecurities = symbols.All(s => databaseSecurities.Select(x => x.Symbol).Contains(s));

            if (_makeMarketWebCalls && (isExpired || !databaseSecurities.Any() || !previouslyDownloadedAllSecurities))
            {
                var marketSecurities = createSecurity().ToList();
                securities = marketSecurities;
                if (databaseSecurities?.Any() == false)
                {
                    // TODO: fix the case where we need insert some and update some
                    _securityRepository.Add(marketSecurities);
                }
                else
                {
                    databaseSecurities.ToList().ForEach(d =>
                    {
                        var m = marketSecurities.FirstOrDefault(x => x.Symbol.Equals(d.Symbol, StringComparison.OrdinalIgnoreCase));
                        d.Price = m.Price;
                        d.LastUpdateDate = DateTime.Now;
                    });

                    _securityRepository.Update(databaseSecurities);
                }
            }

            return securities;
        }
    }
}
