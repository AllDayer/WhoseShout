using System;
using System.Threading.Tasks;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();


        IFriendStore FriendStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();
    }
}