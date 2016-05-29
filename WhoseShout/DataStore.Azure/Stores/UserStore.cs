using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoseShout.Core;
using WhoseShout.DataStore.Azure.Interfaces;
using WhoseShout.Models;

namespace WhoseShout.DataStore.Azure
{
    public class UserStore : BaseStore<User>, IUserStore
    {

        public async Task<IEnumerable<User>> FindFriends(string name)
        {
            await InitializeStore().ConfigureAwait(false);
            var users = await GetItemsAsync(true).ConfigureAwait(false);
            return users.Where(f => f.Name.Contains(name));

        }
        
        public async Task<User> GetUser(String id)
        {
            await InitializeStore().ConfigureAwait(false);
            var users = await GetItemsAsync().ConfigureAwait(false);
            return users.FirstOrDefault(x => x.UserId == id);
        }
    }
}