using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rebalancing.Core;

namespace Rebalancing.Data.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction> { }

    public class TransactionRepository
        : EfCoreRepository<Transaction, RebalancingDbContext>
        , ITransactionRepository
    {
        public TransactionRepository(RebalancingDbContext context) : base(context)
        { }

        //public override List<Transaction> Get()
        //{
        //    return Context.Transactions
        //        //.Include(x => x.Symbol)
        //        .ToList();
        //}
    }
}
