using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using WhoseShoutWeb.Models;
using Owin;
using WhoseShout.Models;

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

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<WhoseShoutServiceContext>
    {
        protected override void Seed(WhoseShoutServiceContext context)
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

