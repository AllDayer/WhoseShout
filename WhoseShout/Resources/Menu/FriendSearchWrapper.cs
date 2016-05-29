
using MvvmCross.Core.ViewModels;
using WhoseShout.Core.ViewModels;
using WhoseShout.Models;

namespace WhoseShout.Resources
{
    public class FriendSearchWrapper
    {
        User m_User;
        FriendsViewModel m_ViewModel;

        public FriendSearchWrapper(User user, FriendsViewModel viewModel)
        {
            m_User = user;
            m_ViewModel = viewModel;
        }

        public IMvxCommand AddClick
        {
            get
            {
                return new MvxCommand(() => m_ViewModel.AddFriendRequest(this));
            }
        }

        public User Item { get { return m_User; } }
    }
}