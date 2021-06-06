using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Rebalancing.Integrations
{
    public class RapidApiYahooFinanceClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<RapidApiYahooFinanceClient> _logger;

        private readonly string _keyHeader;
        private readonly string _hostHeader;

        public RapidApiYahooFinanceClient(HttpClient client, ILogger<RapidApiYahooFinanceClient> logger, IConfiguration config)
        {
            var baseUrl = config["RapidApiYahooFinance:Url"];
            _keyHeader = config["RapidApiYahooFinance:Key"];
            _hostHeader = config["RapidApiYahooFinance:Host"];

            _client = client;
            _client.BaseAddress = new Uri(baseUrl);
            _logger = logger;
        }

        public async Task<RapidApiYahooFinanceResponse> GetQuote(RapidApiYahooFinanceRequest request)
        {
            try
            {
                var requestUrl = new Uri($"market/v2/get-quotes?region=US&symbols={request}", UriKind.Relative);
                _logger.LogDebug($"HttpClient: Loading {requestUrl}");

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                requestMessage.Headers.Add("x-rapidapi-key", _keyHeader);
                requestMessage.Headers.Add("x-rapidapi-host", _hostHeader);

                using var response = await _client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                var quote = JsonConvert.DeserializeObject<RapidApiYahooFinanceResponse>(responseContent);
                return quote;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occurred connecting to Rapid Api Yahoo Finance API {ex.ToString()}");
                throw;
            }
        }
    }
}
