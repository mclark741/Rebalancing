using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rebalancing.Core;
using Rebalancing.Data.Repositories;
using Rebalancing.Import;

namespace Rebalancing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IFileImport _fileImport;
        private readonly IMarket _market;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(IFileImport fileImport, IMarket market, ITransactionRepository transactionRepository)
        {
            _fileImport = fileImport;
            _market = market;
            _transactionRepository = transactionRepository;
        }

        // GET: api/Transaction
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _transactionRepository.Get().OrderBy(x => x.TransactionDate);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}", Name = "Get")]
        public Transaction Get(int id)
        {
            return _transactionRepository.Get(id);
        }

        // POST: api/Transaction
        [HttpPost]
        public Transaction Post([FromBody] Transaction value)
        {
            return _transactionRepository.Add(value);
        }

        // POST: api/Transaction
        [HttpPost]
        [Route("file")]
        public IEnumerable<Transaction> Post(IFormFile file)
        {
            using (var readStream = file.OpenReadStream())
            {
                var transactionsToImport = _fileImport.Import(readStream).Where(x => x.Action != Core.Action.None).ToList();
                var securities = _market.GetSecurities(transactionsToImport
                    .Where(x => !string.IsNullOrWhiteSpace(x.Symbol))
                    .Select(x => x.Symbol.Trim()).Distinct().ToArray());
                transactionsToImport.ForEach(t => t.Symbol = securities.First(s => s.Symbol == t.Symbol).Symbol);


                var allTransactions = _transactionRepository.Get();

                IEnumerable<Transaction> transactionsToAdd = transactionsToImport.Except(allTransactions, new TransactionEqualityComparer());

                return _transactionRepository.Add(transactionsToAdd);
                //return transactions;
            }
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public Transaction Put(int id, [FromBody] Transaction value)
        {
            return _transactionRepository.Update(value);
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
