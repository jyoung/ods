namespace OutdoorShop.Catalog.Api.SharedModels
{
    using System.Runtime.Serialization;

    [DataContract(Name = "Price")]
    public class PriceModel
    {
        public string Currency { get; set; }
        public decimal Value { get; set; }
    }
}