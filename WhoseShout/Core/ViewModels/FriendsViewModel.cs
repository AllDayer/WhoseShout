using System;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;
using WhoseShout.Helpers;
using WhoseShout.Services;
using WhoseShout.Models;
using System.Threading.Tasks;

namespace WhoseShout.Core.ViewModels
{
	public class FriendsViewModel : MvxViewModel
	{
        IService m_Service;

  
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

        public FriendsViewModel()
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
            //var fds = await m_Service.GetFriends("123");
            //Friends.Clear();
            //foreach(var f in fds)
            //{
            //    Friends.Add(f);
            //}
        }

	}
}