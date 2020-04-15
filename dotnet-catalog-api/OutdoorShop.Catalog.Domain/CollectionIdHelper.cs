namespace OutdoorShop.Catalog.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System;
    using Pluralize.NET.Core;

    public static class CollectionIdHelper
    {
        public static string GetId(Type type)
        {
            var name = type.Name;

            if (name.Contains("Document"))
            {
                var index = name.IndexOf("Document");
                name = name.Remove(index, 8);
            }

            return new Pluralizer().Pluralize(name);
        }
    }
}
