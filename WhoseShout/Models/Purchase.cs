using System;

namespace WhoseShout.Models
{
    public class Purchase : BaseDataObject
    {
        public Guid PurchaseId { get; set; }
        public Guid ShoutTrackerId { get; set; }
        public string UserId { get; set; }
        public float Amount { get; set; }
    }
}