using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.Services
{
	public interface IService
	{
		Task Initialize();

		Task<IEnumerable<Friend>> GetFriends();

		Task<Friend> AddFriend(string name);

		Task<Friend> UpdateFriend(Friend friend);

		Task<bool> DeleteFriend(Friend friend);

		Task SyncFriends();

	}
}

