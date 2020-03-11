namespace OutdoorShop.Catalog.Domain
{
    using System;
    using Newtonsoft.Json;

    public abstract class DocumentBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        // the etag comes from Azure adding an etag to the document
        [JsonProperty(PropertyName = "_etag")]
        public string ETag { get; set; }
    }
}