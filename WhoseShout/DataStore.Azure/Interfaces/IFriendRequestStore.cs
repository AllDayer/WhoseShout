using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IFriendRequestStore : IBaseStore<FriendRequest>
    {
        Task<bool> AddFriendRequest(string userId, string futureFriendId);
    }
}