﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Gms.Common;
using Android.Util;
using Android.Gms.Plus;
using UniversalImageLoader.Core;
using Android.Gms.Auth;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Identity;

using WhoseShout.Fragments;
using WhoseShout.Helpers;
using WhoseShout.Services;
using WhoseShout.Core;
using WhoseShout.Core.ViewModels;
using Android.Support.Design.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Collections.Generic;
using WhoseShout.DataStore.Azure.Interfaces;

//462990388141-f5ovgtol24vkrggr92ur5ubg5pfg1e76.apps.googleusercontent.com
namespace WhoseShout.Activities
{
    [Activity(Label = "WhoseShout", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>,
        GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        public Android.Support.V7.Widget.Toolbar Toolbar
        {
            get;
            set;
        }

        protected int LayoutResource
        {
            get
            {
                return Resource.Layout.page_home_view;
            }
        }

        const string TAG = "MainActivity";

        const int RC_SIGN_IN = 9001;

        const string KEY_IS_RESOLVING = "is_resolving";
        const string KEY_SHOULD_RESOLVE = "should_resolve";


        GoogleApiClient mGoogleApiClient;
        GoogleSignInAccount mGoogleSignInAccount;


        bool mIsResolving = false;

        bool mShouldResolve = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            SetContentView(LayoutResource);
            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetHomeButtonEnabled(true);



            var config = ImageLoaderConfiguration.CreateDefault(ApplicationContext);
            ImageLoader.Instance.Init(config);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var blah = FindViewById(Resource.Layout.page_home_view);

            
            //nav_view is drawerlistview

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                ShowFragment(e.MenuItem.ItemId);
            };

            //SetContentView (Resource.Layout.Main);

            if (bundle != null)
            {
                mIsResolving = bundle.GetBoolean(KEY_IS_RESOLVING);
                mShouldResolve = bundle.GetBoolean(KEY_SHOULD_RESOLVE);
            }

            //if first time you will want to go ahead and click first item.
            if (bundle == null)
            {
                //ListItemClicked(0);
            }

            GoogleSignIn();

            ShowFragment(Resource.Id.nav_friends);

        }

        private void ShowFragment(int position)
        {
            switch (position)
            {
                case Resource.Id.nav_home:
                    ViewModel.NavigateTo(0);
                    break;

                case Resource.Id.nav_profile:
					var param = new Dictionary<string, string>
					{
						{ "email", mGoogleSignInAccount.Email},
						{ "name", mGoogleSignInAccount.DisplayName }
					};

					ViewModel.NavigateTo(1, param);
                    break;

				case Resource.Id.nav_friends:
					ViewModel.NavigateTo(2);
				break;
            }

            drawerLayout.CloseDrawers();
        }

        protected int ActionBarIcon
        {
            set { Toolbar.SetNavigationIcon(value); }
        }

        private void GoogleSignIn()
        {
            var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                        .RequestEmail()
                        .Build();

            mGoogleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .AddScope(new Scope(Scopes.Profile))
                .Build();

            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
            StartActivityForResult(signInIntent, RC_SIGN_IN);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }


        public void OnConnected(Bundle connectionHint)
        {
            Log.Debug(TAG, "onConnected:" + connectionHint);

            //UpdateUI(true);
        }

        public void OnConnectionSuspended(int cause)
        {
            Log.Warn(TAG, "onConnectionSuspended:" + cause);
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            Log.Debug(TAG, "onConnectionFailed:" + result);

            if (!mIsResolving && mShouldResolve)
            {
                if (result.HasResolution)
                {
                    try
                    {
                        result.StartResolutionForResult(this, RC_SIGN_IN);
                        mIsResolving = true;
                    }
                    catch (IntentSender.SendIntentException e)
                    {
                        Log.Error(TAG, "Could not resolve ConnectionResult.", e);
                        mIsResolving = false;
                        mGoogleApiClient.Connect();
                    }
                }
                else
                {
                    ShowErrorDialog(result);
                }
            }
            else
            {
                //UpdateUI(false);
            }
        }


        void ShowErrorDialog(ConnectionResult connectionResult)
        {
            int errorCode = connectionResult.ErrorCode;

            if (GooglePlayServicesUtil.IsUserRecoverableError(errorCode))
            {
                var listener = new DialogInterfaceOnCancelListener();
                listener.OnCancelImpl = (dialog) =>
                {
                    mShouldResolve = false;
                    //UpdateUI(false);
                };
                GooglePlayServicesUtil.GetErrorDialog(errorCode, this, RC_SIGN_IN, listener).Show();
            }
            else
            {
                var errorstring = string.Format(GetString(Resource.String.play_services_error_fmt), errorCode);
                Toast.MakeText(this, errorstring, ToastLength.Short).Show();

                mShouldResolve = false;
                //UpdateUI(false);
            }
        }

        class DialogInterfaceOnCancelListener : Java.Lang.Object, IDialogInterfaceOnCancelListener
        {
            public Action<IDialogInterface> OnCancelImpl { get; set; }

            public void OnCancel(IDialogInterface dialog)
            {
                OnCancelImpl(dialog);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Debug(TAG, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);

            if (requestCode == RC_SIGN_IN)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                HandleSignInResult(result);
                if (resultCode != Result.Ok)
                {
                    mShouldResolve = false;
                }

                mIsResolving = false;
                mGoogleApiClient.Connect();
            }
        }

        public void HandleSignInResult(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {


                mGoogleSignInAccount = result.SignInAccount;
                navigationView.GetHeaderView(0).FindViewById<TextView>(Resource.Id.profile_name).Text = mGoogleSignInAccount.DisplayName;
                navigationView.GetHeaderView(0).FindViewById<TextView>(Resource.Id.profile_email).Text = mGoogleSignInAccount.Email;

                CurrentApp.AppContext.UserProfile = new Models.User()
                {
                    UserId = mGoogleSignInAccount.Id,
                    Name = mGoogleSignInAccount.DisplayName,
                    Email = mGoogleSignInAccount.Email
                };

                IService service = ServiceLocator.Instance.Resolve<IService>();
                service.AddUser(CurrentApp.AppContext.UserProfile.UserId, CurrentApp.AppContext.UserProfile.Name, CurrentApp.AppContext.UserProfile.Email);


                System.Threading.Tasks.Task.Run(async () =>
                {
                    try
                    {
                        ViewModelBase.Init();
                        // Download data
                        var manager = ServiceLocator.Instance.Resolve<IStoreManager>();
                        if (manager == null)
                            return;

                        await manager.SyncAllAsync(false);
                    }
                    catch (Exception ex)
                    {

                    }
                }).Wait(TimeSpan.FromSeconds(60));

                //service.AddFriendRequest(CurrentApp.AppContext.UserProfile.UserId, "dbca2a93-f595-48da-838f-b20088e57086");

            }
        }

        //protected override void OnStart ()
        //{
        //	base.OnStart ();
        //	mGoogleApiClient.Connect ();
        //}

        //protected override void OnStop ()
        //{
        //	base.OnStop ();
        //	mGoogleApiClient.Disconnect ();
        //}

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutBoolean(KEY_IS_RESOLVING, mIsResolving);
            outState.PutBoolean(KEY_SHOULD_RESOLVE, mIsResolving);
        }

    }
}

