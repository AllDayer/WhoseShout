using System;
using WhoseShout.Helpers;

namespace WhoseShout.Models
{
    public class FriendItem : EntityData
    {
        public String UserId { get; set; }
        public String FriendId { get; set; }
        public string Name { get; set; }
        
    }
}

