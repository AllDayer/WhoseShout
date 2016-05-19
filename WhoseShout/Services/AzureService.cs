using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using WhoseShout.Models;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace WhoseShout.Services
{
    public class AzureService : IService
    {
        public MobileServiceClient MobileService { get; set; }

        IMobileServiceSyncTable<Friend> friendTable;

        bool m_IsInitialised;

        public Task Initialize()
        {
            if (m_IsInitialised)
            {
                return;
            }

            MobileService = new MobileServiceClient("AzureKey", null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };

            var store = new MobileServiceSQLiteStore("whoseshout.db");
            store.DefineTable<Friend>();
        }

        public Task<IEnumerable<Friend>> GetFriends()
        {
            throw new NotImplementedException();
        }

        public Task<Friend> AddFriend(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Friend> UpdateFriend(Friend friend)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFriend(Friend friend)
        {
            throw new NotImplementedException();
        }

        public Task SyncFriends()
        {
            throw new NotImplementedException();
        }
    }
}