using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IFriendRequestStore : IBaseStore<FriendRequest>
    {
        Task<bool> AddFriendRequest(string userId, string futureFriendId);
        Task<bool> AcceptFriendRequest(string userId, string futureFriendId);
        Task<bool> RejectFriendRequest(string userId, string futureFriendId);
        Task<IEnumerable<FriendRequest>> PendingFriendRequests(string userId);
    }
}