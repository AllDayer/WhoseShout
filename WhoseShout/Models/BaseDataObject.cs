using System;

#if BACKEND
using Microsoft.Azure.Mobile.Server;

#endif

namespace WhoseShout.Models
{

    public interface IBaseDataObject
    {
        string Id { get; set; }
    }
#if BACKEND
    public class BaseDataObject : EntityData
    {
        public BaseDataObject ()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string RemoteId { get; set; }
    }
#else
    public class BaseDataObject :  IBaseDataObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Microsoft.WindowsAzure.MobileServices.CreatedAt]
        public DateTimeOffset? CreatedAt { get; set; }

        public string RemoteId { get; set; }

        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
    }
#endif
}