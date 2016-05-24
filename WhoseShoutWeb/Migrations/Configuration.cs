namespace WhoseShoutWeb.Migrations
{
    using DataObjects;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WhoseShoutWeb.Models.MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WhoseShoutWeb.Models.MobileServiceContext context)
        {
            String tristanGuid = Guid.NewGuid().ToString();
            String normanGuid = Guid.NewGuid().ToString();
            String elspethGuid = Guid.NewGuid().ToString();
            String georgieGuid = Guid.NewGuid().ToString();

            List<UserItem> userItems = new List<UserItem>
            {
                new UserItem { Id = tristanGuid.ToString(), UserId = tristanGuid, Name = "Tristan" },
                new UserItem { Id = normanGuid.ToString(), UserId = normanGuid, Name = "Norman" },
                new UserItem { Id = elspethGuid.ToString(), UserId = elspethGuid, Name = "Elspeth" },
                new UserItem { Id = georgieGuid.ToString(), UserId = georgieGuid, Name = "Georgie" },
            };
            List<FriendItem> friendItems = new List<FriendItem>
            {
                new FriendItem { Id = Guid.NewGuid().ToString(), UserId = tristanGuid, FriendId = elspethGuid },
                new FriendItem { Id = Guid.NewGuid().ToString(), UserId = elspethGuid, FriendId = tristanGuid },
                new FriendItem { Id = Guid.NewGuid().ToString(), UserId = tristanGuid, FriendId = normanGuid },
                new FriendItem { Id = Guid.NewGuid().ToString(), UserId = normanGuid, FriendId = tristanGuid },
                new FriendItem { Id = Guid.NewGuid().ToString(), UserId = normanGuid, FriendId = georgieGuid },
            };

            foreach (UserItem useritem in userItems)
            {
                context.Set<UserItem>().AddOrUpdate(useritem);
            }

            foreach (FriendItem friendItem in friendItems)
            {
                context.Set<FriendItem>().AddOrUpdate(friendItem);
            }

            FriendRequest fr = new FriendRequest()
            {
                UserId = tristanGuid.ToString(),
                FutureFriendId = normanGuid.ToString(),
                ApproveFlag = true
            };

            context.Set<FriendRequest>().AddOrUpdate(fr);
        }
    }
}
