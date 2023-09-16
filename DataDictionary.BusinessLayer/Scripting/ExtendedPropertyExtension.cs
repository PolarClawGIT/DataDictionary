using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    public static class ExtendedPropertyExtension
    {

        public static IEnumerable<DbExtendedPropertyParameter> ExtendedProperties(this ModelData data)
        {
            List<DbExtendedPropertyParameter> result = new List<DbExtendedPropertyParameter>();


            foreach (DomainAttributeItem attributeItem in data.DomainAttributes)
            {
                DomainAttributeKey attributeKey = new DomainAttributeKey(attributeItem);

                foreach (DomainAttributePropertyItem propertyItem in data.DomainAttributeProperties.GetProperties(attributeKey))
                {
                    PropertyKey propertyKey = new PropertyKey(propertyItem);
                    String propertyName = "MS_Description";
                    PropertyItem? propertypeType = data.Properties.FirstOrDefault(w => new PropertyKey(w) == propertyKey);

                    if(propertypeType is not null && propertypeType.ExtendedProperty is String)
                    { propertyName = propertypeType.ExtendedProperty; }

                    foreach (DomainAttributeAliasItem aliasItem in data.DomainAttributeAliases.GetAliases(attributeKey))
                    {
                        DbExtendedPropertyParameter value = new DbExtendedPropertyParameter()
                        {
                            PropertyName = propertyName,
                            PropertyValue = propertyItem.PropertyValue,
                            Level0Type = aliasItem.CatalogScope.ToString(),
                            Level0Name = aliasItem.SchemaName,
                            Level1Type = aliasItem.ObjectScope.ToString(),
                            Level1Name = aliasItem.ObjectName,
                            Level2Type = aliasItem.ElementScope.ToString(),
                            Level2Name = aliasItem.ElementName
                        };

                        result.Add(value);
                    }
                }
            }

            return result;
        }
    }
}
