using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;
using System.Linq;

namespace WhoseShout.Services
{
	public class MockService : IService
	{

		List<FriendItem> friends { get; set; } = new List<FriendItem>();

		public MockService()
		{
			if (friends.Count == 0)
			{
				friends = MockFriends();
			}
		}

		public Task<FriendItem> AddFriend(String friendId, String userId, string name)
		{
            var friend = new FriendItem()
            {
                Name = name
            };

            friends.Add(friend);
            return Task.FromResult(friend);
		}

        List<FriendItem> MockFriends()
        {
            var items = new List<FriendItem>();

            items.Add(new FriendItem() { Name = "Norman" });

            items.Add(new FriendItem() { Name = "Tristan" });

            return items;
        }

        public Task Initialize()
        {
			return null;
        }

        public Task<IEnumerable<FriendItem>> GetFriends(String userId)
        {
			IEnumerable<FriendItem> items = friends.AsEnumerable();
			return Task.FromResult(items);
        }

        public Task<FriendItem> UpdateFriend(FriendItem friend)
        {
			var item = friends.FirstOrDefault(x => x.FriendId == friend.FriendId);
			friends.Remove(item);
			friends.Add(item);
			return Task.FromResult(friend);

        }

        public Task<bool> DeleteFriend(FriendItem friend)
        {
			friends.Remove(friend);
			return Task.FromResult(true);
        }

        public Task SyncFriends(String userId)
        {
			return null;
        }

        public Task AddUser(String userId, string name)
        {
            throw new NotImplementedException();
        }

        Task<UserItem> IService.AddUser(string userId, string name, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(UserItem user)
        {
            throw new NotImplementedException();
        }

        public Task SyncUsers(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SyncFriendRequests(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FriendRequest>> GetFriendRequests(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<FriendRequest> AddFriendRequest(string userId, string futureFriendId)
        {
            throw new NotImplementedException();
        }

        public Task<FriendRequest> UpdateFriendRequest(FriendRequest friendRequest)
        {
            throw new NotImplementedException();
        }
    }
}

