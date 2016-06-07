using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.DataStore.Azure;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IPurchaseStore : IBaseStore<Purchase>
    {
        Task<IEnumerable<Purchase>> GetPurchasesForShout(Guid shoutId);
    }
}