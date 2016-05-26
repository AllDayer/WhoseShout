using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure
{
    public class FriendRequestStore : BaseStore<FriendRequest>, IFriendRequestStore
    {
        public async Task<bool> AddFriendRequest(string userId, string futureFriendId)
        {
            await InitializeStore().ConfigureAwait(false);
            var items = await Table.Where(fr => fr.UserId == userId && fr.FutureFriendId == futureFriendId).ToListAsync().ConfigureAwait(false);
            if (items.Count == 0)
            {
                return await this.InsertAsync(new FriendRequest() { UserId = userId, FutureFriendId = futureFriendId }).ConfigureAwait(false);
            }

            return false;
        }

    }
}