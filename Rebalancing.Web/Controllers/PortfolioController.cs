using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Rebalancing.Core;
using Rebalancing.Data.Repositories;

namespace Rebalancing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolio _portfolio;
        private readonly ITransactionRepository _transactionRepository;

        public PortfolioController(IPortfolio portfolio, ITransactionRepository transactionRepository)
        {
            _portfolio = portfolio;
            _transactionRepository = transactionRepository;
        }

        // GET: api/Portfolio
        [HttpGet]
        public IEnumerable<CurrentPosition> Get()
        {
            var transactions = _transactionRepository.Get();

            _portfolio.AddTransactions(transactions);

            return _portfolio.Positions.Where(x => x.Quantity > 0).OrderBy(x => x.Symbol);
        }

        // PUT: api/Portfolio/100.50
        [HttpPut("{additionalInvestment:decimal?}")]
        public IEnumerable<Transaction> Rebalance([FromBody] IEnumerable<DesiredPosition> desiredPositions, decimal additionalInvestment)
        {
            var transactions = _transactionRepository.Get();

            transactions.ForEach(_portfolio.AddTransaction);

            return _portfolio.Rebalance(desiredPositions, additionalInvestment);
        }


        // PUT: api/Portfolio/Exchange/100.50
        [HttpPut("exchange/{additionalInvestment:decimal?}")]
        public IEnumerable<TransactionExchange> RebalanceExchange([FromBody] IEnumerable<DesiredPosition> desiredPositions, decimal additionalInvestment)
        {
            var transactions = Rebalance(desiredPositions, additionalInvestment);
            return _portfolio.Format(transactions);
        }
    }
}
