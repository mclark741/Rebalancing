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
                return securities;
            });

            return securities;
        }


        private IEnumerable<Security> GetOrCreateOrUpdate(string[] symbols, Func<IEnumerable<Security>> createSecurity)
        {
            // fetch all securities from the database
            var databaseSecurities = _securityRepository.Get(symbols).ToList();
            List<Security> securities = databaseSecurities.ToList();

            // make sure the price is current as of midnight last night
            var isExpired = databaseSecurities.Any(db => db.LastUpdateDate < DateTime.Today);

            // check to see if there are any new securities in the request that are not in the database
            var allSymbolsFoundInDatabase = symbols.All(s => databaseSecurities.Select(x => x.Symbol).Contains(s, StringComparer.OrdinalIgnoreCase));

            if (_makeMarketWebCalls && (isExpired || !allSymbolsFoundInDatabase))
            {
                // execute the delegate to get current Market pricing
                var marketSecurities = createSecurity().ToList();

                // update existing securities
                databaseSecurities.ToList().ForEach(d =>
                {
                    var m = marketSecurities.FirstOrDefault(x => x.Symbol.Equals(d.Symbol, StringComparison.OrdinalIgnoreCase));
                    d.Price = m.Price;
                    d.LastUpdateDate = DateTime.Now;
                });
                _securityRepository.Update(databaseSecurities);

                // add new securities
                var securitiesToAdd = marketSecurities.Except(databaseSecurities, new SecuritySymbolEqualityComparer()).ToList();
                if (securitiesToAdd.Any())
                {
                    _securityRepository.Add(securitiesToAdd);
                    securities.AddRange(securitiesToAdd);
                }
            }

            return securities;
        }
    }
}
