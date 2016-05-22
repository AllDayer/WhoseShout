using Microsoft.Azure.Mobile.Server;
using System;

namespace WhoseShoutWeb.DataObjects
{
    public class FriendItem : EntityData
    {
        public String UserId { get; set; }
        public String FriendId { get; set; }
        public string Name { get; set; }
    }
}