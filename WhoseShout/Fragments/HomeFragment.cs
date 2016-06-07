using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using WhoseShout.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.WeakSubscription;
using MvvmCross.Droid.Support.V7.RecyclerView;
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

        private IDisposable _itemSelectedToken;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);



            var view = this.BindingInflate(Resource.Layout.HomeFragment, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.lvshoutusers);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                layoutManager.Orientation = (int)Orientation.Horizontal;
                recyclerView.SetLayoutManager(layoutManager);
                
            }

            //_itemSelectedToken = ViewModel.WeakSubscribe(() => ViewModel.SelectedUser,
            //(sender, args) =>
            //{
            //    if (ViewModel.SelectedUser != null)
            //        Toast.MakeText(Activity,
            //            $"Selected: {ViewModel.SelectedUser.Name}",
            //            ToastLength.Short).Show();
            //});
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