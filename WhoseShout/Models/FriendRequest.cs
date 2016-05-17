using System;
using WhoseShout.Helpers;

namespace WhoseShout.Models
{
    public class FriendRequest
    {
        public Guid UserId { get; set; }
        public Guid FutureFriendId { get; set; }
        public string Message { get; set; }
        public bool ApproveFlag { get; set; }
        public bool RejectFlag { get; set; }
        public bool BlockFlag { get; set; }
        public bool SpamFlag { get; set; }
    }
}