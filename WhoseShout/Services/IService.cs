using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

namespace WhoseShout.Services
{
	public interface IService
	{
		Task Initialize();

		Task<IEnumerable<Friend>> GetFriends(String userId);

		Task<Friend> AddFriend(String friendId, String userId, string name);

		Task<Friend> UpdateFriend(Friend friend);

		Task<bool> DeleteFriend(Friend friend);

		Task SyncFriends(String userId);

        Task<User> AddUser(String userId, String name, String Email);

        Task<bool> DeleteUser(User user);

        Task SyncUsers(String userId);//Shouldn't be needed

        Task SyncFriendRequests(String userId);

        Task<IEnumerable<FriendRequest>> GetFriendRequests(String userId);

        Task<FriendRequest> AddFriendRequest(String userId, String futureFriendId);

        Task<FriendRequest> UpdateFriendRequest(FriendRequest friendRequest);

    }
}

