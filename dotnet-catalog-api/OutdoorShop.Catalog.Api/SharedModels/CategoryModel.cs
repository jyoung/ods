namespace OutdoorShop.Catalog.Api.SharedModels
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Category")]
    public class CategoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}