using System;
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
    [Register("whoseshout.fragments.HomeFragment")]
    public class HomeFragment : MvxFragment<HomeViewModel>
    {
        private String m_Home = "Home";
        public String Home
        {
            get
            {
                return m_Home;
            }
            set
            {
                m_Home = value;
            }
        }

		public HomeFragment()
		{
			RetainInstance = true;
		}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.HomeFragment, null);
            return view;
        }


        //		//List<Monkey> friends;
        //		public override View OnCreateView(LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        //		{
        //			base.OnCreateView(inflater, container, savedInstanceState);

        //			HasOptionsMenu = true;
        //			var view = inflater.Inflate(Resource.Layout.fragment_browse, null);

        ////			var grid = view.FindViewById<GridView>(Resource.Id.grid);
        ////			friends = Util.GenerateFriends();
        ////			grid.Adapter = new MonkeyAdapter(Activity, friends);
        ////			grid.ItemClick += GridOnItemClick;
        //			return view;
        //		}

//        void GridOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
//		{
////			var intent = new Intent(Activity, typeof(FriendActivity));
////			intent.PutExtra("Title", friends[itemClickEventArgs.Position].Title);
////			intent.PutExtra("Image", friends[itemClickEventArgs.Position].Image);
////			intent.PutExtra("Details", friends[itemClickEventArgs.Position].Details);
////			StartActivity(intent);
//		}


	}
}