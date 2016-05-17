using System;
using WhoseShout.Helpers;


namespace WhoseShout.Models
{
    public class User : EntityData
    {
        public Guid UserId { get; set; }
        public String Name { get; set; }

    }
}