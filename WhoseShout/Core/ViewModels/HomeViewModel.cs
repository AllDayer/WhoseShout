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
        private ArrayAdapter m_Adapter;

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
                    await StoreManager.ShoutTrackerStore.InsertAsync(Shout);
                    StartShout = false;
                });
            }
        }

        public MvxCommand AddUserCommand
        {
            get
            {
                return new MvxCommand(async () =>
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
            { return m_CurrentTextHint; }
            set
            {
                MvvmCross.Platform.Platform.MvxTrace.Trace("Partial Text Value Sent {0}", value);
                //Setting _currentTextHint to null if an empty string gets passed here
                //is extremely important.
                if (value == "")
                {
                    m_CurrentTextHint = null;
                    SetSuggestionsEmpty();
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

        private void SetSuggestionsEmpty()
        {
            FriendSuggestions = new List<User>();
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
                RaisePropertyChanged(nameof(SelectedObj));
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
            }
        }

        public HomeViewModel()
        {
            Friends = new List<User>();
            FriendSuggestions = new List<User>();
            Refresh();
        }

        public void Refresh()
        {
            Task.Run(async () =>
            {
                await ExecuteRefresh();
            });
        }


        async Task ExecuteRefresh()
        {

            var friends = await StoreManager.FriendStore.GetAllFriends(CurrentApp.AppContext.UserProfile.UserId);
            FriendSuggestions.Clear();
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