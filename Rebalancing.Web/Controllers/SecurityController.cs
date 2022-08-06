using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rebalancing.Core;
using Rebalancing.Data.Repositories;

namespace Rebalancing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IMarket _market;
        private readonly ISecurityRepository _securityRepository;

        public SecurityController(IMarket market, ISecurityRepository securityRepository)
        {
            _market = market;
            _securityRepository = securityRepository;
        }

        // GET: api/Security
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Security> GetAll()
        {
            return  _securityRepository.Get();
        }

        // GET: api/Security/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Security GetById(int id)
        {
            return  _securityRepository.Get(id);
        }

        // GET: api/Security/AAPL[,TSLA,GOOGL]

        /// <summary>
        /// Gets quotes for the provided symbol(s)
        /// </summary>
        /// <param name="symbols">A single symbol or comma separated list of symbols</param>
        /// <returns></returns>
        [HttpGet("{symbols}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Security> GetBySymbol(string symbols)
        {
            string symbolsUpper = symbols.ToUpperInvariant();
            string[] symbolsSplit;
            if (symbolsUpper.Contains(','))
            {
                symbolsSplit = symbolsUpper.Split(",", StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                symbolsSplit = new[] { symbolsUpper };
            }

            var marketSecurities = _market.GetSecurities(symbolsSplit);

            return marketSecurities;
        }

        //// POST: api/Security
        //[HttpPost]
        //public async Task<Security> Create([FromBody] Security security)
        //{
        //    return await _securityRepository.Add(security);
        //}

        // POST: api/Security
        [HttpPost(Name = "Multiple")]
        public IEnumerable<Security> CreateMultiple([FromBody] IEnumerable<Security> securities)
        {
            return _securityRepository.Add(securities);
        }

        // PUT: api/Security/5
        [HttpPut("{id}")]
        public  Security UpdateById(int id, [FromBody] Security security)
        {
            return _securityRepository.Update(security);
        }

        // PUT: api/Security/AAPL
        [HttpPut("{symbol}")]
        public  Security UpdateBySymbol(string symbol, [FromBody] Security security)
        {
            return _securityRepository.Update(security);
        }

        // DELETE: api/Security/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
