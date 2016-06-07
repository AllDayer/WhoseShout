using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WhoseShout.Helpers;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure
{
    public class StoreManager : IStoreManager
    {

        public static MobileServiceClient MobileService { get; set; }

        /// <summary>
        /// Syncs all tables.
        /// </summary>
        /// <returns>The all async.</returns>
        /// <param name="syncUserSpecific">If set to <c>true</c> sync user specific.</param>
        public async Task<bool> SyncAllAsync(bool syncUserSpecific)
        {
            if (!IsInitialized)
                await InitializeAsync();

            var taskList = new List<Task<bool>>();
            taskList.Add(UserStore.SyncAsync());
            taskList.Add(FriendStore.SyncAsync());
            taskList.Add(FriendRequestStore.SyncAsync());
            taskList.Add(PurchaseStore.SyncAsync());
            taskList.Add(ShoutTrackerStore.SyncAsync());
            taskList.Add(ShoutUserStore.SyncAsync());


            var successes = await Task.WhenAll(taskList).ConfigureAwait(false);
            return successes.Any(x => !x);//if any were a failure.
        }

        /// <summary>
        /// Drops all tables from the database and updated DB Id
        /// </summary>
        /// <returns>The everything async.</returns>
        public Task DropEverythingAsync()
        {
            Keys.UpdateDatabaseId();
            UserStore.DropTable();
            FriendStore.DropTable();
            FriendRequestStore.DropTable();
            PurchaseStore.DropTable();
            ShoutTrackerStore.DropTable();
            ShoutUserStore.DropTable();
            IsInitialized = false;
            return Task.FromResult(true);
        }

        public bool IsInitialized { get; private set; }
        #region IStoreManager implementation
        object locker = new object();
        public async Task InitializeAsync()
        {
            MobileServiceSQLiteStore store;
            lock (locker)
            {

                if (IsInitialized)
                    return;

                IsInitialized = true;
                var dbId = Keys.DatabaseId;
                var path = $"syncstore{dbId}.db";
                MobileService = new MobileServiceClient(Keys.AzureServiceUrl);
                store = new MobileServiceSQLiteStore(path);

                store.DefineTable<Friend>();
                store.DefineTable<User>();
                store.DefineTable<FriendRequest>();
                store.DefineTable<Purchase>();
                store.DefineTable<ShoutTracker>();
                store.DefineTable<ShoutUser>();
            }

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler()).ConfigureAwait(false);

            //await LoadCachedTokenAsync().ConfigureAwait(false);

        }

        IFriendStore friendStore;
        public IFriendStore FriendStore => friendStore ?? (friendStore = ServiceLocator.Instance.Resolve<IFriendStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = ServiceLocator.Instance.Resolve<IUserStore>());

        IFriendRequestStore friendRequestStore;
        public IFriendRequestStore FriendRequestStore => friendRequestStore ?? (friendRequestStore = ServiceLocator.Instance.Resolve<IFriendRequestStore>());

        IPurchaseStore purchaseStore;
        public IPurchaseStore PurchaseStore => purchaseStore ?? (purchaseStore = ServiceLocator.Instance.Resolve<IPurchaseStore>());

        IShoutTrackerStore shoutTrackerStore;
        public IShoutTrackerStore ShoutTrackerStore => shoutTrackerStore ?? (shoutTrackerStore = ServiceLocator.Instance.Resolve<IShoutTrackerStore>());

        IShoutUserStore shoutUserStore;
        public IShoutUserStore ShoutUserStore => shoutUserStore ?? (shoutUserStore = ServiceLocator.Instance.Resolve<IShoutUserStore>());
        #endregion

        //public async Task<MobileServiceUser> LoginAsync(string username, string password)
        //{
        //    if (!IsInitialized)
        //    {
        //        await InitializeAsync();
        //    }

        //    var credentials = new JObject();
        //    credentials["email"] = username;
        //    credentials["password"] = password;

        //    MobileServiceUser user = await MobileService.LoginAsync("Xamarin", credentials);

        //    await CacheToken(user);

        //    return user;
        //}

        //public async Task LogoutAsync()
        //{
        //    if (!IsInitialized)
        //    {
        //        await InitializeAsync();
        //    }

        //    await MobileService.LogoutAsync();

        //    var settings = await ReadSettingsAsync();

        //    if (settings != null)
        //    {
        //        settings.AuthToken = string.Empty;
        //        settings.UserId = string.Empty;

        //        await SaveSettingsAsync(settings);
        //    }
        //}

        //async Task SaveSettingsAsync(StoreSettings settings) =>
        //    await MobileService.SyncContext.Store.UpsertAsync(nameof(StoreSettings), new[] { JObject.FromObject(settings) }, true);

        //async Task<StoreSettings> ReadSettingsAsync() =>
        //    (await MobileService.SyncContext.Store.LookupAsync(nameof(StoreSettings), StoreSettings.StoreSettingsId))?.ToObject<StoreSettings>();


        //async Task CacheToken(MobileServiceUser user)
        //{
        //    var settings = new StoreSettings
        //    {
        //        UserId = user.UserId,
        //        AuthToken = user.MobileServiceAuthenticationToken
        //    };

        //    await SaveSettingsAsync(settings);

        //}

        //async Task LoadCachedTokenAsync()
        //{
        //    StoreSettings settings = await ReadSettingsAsync();

        //    if (settings != null)
        //    {
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(settings.AuthToken) && JwtUtility.GetTokenExpiration(settings.AuthToken) > DateTime.UtcNow)
        //            {
        //                MobileService.CurrentUser = new MobileServiceUser(settings.UserId);
        //                MobileService.CurrentUser.MobileServiceAuthenticationToken = settings.AuthToken;
        //            }
        //        }
        //        catch (InvalidTokenException)
        //        {
        //            settings.AuthToken = string.Empty;
        //            settings.UserId = string.Empty;

        //            await SaveSettingsAsync(settings);
        //        }
        //    }
        //}

        //public class StoreSettings
        //{
        //    public const string StoreSettingsId = "store_settings";

        //    public StoreSettings()
        //    {
        //        Id = StoreSettingsId;
        //    }

        //    public string Id { get; set; }

        //    public string UserId { get; set; }

        //    public string AuthToken { get; set; }
        //}
    }
}
