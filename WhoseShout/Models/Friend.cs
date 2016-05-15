using System;
using WhoseShout.Helpers;

namespace WhoseShout.Models
{
    public class Friend : EntityData
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}

