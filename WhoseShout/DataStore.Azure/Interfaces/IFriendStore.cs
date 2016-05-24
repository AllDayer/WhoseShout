using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IFriendStore : IBaseStore<Friend>
    {
    }
}