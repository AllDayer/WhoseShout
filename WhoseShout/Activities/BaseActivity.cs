﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Support.V7.App;
//using Android.Support.V7.Widget;

//namespace WhoseShout.Activities
//{
//	[Activity (Label = "BaseActivity")]			
//	public abstract class BaseActivity : AppCompatActivity
//	{

//		protected override void OnCreate (Bundle bundle)
//		{
//			base.OnCreate (bundle);
//			SetContentView (LayoutResource);
//			Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
//			if (Toolbar != null) {
//				SetSupportActionBar(Toolbar);
//				SupportActionBar.SetDisplayHomeAsUpEnabled(true);
//				SupportActionBar.SetHomeButtonEnabled (true);

//			}
//		}
        

//		protected int ActionBarIcon {
//			set{ Toolbar.SetNavigationIcon (value); }
//		}
//	}
//}

