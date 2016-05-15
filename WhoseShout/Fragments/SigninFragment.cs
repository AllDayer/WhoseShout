
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Gms.Common.Apis;
//using Android.Support.V7.App;
//using Android.Support.V4.Widget;
//using Android.Gms.Common;
//using Android.Util;
//using Android.Gms.Plus;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using Fragment = Android.Support.V4.App.Fragment;

//namespace WhoseShout
//{
//	public class SigninFragment : Fragment, View.IOnClickListener,
//		GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
//	{
//		const string TAG = "MainActivity";

//		const int RC_SIGN_IN = 9001;

//		const string KEY_IS_RESOLVING = "is_resolving";
//		const string KEY_SHOULD_RESOLVE = "should_resolve";

//		int count = 1;
//		GoogleApiClient mGoogleApiClient;

//		TextView mStatus;

//		bool mIsResolving = false;

//		bool mShouldResolve = false;

//		public override void OnCreate(Bundle savedInstanceState)
//		{
//			base.OnCreate(savedInstanceState);


//		}

//		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//		{
//			// Use this to return your custom view for this Fragment
//			var view = inflater.Inflate(Resource.Layout.fragment_signin, null);
//			view.FindViewById(Resource.Id.sign_in_button).SetOnClickListener(this);
//			view.FindViewById(Resource.Id.sign_out_button).SetOnClickListener(this);
//			view.FindViewById(Resource.Id.disconnect_button).SetOnClickListener(this);

//			view.FindViewById<view.SignInButton>(Resource.Id.sign_in_button).SetSize(view.SignInButton.SizeWide);
//			view.FindViewById(Resource.Id.sign_in_button).Enabled = false;

//			mStatus = view.FindViewById<TextView>(Resource.Id.status);
//			return view;
//		}


//		void UpdateUI(bool isSignedIn)
//		{
//			if (isSignedIn)
//			{
//				var person = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
//				var name = string.Empty;
//				if (person != null)
//					name = person.DisplayName;
//				mStatus.Text = string.Format(GetString(Resource.String.signed_in_fmt), name);

//				this.View.FindViewById(Resource.Id.sign_in_button).Visibility = ViewStates.Gone;
//				this.View.FindViewById(Resource.Id.sign_out_and_disconnect).Visibility = ViewStates.Visible;
//			}
//			else {
//				mStatus.Text = GetString(Resource.String.signed_out);

//				this.View.FindViewById(Resource.Id.sign_in_button).Enabled = true;
//				this.View.FindViewById(Resource.Id.sign_in_button).Visibility = ViewStates.Visible;
//				this.View.FindViewById(Resource.Id.sign_out_and_disconnect).Visibility = ViewStates.Gone;
//			}
//		}

//		class DialogInterfaceOnCancelListener : Java.Lang.Object, IDialogInterfaceOnCancelListener
//		{
//			public Action<IDialogInterface> OnCancelImpl { get; set; }

//			public void OnCancel(IDialogInterface dialog)
//			{
//				OnCancelImpl(dialog);
//			}
//		}


//		public async void OnClick(View v)
//		{
//			switch (v.Id)
//			{
//				case Resource.Id.sign_in_button:
//					mStatus.Text = GetString(Resource.String.signing_in);
//					mShouldResolve = true;
//					mGoogleApiClient.Connect();
//					break;
//				case Resource.Id.sign_out_button:
//					if (mGoogleApiClient.IsConnected)
//					{
//						PlusClass.AccountApi.ClearDefaultAccount(mGoogleApiClient);
//						mGoogleApiClient.Disconnect();
//					}
//					UpdateUI(false);
//					break;
//				case Resource.Id.disconnect_button:
//					if (mGoogleApiClient.IsConnected)
//					{
//						PlusClass.AccountApi.ClearDefaultAccount(mGoogleApiClient);
//						await PlusClass.AccountApi.RevokeAccessAndDisconnect(mGoogleApiClient);
//						mGoogleApiClient.Disconnect();
//					}
//					UpdateUI(false);
//					break;
//			}
//		}
//	}
//}

