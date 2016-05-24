namespace WhoseShoutWeb.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WhoseShout.Models;
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

            List<User> users = new List<User>
            {
                new User { Id = tristanGuid.ToString(), UserId = tristanGuid, Name = "Tristan" },
                new User { Id = normanGuid.ToString(), UserId = normanGuid, Name = "Norman" },
                new User { Id = elspethGuid.ToString(), UserId = elspethGuid, Name = "Elspeth" },
                new User { Id = georgieGuid.ToString(), UserId = georgieGuid, Name = "Georgie" },
            };
            List<Friend> friends = new List<Friend>
            {
                new Friend { Id = Guid.NewGuid().ToString(), UserId = tristanGuid, FriendId = elspethGuid },
                new Friend { Id = Guid.NewGuid().ToString(), UserId = elspethGuid, FriendId = tristanGuid },
                new Friend { Id = Guid.NewGuid().ToString(), UserId = tristanGuid, FriendId = normanGuid },
                new Friend { Id = Guid.NewGuid().ToString(), UserId = normanGuid, FriendId = tristanGuid },
                new Friend { Id = Guid.NewGuid().ToString(), UserId = normanGuid, FriendId = georgieGuid },
            };

            foreach (User useritem in users)
            {
                context.Set<User>().Add(useritem);
            }

            foreach (Friend friendItem in friends)
            {
                context.Set<Friend>().Add(friendItem);
            }

            base.Seed(context);
        }
    }
}
