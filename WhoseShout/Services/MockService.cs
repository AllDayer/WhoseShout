using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;
using System.Linq;

namespace WhoseShout.Services
{
	public class MockService : IService
	{

		List<Friend> friends { get; set; } = new List<Friend>();

		public MockService()
		{
			if (friends.Count == 0)
			{
				friends = MockFriends();
			}
		}

		public Task<Friend> AddFriend(string name)
		{
            var friend = new Friend()
            {
                Name = name
            };

            friends.Add(friend);
            return Task.FromResult(friend);
		}

        List<Friend> MockFriends()
        {
            var items = new List<Friend>();

            items.Add(new Friend() { Name = "Norman" });

            items.Add(new Friend() { Name = "Tristan" });

            return items;
        }

        public Task Initialize()
        {
			return null;
        }

        public Task<IEnumerable<Friend>> GetFriends()
        {
			IEnumerable<Friend> items = friends.AsEnumerable();
			return Task.FromResult(items);
        }

        public Task<Friend> UpdateFriend(Friend friend)
        {
			var item = friends.FirstOrDefault(x => x.FriendId == friend.FriendId);
			friends.Remove(item);
			friends.Add(item);
			return Task.FromResult(friend);

        }

        public Task<bool> DeleteFriend(Friend friend)
        {
			friends.Remove(friend);
			return Task.FromResult(true);
        }

        public Task SyncFriends()
        {
			return null;
        }
    }
}

