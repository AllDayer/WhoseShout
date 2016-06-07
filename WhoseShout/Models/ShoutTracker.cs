using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhoseShout.Models
{
    public class ShoutTracker : BaseDataObject
    {
        public Guid ShoutId { get; set; }
        public String Name { get; set; }
        //public List<User> Users { get; set; }
        //public List<Purchase> Purchases { get; set; }
    }
}