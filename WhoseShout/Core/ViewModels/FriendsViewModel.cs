using System;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;
using WhoseShout.Helpers;
using WhoseShout.Services;
using WhoseShout.Models;

namespace WhoseShout.Core.ViewModels
{
	public class FriendsViewModel : MvxViewModel
	{
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

		public FriendsViewModel()
		{
			ServiceLocator.Instance.Resolve<IService>();
		}

	}
}