using System.Collections.Generic;

namespace Rebalancing.Integrations
{
    public class RapidApiYahooFinanceRequest
    {
        public RapidApiYahooFinanceRequest()
        { }

        public RapidApiYahooFinanceRequest(params string[] symbols)
        {
            Symbols = symbols;
        }

        public IEnumerable<string> Symbols { get; set; }

        public override string ToString()
        {
            return string.Join(",", Symbols);
        }
    }
}

