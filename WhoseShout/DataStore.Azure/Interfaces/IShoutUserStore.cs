using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure.Interfaces
{
    public interface IShoutUserStore : IBaseStore<ShoutUser>
    {
        Task<IEnumerable<ShoutUser>> GetShoutUsers(Guid shoutId);

        Task<IEnumerable<ShoutUser>> GetShoutsForUser(String userId);
    }
}