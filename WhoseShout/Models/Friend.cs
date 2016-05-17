using System;
using WhoseShout.Helpers;

namespace WhoseShout.Models
{
    public class Friend : EntityData
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public string Name { get; set; }
        
    }
}

