
using MvvmCross.Core.ViewModels;
using WhoseShout.Core.ViewModels;
using WhoseShout.Models;

namespace WhoseShout.Resources
{
    public class FriendRequestWrapper
    {
        User m_User;
        FriendsViewModel m_ViewModel;

        public FriendRequestWrapper(User user, FriendsViewModel viewModel)
        {
            m_User = user;
            m_ViewModel = viewModel;
        }

        public IMvxCommand AcceptClick
        {
            get
            {
                return new MvxCommand(() => m_ViewModel.AcceptFriendRequest(this));
            }
        }

        public IMvxCommand RejectClick
        {
            get
            {
                return new MvxCommand(() => m_ViewModel.AcceptFriendRequest(this));
            }
        }

        public User Item { get { return m_User; } }
    }
}