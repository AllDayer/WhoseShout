using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoseShoutWeb.DataObjects
{
    public class FriendRequest : EntityData
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