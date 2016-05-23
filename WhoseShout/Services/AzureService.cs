using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using WhoseShout.Models;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using System.Diagnostics;

namespace WhoseShout.Services
{
    public class AzureService : IService
    {
        public MobileServiceClient MobileService { get; set; }

        IMobileServiceSyncTable<UserItem> userTable;
        IMobileServiceSyncTable<FriendItem> friendTable;

        bool m_IsInitialised;

        public async Task Initialize()
        {
            if (m_IsInitialised)
            {
                return;
            }

            MobileService = new MobileServiceClient(new Uri(Helpers.Keys.AzureServiceUrl).ToString(), null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };

            var store = new MobileServiceSQLiteStore("whoseshout.db");
            store.DefineTable<UserItem>();
            store.DefineTable<FriendItem>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            userTable = MobileService.GetSyncTable<UserItem>();
            friendTable = MobileService.GetSyncTable<FriendItem>();

            m_IsInitialised = true;

        }

        public async Task SyncFriends(String userId)
        {
            //var key = Helpers.Keys.AzureServiceUrl;
            //var connected = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable(key);
            //if (connected == false)
            //{
            //    return;
            //}

            try
            {
                await MobileService.SyncContext.PushAsync();
                await friendTable.PurgeAsync();
                await friendTable.PullAsync("allFriendItems" + userId, friendTable.CreateQuery().Where(u => u.UserId == userId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync items, that is alright as we have offline capabilities: " + ex);
            }
        }

        public async Task<UserItem> AddUser(String userId, string name)
        {
            await Initialize();
            var item = new UserItem
            {
                UserId = userId,
                Name = name
            };

            await userTable.InsertAsync(item);
            //Synchronize friends
            await SyncFriends(userId);
            return item;
        }

        public async Task<IEnumerable<FriendItem>> GetFriends(String userId)
        {
            await Initialize();
            await SyncFriends(userId);
            return await friendTable.ToEnumerableAsync();
        }

        public async Task<FriendItem> AddFriend(String userId, String friendId, string name)
        {

            await Initialize();
            var item = new FriendItem
            { 
                UserId = userId,
                FriendId = friendId,
                Name = name//Remove this
            };

            await friendTable.InsertAsync(item);
            //Synchronize friends
            await SyncFriends(userId);
            return item;
        }

        public async Task<FriendItem> UpdateFriend(FriendItem friend)
        {
            await Initialize();

            await friendTable.UpdateAsync(friend);
            
            await SyncFriends(friend.UserId);
            return friend;
        }

        public async Task<bool> DeleteFriend(FriendItem friend)
        {
            await Initialize();
            try
            {
                await friendTable.DeleteAsync(friend);
                await SyncFriends(friend.UserId);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}