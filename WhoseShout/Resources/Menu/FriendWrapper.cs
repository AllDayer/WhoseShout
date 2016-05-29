
using MvvmCross.Core.ViewModels;
using WhoseShout.Core.ViewModels;
using WhoseShout.Models;

namespace WhoseShout.Resources
{
    public class FriendWrapper
    {
        User m_User;
        FriendsViewModel m_ViewModel;

        public FriendWrapper(User user, FriendsViewModel viewModel)
        {
            m_User = user;
            m_ViewModel = viewModel;
        }

        public User Item { get { return m_User; } }
    }
}