using System;
using System.Threading.Tasks;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();

        IUserStore UserStore { get; }
        IFriendStore FriendStore { get; }
        IFriendRequestStore FriendRequestStore { get; }
        IPurchaseStore PurchaseStore { get; }
        IShoutTrackerStore ShoutTrackerStore { get; }
        IShoutUserStore ShoutUserStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();
    }
}