using System;
using WhoseShout.Helpers;


namespace WhoseShout.Models
{
    public class UserItem : EntityData
    {
        public String UserId { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }

    }
}