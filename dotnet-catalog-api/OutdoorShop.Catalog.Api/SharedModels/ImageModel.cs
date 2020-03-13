namespace OutdoorShop.Catalog.Api.SharedModels
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Image")]
    public class ImageModel
    {
        public string SmallUrl { get; set; }
        public string LargeUrl { get; set; }
    }
}