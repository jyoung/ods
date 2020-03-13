namespace OutdoorShop.Catalog.Api.SharedModels
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "Copy")]
    public class CopyModel
    {
        public string Description { get; set; }
        public string Notes { get; set; }
        public List<string> Bullets { get; set; }

        public CopyModel()
        {
            Bullets = new List<string>();
        }
    }
}