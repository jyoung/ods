namespace OutdoorShop.Catalog.Api.SharedModels
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Brand Model
    /// </summary>
    [DataContract(Name = "Brand")]
    public class BrandModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}