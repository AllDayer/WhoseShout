using Microsoft.Azure.Mobile.Server;
using System;

namespace WhoseShoutWeb.DataObjects
{
    public class FriendItem : EntityData
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public string Name { get; set; }
    }
}