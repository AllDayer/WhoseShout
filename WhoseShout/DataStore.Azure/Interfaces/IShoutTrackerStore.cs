using System;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IShoutTrackerStore : IBaseStore<ShoutTracker>
    {
        Task<ShoutTracker> GetShout(Guid shoutId);
    }
}