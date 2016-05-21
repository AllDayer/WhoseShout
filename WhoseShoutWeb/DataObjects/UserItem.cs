using Microsoft.Azure.Mobile.Server;
using System;

namespace WhoseShoutWeb.DataObjects
{
    public class UserItem : EntityData
    {
        public Guid UserId { get; set; }
        public String Name { get; set; }
    }
}