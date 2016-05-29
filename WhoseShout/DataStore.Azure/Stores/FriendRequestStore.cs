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

        private async Task<bool> UpdateFriendRequest(string userId, string futureFriendId, bool approved, bool rejected, bool block, bool spam)
        {
            await InitializeStore().ConfigureAwait(false);
            var items = await Table.Where(fr => (fr.UserId == userId && fr.FutureFriendId == futureFriendId) ||
                                                (fr.UserId == futureFriendId && fr.FutureFriendId == userId)).ToListAsync().ConfigureAwait(false);
            if (items.Count == 0)
            {
                return false;//What has happened
            }

            foreach (var fr in items)
            {
                if (approved)
                {
                    fr.ApproveFlag = true;
                }
                else if (rejected)
                {
                    fr.RejectFlag = true;
                }
                else if (block)
                {
                    fr.BlockFlag = true;
                }
                else if (spam)
                {
                    fr.SpamFlag = true;
                }

                await this.UpdateAsync(fr).ConfigureAwait(false);
            }

            return true;
        }

        public async Task<bool> AcceptFriendRequest(string userId, string futureFriendId)
        {
            return await UpdateFriendRequest(userId, futureFriendId, true, false, false, false);
        }
        public async Task<bool> RejectFriendRequest(string userId, string futureFriendId)
        {
            return await UpdateFriendRequest(userId, futureFriendId, false, true, false, false);
        }

        public async Task<IEnumerable<FriendRequest>> PendingFriendRequests(string userId)
        {
            await InitializeStore().ConfigureAwait(false);
            var friendRequests = await GetItemsAsync(true).ConfigureAwait(false);
            return friendRequests.Where(f => f.FutureFriendId == userId && (f.ApproveFlag == false && f.RejectFlag == false && f.BlockFlag == false && f.SpamFlag == false)) ;
        }

    }
}