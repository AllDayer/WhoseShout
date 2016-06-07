using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhoseShout.Models
{
    public class ShoutUser : BaseDataObject
    {
        public Guid ShoutId { get; set; }
        public String UserId { get; set; }
    }
}