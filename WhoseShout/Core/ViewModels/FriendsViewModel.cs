using System;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;
using WhoseShout.Helpers;
using WhoseShout.Services;
using WhoseShout.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using WhoseShout.Resources;
using System.Collections.Generic;
using System.Linq;

namespace WhoseShout.Core.ViewModels
{
    public class FriendsViewModel : ViewModelBase
    {
        IService m_Service;

        public ICommand SearchCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    var searchResult = await StoreManager.UserStore.FindFriends(FriendsSearch);
                    FriendSearchList.Clear();

                    foreach (var v in searchResult)
                    {
                        //UserSearch.Add(v);
                        FriendSearchList.Add(new FriendSearchWrapper(v, this));
                    }

                    await ExecuteRefreshCommand();
                });
            }
        }


        private IMvxCommand m_AddFriendRequestCommand;
        public IMvxCommand AddFriendRequestCommand
        {
            get
            {
                m_AddFriendRequestCommand = m_AddFriendRequestCommand ?? new MvxCommand<FriendSearchWrapper>(AddFriendRequest);
                return m_AddFriendRequestCommand;

                //return new MvxCommand<FriendSearchWrapper>(async () =>
                //{
                //    await AddFriendRequest();
                //});
            }
        }


        ObservableCollection<FriendWrapper> m_Friends = new ObservableCollection<FriendWrapper>();
        public ObservableCollection<FriendWrapper> Friends
        {
            get
            {
                return m_Friends;
            }
            set
            {
                m_Friends = value;
                RaisePropertyChanged(nameof(Friends));
            }
        }

        private ObservableCollection<FriendSearchWrapper> m_FriendSearchList = new ObservableCollection<FriendSearchWrapper>();
        public ObservableCollection<FriendSearchWrapper> FriendSearchList
        {
            get
            {
                return m_FriendSearchList;
            }
            set
            {
                m_FriendSearchList = value;
                RaisePropertyChanged(nameof(FriendSearchList));
            }
        }


        ObservableCollection<FriendRequestWrapper> m_FriendRequestList = new ObservableCollection<FriendRequestWrapper>();
        public ObservableCollection<FriendRequestWrapper> FriendRequestList
        {
            get
            {
                return m_FriendRequestList;
            }
            set
            {
                m_FriendRequestList = value;
            }
        }

        private string m_FriendsSearch = "n";
        public string FriendsSearch
        {
            get
            {
                return m_FriendsSearch;
            }
            set
            {
                m_FriendsSearch = value;
                RaisePropertyChanged(nameof(FriendsSearch));
            }
        }

        private bool m_HasFriendRequests = false;
        public bool HasFriendRequests
        {
            get
            {
                return m_HasFriendRequests;
            }
            set
            {
                m_HasFriendRequests = value;
                RaisePropertyChanged(nameof(HasFriendRequests));
            }
        }

        public FriendsViewModel() : base()
        {
            m_Service = ServiceLocator.Instance.Resolve<IService>();
            //var fds = m_Service.GetFriends();
            Refresh();
        }

        public void AddFriendRequest(FriendSearchWrapper user)
        {
            Task.Run(async () =>
            {
                //var fds = await StoreManager.FriendStore.GetItemsAsync(true);
                await StoreManager.FriendRequestStore.AddFriendRequest(CurrentApp.AppContext.UserProfile.UserId, user.Item.UserId);
            });
        }

        public void AcceptFriendRequest(FriendRequestWrapper user)
        {
            Task.Run(async () =>
            {
                //var fds = await StoreManager.FriendStore.GetItemsAsync(true);
                await StoreManager.FriendRequestStore.AcceptFriendRequest(CurrentApp.AppContext.UserProfile.UserId, user.Item.UserId);
                await StoreManager.FriendStore.InsertAsync(new Friend() { UserId = CurrentApp.AppContext.UserProfile.UserId, FriendId = user.Item.UserId });
                await StoreManager.FriendStore.InsertAsync(new Friend() { UserId = user.Item.UserId, FriendId = CurrentApp.AppContext.UserProfile.UserId });
                await ExecuteRefreshCommand();
            });
        }

        public void RejectFriendRequest(FriendRequestWrapper user)
        {
            Task.Run(async () =>
            {
                //var fds = await StoreManager.FriendStore.GetItemsAsync(true);
                await StoreManager.FriendRequestStore.RejectFriendRequest(CurrentApp.AppContext.UserProfile.UserId, user.Item.UserId);
                await ExecuteRefreshCommand();
            });
        }

        void Refresh()
        {
            ExecuteRefreshCommand();
        }

        async Task ExecuteRefreshCommand()
        {
            var frnds = await StoreManager.FriendStore.GetAllFriends(CurrentApp.AppContext.UserProfile.UserId);
            Friends.Clear();
            foreach (var f in frnds)
            {
                var user = await StoreManager.UserStore.GetUser(f.FriendId);
                if (user != null)
                {
                    Friends.Add(new FriendWrapper(user, this));
                }
            }

            var friendRequests = await StoreManager.FriendRequestStore.PendingFriendRequests(CurrentApp.AppContext.UserProfile.UserId);
            bool hasFriendRequests = false;
            FriendRequestList.Clear();
            foreach ( var fr in friendRequests)
            {
                hasFriendRequests = true;
                var user = await StoreManager.UserStore.GetUser(fr.UserId);//Person who requested
                if (user != null)
                {
                    FriendRequestList.Add(new FriendRequestWrapper(user, this));
                }
            }

            HasFriendRequests = hasFriendRequests;
        }

    }
}