using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoseShout.Core;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure
{
    public class FriendStore : BaseStore<Friend>, IFriendStore
    {

        public async Task<IEnumerable<Friend>> GetAllFriends(string userId)
        {
            await InitializeStore().ConfigureAwait(false);
            var friends = await GetItemsAsync().ConfigureAwait(false);
            return friends.Where(f => f.UserId == userId);
        }
    }
}