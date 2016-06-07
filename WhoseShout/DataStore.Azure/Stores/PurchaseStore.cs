using WhoseShout.Models;
using WhoseShout.DataStore.Azure.Interfaces;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WhoseShout.DataStore.Azure
{
    public class PurchaseStore : BaseStore<Purchase>, IPurchaseStore
    {
        public async Task<IEnumerable<Purchase>> GetPurchasesForShout(Guid shoutId)
        {
            await InitializeStore().ConfigureAwait(false);
            var purchases = await GetItemsAsync().ConfigureAwait(false);
            return purchases.Where(f => f.ShoutTrackerId == shoutId);
        }
        
    }
}