using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using WhoseShoutWeb.DataObjects;
using WhoseShoutWeb.Models;
using Owin;

namespace WhoseShoutWeb
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    // This middleware is intended to be used locally for debugging. By default, HostName will
                    // only have a value when running in an App Service application.
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            Guid tristanGuid = Guid.NewGuid();
            Guid normanGuid = Guid.NewGuid();
            Guid elspethGuid = Guid.NewGuid();
            Guid georgieGuid = Guid.NewGuid();

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
                context.Set<UserItem>().Add(useritem);
            }

            foreach (FriendItem friendItem in friendItems)
            {
                context.Set<FriendItem>().Add(friendItem);
            }

            base.Seed(context);
        }
    }
}

