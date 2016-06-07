using Android.Widget;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        #region NewShout
        public MvxCommand StartShoutCommand
        {
            get
            {
                return new MvxCommand(() =>
                {

                    Refresh();
                    if (!StartShout)
                    {
                        StartShout = true;
                        Shout = new ShoutTracker();
                        Shout.ShoutId = Guid.NewGuid();
                        UsersInShout.Add(m_CurrentUser);
                    }
                    else
                    {
                        StartShout = false;
                    }
                    System.Console.WriteLine("Start Shout");
                });
            }
        }

        public MvxCommand SaveShoutCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    if (UsersInShout.Count > 1)
                    {
                        await StoreManager.ShoutTrackerStore.InsertAsync(Shout);
                        foreach (var su in UsersInShout)
                        {
                            ShoutUser shoutUser = new ShoutUser() { ShoutId = Shout.ShoutId, UserId = su.UserId };
                            await StoreManager.ShoutUserStore.InsertAsync(shoutUser);

                        }

                        StartShout = false;
                        UsersInShout.Clear();
                    }
                });
            }
        }

        public MvxCommand AddUserCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    this.Refresh();
                    System.Console.WriteLine("Hello");
                    //await StoreManager.u
                    //await StoreManager.ShoutTrackerStore.InsertAsync(Shout);
                });
            }
        }

        public MvxCommand ItemSelectedCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    System.Console.WriteLine(SelectedObj);
                });
            }
        }

        private bool m_StartShout = false;
        public bool StartShout
        {
            get
            {
                return m_StartShout;
            }
            set
            {
                m_StartShout = value;
                RaisePropertyChanged(nameof(StartShout));
            }
        }

        private ShoutTracker m_Shout;
        public ShoutTracker Shout
        {
            get
            {
                return m_Shout;
            }
            set
            {
                m_Shout = value;
                RaisePropertyChanged(nameof(Shout));
            }
        }

        public List<User> m_Friends;
        public List<User> Friends
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

        private List<User> m_FriendSuggestions = new List<User>();
        public List<User> FriendSuggestions
        {
            get
            {
                if (m_FriendSuggestions == null)
                {
                    m_FriendSuggestions = new List<User>();
                }
                return m_FriendSuggestions;
            }
            set
            {
                m_FriendSuggestions = value;
                RaisePropertyChanged(nameof(FriendSuggestions));
            }
        }

        private string m_CurrentTextHint;
        public string CurrentTextHint
        {
            get
            {
                return m_CurrentTextHint;
            }
            set
            {

                MvvmCross.Platform.Platform.MvxTrace.Trace("Partial Text Value Sent {0}", value);
                //Setting _currentTextHint to null if an empty string gets passed here
                //is extremely important.

                System.Diagnostics.Debug.WriteLine("CurrentTextHint: " + value.ToString());

                if (value == "")
                {
                    m_CurrentTextHint = "";
                    SetSuggestionsEmpty();
                    RaisePropertyChanged(nameof(CurrentTextHint));
                    return;
                }
                else
                {
                    m_CurrentTextHint = value;
                }

                if (m_CurrentTextHint.Trim().Length < 2)
                {
                    SetSuggestionsEmpty();
                    return;
                }

                var list = Friends.Where(i => (i ?? null).Name.ToUpper().Contains(m_CurrentTextHint.ToUpper()));
                if (list.Count() > 0)
                {
                    FriendSuggestions = list.ToList();
                }
                else
                {
                    SetSuggestionsEmpty();
                }
            }
        }

        private User m_SelectedUser;
        public User SelectedUser
        {
            get
            {
                return m_SelectedUser;
            }
            set
            {
                m_SelectedUser = value;
                RaisePropertyChanged(nameof(SelectedUser));
            }
        }

        private object m_SelectedObject;
        public object SelectedObj
        {
            get
            {
                return m_SelectedObject;
            }
            set
            {
                m_SelectedObject = value;

                if (m_SelectedObject != null && CurrentTextHint.Length > 0)
                {
                    //var friend = FriendSuggestions.FirstOrDefault(x => x.Name == m_SelectedObject.ToString());
                    //if (friend != null)
                    {
                        //Need a check for this
                        UsersInShout.Add((User)m_SelectedObject);
                    }

                    m_SelectedObject = null;
                    CurrentTextHint = "";
                    RaisePropertyChanged(nameof(SelectedObj));
                }
            }
        }

        private string m_SearchTerm;
        public string SearchTerm
        {
            get
            {
                return m_SearchTerm;
            }
            set
            {
                m_SearchTerm = value;
                this.RaisePropertyChanged(nameof(SearchTerm));
                RaisePropertyChanged(nameof(FriendSuggestions));
            }
        }

        private ObservableCollection<User> m_UsersInShout = new ObservableCollection<User>();
        public ObservableCollection<User> UsersInShout
        {
            get
            {
                return m_UsersInShout;
            }
            set
            {
                m_UsersInShout = value;
            }
        }

        private void SetSuggestionsEmpty()
        {
            FriendSuggestions = new List<User>(Friends);
        }

        #endregion
        #region CurrentShouts
        public ObservableCollection<ShoutViewModel> CurrentShouts { get; set; }
        #endregion

        public HomeViewModel()
        {
            Friends = new List<User>();
            FriendSuggestions = new List<User>();
            CurrentShouts = new ObservableCollection<ShoutViewModel>();
            Refresh();
        }

        public void Refresh()
        {
            Task.Run(async () =>
            {
                await ExecuteRefresh();
            });
        }

        private User m_CurrentUser;

        async Task ExecuteRefresh()
        {
            m_CurrentUser = await StoreManager.UserStore.GetUser(CurrentApp.AppContext.UserProfile.UserId);
            var shoutsForUser = await StoreManager.ShoutUserStore.GetShoutsForUser(CurrentApp.AppContext.UserProfile.UserId);
            var count = shoutsForUser.Count();
            foreach (var userShout in shoutsForUser)
            {
                ShoutViewModel svm = new ShoutViewModel()
                {
                    ShoutId = userShout.ShoutId,
                };
                await svm.Initialise(userShout.ShoutId);
                CurrentShouts.Add(svm);
            }

            var friends = await StoreManager.FriendStore.GetAllFriends(CurrentApp.AppContext.UserProfile.UserId);
            FriendSuggestions.Clear();
            Friends.Clear();
            foreach (var f in friends)
            {
                var user = await StoreManager.UserStore.GetUser(f.FriendId);
                if (user != null)
                {
                    Friends.Add(user);
                    FriendSuggestions.Add(user);
                }

            }

            RaisePropertyChanged(nameof(Friends));
            RaisePropertyChanged(nameof(FriendSuggestions));
            //m_Adapter = new ArrayAdapter(Mvx.Resolve, Android.Resource.Layout.SimpleDropDownItem1Line, Friends.ToArray());

        }
        

    }
}