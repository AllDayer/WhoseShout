using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoseShout.Models;

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

            var friend1 = new Friend() { Name = "Norman" };

            var friend2 = new Friend() { Name = "Tristan" };

            return items;
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Friend>> GetFriends()
        {
            throw new NotImplementedException();
        }

        public Task<Friend> UpdateFriend(Friend friend)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFriend(Friend friend)
        {
            throw new NotImplementedException();
        }

        public Task SyncFriends()
        {
            throw new NotImplementedException();
        }
    }
}

