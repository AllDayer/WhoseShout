using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure
{
    public class ShoutUserStore : BaseStore<ShoutUser>, IShoutUserStore
    {
        public async Task<IEnumerable<ShoutUser>> GetShoutUsers(Guid shoutId)
        {
            await InitializeStore().ConfigureAwait(false);
            var users = await GetItemsAsync().ConfigureAwait(false);
            return users.Where(x => x.ShoutId == shoutId);
        }


        public async Task<IEnumerable<ShoutUser>> GetShoutsForUser(String userId)
        {
            await InitializeStore().ConfigureAwait(false);
            var users = await GetItemsAsync().ConfigureAwait(false);
            return users.Where(x => x.UserId == userId);
        }
    }
}