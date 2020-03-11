namespace OutdoorShop.Catalog.Api.Models
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Category")]
    public class CategoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}