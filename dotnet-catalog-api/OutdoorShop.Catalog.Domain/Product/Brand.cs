using Newtonsoft.Json;

namespace OutdoorShop.Catalog.Domain.Product
{
    public class Brand 
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}