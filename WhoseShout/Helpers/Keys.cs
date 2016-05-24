using System;

namespace WhoseShout.Helpers
{
    public static class Keys
    {
        public static readonly string AzureServiceUrl = "http://whoseshout.azurewebsites.net";


        const string DatabaseIdKey = "azure_database";
        static readonly int DatabaseIdDefault = 0;

        public static int DatabaseId { get; set; }
        public static int UpdateDatabaseId()
        {
            return DatabaseId++;
        }
    }
}