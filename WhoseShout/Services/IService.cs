using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.Services
{
	public interface IService
	{
		Task Initialize();

		Task<IEnumerable<FriendItem>> GetFriends(String userId);

		Task<FriendItem> AddFriend(String friendId, String userId, string name);

		Task<FriendItem> UpdateFriend(FriendItem friend);

		Task<bool> DeleteFriend(FriendItem friend);

		Task SyncFriends(String userId);

        Task<UserItem> AddUser(String userId, String name, String Email);

        Task<bool> DeleteUser(UserItem user);

        Task SyncUsers(String userId);//Shouldn't be needed

        Task SyncFriendRequests(String userId);

        Task<IEnumerable<FriendRequest>> GetFriendRequests(String userId);

        Task<FriendRequest> AddFriendRequest(String userId, String futureFriendId);

        Task<FriendRequest> UpdateFriendRequest(FriendRequest friendRequest);

    }
}

