using System;
using System.Collections.Generic;

using MvvmCross.Core.ViewModels;
using WhoseShout.Helpers;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.DataStore.Azure;

namespace WhoseShout.Core.ViewModels
{
    public class ViewModelBase : MvxViewModel
    {
        protected IStoreManager StoreManager { get; } = ServiceLocator.Instance.Resolve<IStoreManager>();

        public static void Init(bool mock = false)
        {
            ServiceLocator.Instance.Add<IStoreManager, StoreManager>();
            ServiceLocator.Instance.Add<IFriendStore, FriendStore>();

        }
    }
}