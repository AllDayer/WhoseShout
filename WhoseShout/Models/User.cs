using System;


namespace WhoseShout.Models
{
    public class User : BaseDataObject
    {
        public String UserId { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}