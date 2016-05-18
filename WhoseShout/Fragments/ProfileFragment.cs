using Android.OS;
using Android.Runtime;
using Android.Views;
using WhoseShout.Core;
using MvvmCross.Droid.Shared.Attributes;
using WhoseShout.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

namespace WhoseShout.Fragments
{
    [MvxFragmentAttribute(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("whoseshout.fragments.ProfileFragment")]
    public class ProfileFragment : MvxFragment<ProfileViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			var ignore = base.OnCreateView(inflater, container, savedInstanceState);
			var view = this.BindingInflate(Resource.Layout.ProfileFragment, null);
			return view;
        }

    }
}
