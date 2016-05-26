using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IUserStore : IBaseStore<User>
    {
        Task<IEnumerable<User>> FindFriends(String name);
    }
}