namespace OutdoorShop.Catalog.Api.Models
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Image")]
    public class ImageModel
    {
        public string SmallUrl { get; set; }
        public string LargeUrl {get; set;}
    }
}