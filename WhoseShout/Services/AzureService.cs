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

        IMobileServiceSyncTable<User> userTable;
        IMobileServiceSyncTable<Friend> friendTable;
        IMobileServiceSyncTable<FriendRequest> friendRequestTable;

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

            var store = new MobileServiceSQLiteStore("whoseshoutdb.db");
            store.DefineTable<User>();
            store.DefineTable<Friend>();
            store.DefineTable<FriendRequest>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            userTable = MobileService.GetSyncTable<User>();
            friendTable = MobileService.GetSyncTable<Friend>();
            friendRequestTable = MobileService.GetSyncTable<FriendRequest>();

            m_IsInitialised = true;

        }

        public async Task SyncUsers(String userId)
        {
            try
            {
                await MobileService.SyncContext.PushAsync();
                await userTable.PurgeAsync();
                await userTable.PullAsync("allUsers" + userId, userTable.CreateQuery().Where(u => u.UserId == userId));
            }
            catch
            {
                System.Console.WriteLine("Could not fetch user");
            }
        }

        public async Task<User> AddUser(String userId, String name, String email)
        {
            await Initialize();
            var item = new User
            {
                UserId = userId,
                Name = name,
                Email = email
            };

            await SyncUsers(userId);

            List<User> exists = await userTable.ToListAsync();

            //foreach(var e in exists)
            //{
            //    await DeleteUser(e);
            //}

            if (exists.Count == 0) // This is shit
            {
                await userTable.InsertAsync(item);
            }
            //Synchronize friends
            await SyncFriends(userId);
            return item;
        }

        public async Task<bool> DeleteUser(User user)
        {
            await Initialize();
            try
            {
                await userTable.DeleteAsync(user);

                return true;
            }
            catch
            {
                return false;
            }
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

        public async Task<IEnumerable<Friend>> GetFriends(String userId)
        {
            await Initialize();
            await SyncFriends(userId);
            return await friendTable.ToEnumerableAsync();
        }

        public async Task<Friend> AddFriend(String userId, String friendId)
        {

            await Initialize();
            var item = new Friend
            {
                UserId = userId,
                FriendId = friendId,
                //Name = name//Remove this
            };

            await friendTable.InsertAsync(item);
            //Synchronize friends
            await SyncFriends(userId);
            return item;
        }

        public async Task<Friend> UpdateFriend(Friend friend)
        {
            await Initialize();

            await friendTable.UpdateAsync(friend);

            await SyncFriends(friend.UserId);
            return friend;
        }

        public async Task<bool> DeleteFriend(Friend friend)
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

        public async Task SyncFriendRequests(String userId)
        {
            try
            {
                await MobileService.SyncContext.PushAsync();
                await friendRequestTable.PurgeAsync();
                await friendRequestTable.PullAsync("allFriendRequests" + userId, friendRequestTable.CreateQuery().Where(u => u.UserId == userId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync items " + ex);
            }
        }


        public async Task<IEnumerable<FriendRequest>> GetFriendRequests(String userId)
        {
            await Initialize();
            await SyncFriendRequests(userId);
            return await friendRequestTable.ToEnumerableAsync();
        }

        public async Task<FriendRequest> AddFriendRequest(String userId, String futureFriendId)
        {
            await Initialize();
            var item = new FriendRequest
            {
                UserId = userId,
                FutureFriendId = futureFriendId,
            };

            await SyncFriendRequests(userId);

            List<FriendRequest> exists = await friendRequestTable.Where(u => u.UserId == userId && u.FutureFriendId == futureFriendId).ToListAsync();
            if (exists.Count == 0) // This is shit
            {
                await friendRequestTable.InsertAsync(item);
            }

            await friendRequestTable.InsertAsync(item);
            //Synchronize friendsrequests
            await SyncFriendRequests(userId);
            return item;
        }

        public async Task<FriendRequest> UpdateFriendRequest(FriendRequest friendRequest)
        {
            await Initialize();

            await friendRequestTable.UpdateAsync(friendRequest);

            if (friendRequest.ApproveFlag)
            {
                await AddFriend(friendRequest.UserId, friendRequest.FutureFriendId);
                await AddFriend(friendRequest.FutureFriendId, friendRequest.UserId);
            }

            await SyncFriendRequests(friendRequest.UserId);
            return friendRequest;
        }
    }
}