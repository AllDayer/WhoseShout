using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoseShout.DataStore.Azure;
using WhoseShout.Models;

namespace WhoseShout.Core.ViewModels
{
    public class ShoutViewModel : ViewModelBase
    {
        public Guid ShoutId { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public List<Purchase> Purchases { get; set; }

        public MvxCommand PurchaseShoutCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    System.Console.WriteLine("Purchasing for: " + Name);

                    Purchase purchase = new Purchase()
                    {
                        UserId = CurrentApp.AppContext.UserProfile.UserId,
                        ShoutTrackerId = ShoutId,
                        PurchaseId = Guid.NewGuid(),
                    };

                    bool allowPurchase = true;
                    if(allowPurchase)
                    {
                        StoreManager.PurchaseStore.InsertAsync(purchase);
                    }
                });
            }
        }


        public ShoutViewModel()
        {
            Users = new List<User>();
            Purchases = new List<Purchase>();
        }

        public async Task Initialise(Guid shoutId)
        {
            ShoutTracker shout = await StoreManager.ShoutTrackerStore.GetShout(shoutId);
            Name = shout.Name;

            var usersInShout = await StoreManager.ShoutUserStore.GetShoutUsers(shoutId);
            foreach (var u in usersInShout)
            {
                var usr = await StoreManager.UserStore.GetUser(u.UserId);
                Users.Add(usr);
            }

            var purchases = await StoreManager.PurchaseStore.GetPurchasesForShout(shoutId);
            foreach (var p in purchases)
            {
                Purchases.Add(p);
            }
        }
    }
}