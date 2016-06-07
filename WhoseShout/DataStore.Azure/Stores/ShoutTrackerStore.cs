using System;
using System.Linq;
using System.Threading.Tasks;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure
{
    public class ShoutTrackerStore : BaseStore<ShoutTracker>, IShoutTrackerStore
    {
        public async Task<ShoutTracker> GetShout(Guid shoutId)
        {
            await InitializeStore().ConfigureAwait(false);
            var shouts = await GetItemsAsync().ConfigureAwait(false);
            return shouts.FirstOrDefault(x => x.ShoutId == shoutId);
        }
    }
}