using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IFriendStore : IBaseStore<Friend>
    {
        Task<IEnumerable<Friend>> GetAllFriends(String userId);
    }
}