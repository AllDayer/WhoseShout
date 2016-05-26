using System;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure
{
    public class ShoutTrackerStore : BaseStore<ShoutTracker>, IShoutTrackerStore
    {
    }
}