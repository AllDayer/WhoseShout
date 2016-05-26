using System;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;
using WhoseShout.Helpers;
using WhoseShout.Services;
using WhoseShout.Models;
using System.Threading.Tasks;
using System.Windows.Input;

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
                    var searchResult = await StoreManager.UserStore.FindFriends(FriendsTest);
                    UserSearch.Clear();

                    foreach (var v in searchResult)
                    {
                        UserSearch.Add(v);
                    }
                });
            }
        }

        ObservableCollection<Friend> m_Friends = new ObservableCollection<Friend>();
        public ObservableCollection<Friend> Friends
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

        ObservableCollection<User> m_UserSearch = new ObservableCollection<User>();
        public ObservableCollection<User> UserSearch
        {
            get
            {
                return m_UserSearch;
            }
            set
            {
                m_UserSearch = value;
                RaisePropertyChanged(nameof(UserSearch));
            }
        }

        private string m_FriendsTest = "Friends";
        public string FriendsTest
        {
            get
            {
                return m_FriendsTest;
            }
            set
            {
                m_FriendsTest = value;
                RaisePropertyChanged(nameof(FriendsTest));
            }
        }

        public FriendsViewModel() : base()
        {
            m_Service = ServiceLocator.Instance.Resolve<IService>();
            //var fds = m_Service.GetFriends();
            Refresh();
        }

        void Refresh()
        {
            ExecuteRefreshCommand();
        }

        async Task ExecuteRefreshCommand()
        {
            var fds = await StoreManager.FriendStore.GetItemsAsync(true);
            var fds2 = await StoreManager.FriendStore.GetAllFriends(CurrentApp.AppContext.UserProfile.UserId);
            //var fds = await m_Service.GetFriends("123");
            Friends.Clear();
            foreach (var f in fds)
            {
                Friends.Add(f);
            }
        }

    }
}