using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using WhoseShout.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

namespace WhoseShout.Fragments
{
    [MvxFragmentAttribute(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("whoseshout.fragments.FriendsFragment")]
    public class FriendsFragment : MvxFragment<FriendsViewModel>
    {
		public FriendsFragment()
		{
		}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.FriendsFragment, null);
            return view;
        }
    }
}

