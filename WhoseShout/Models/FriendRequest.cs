using System;

namespace WhoseShout.Models
{
    public class FriendRequest : BaseDataObject
    {
        public String UserId { get; set; }
        public String FutureFriendId { get; set; }
        public string Message { get; set; }
        public bool ApproveFlag { get; set; }
        public bool RejectFlag { get; set; }
        public bool BlockFlag { get; set; }
        public bool SpamFlag { get; set; }
    }
}