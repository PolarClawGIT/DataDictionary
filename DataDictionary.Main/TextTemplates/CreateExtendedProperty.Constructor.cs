using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.TextTemplates
{
    partial class CreateExtendedProperty
    {
        IEnumerable<DbExtendedPropertyParameter> attributeData;
        IEnumerable<DbExtendedPropertyParameter> entityData;

        internal CreateExtendedProperty(ModelData data)
        {
            attributeData = GetAttributeDescriptions(data);
            entityData = GetEntityDescriptions(data);
        }

        /// <summary>
        /// Gets the MS Description Extended Properties for Attributes
        /// </summary>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyParameter> GetAttributeDescriptions(ModelData data)
        {
            throw new NotImplementedException("This needs to be re-factored");
            List<DbExtendedPropertyParameter> result = new List<DbExtendedPropertyParameter>();

            foreach (DomainAttributeItem attributeItem in data.DomainAttributes)
            {
                DomainAttributeKey attributeKey = new DomainAttributeKey(attributeItem);

                foreach (DomainAttributePropertyItem propertyItem in data.GetProperties(attributeKey))
                {
                    PropertyKey propertyKey = new PropertyKey(propertyItem);
                    String propertyName = string.Empty;
                    PropertyItem? propertypeType = data.Properties.FirstOrDefault(
                        w => propertyKey.Equals(w)
                        && w.IsExtendedProperty == true
                        && !String.IsNullOrWhiteSpace(w.ExtendedProperty));

                    if (propertypeType is not null && propertypeType.ExtendedProperty is String)
                    { propertyName = propertypeType.ExtendedProperty; }

                    foreach (DomainAttributeAliasItem aliasItem in data.GetAliases(attributeKey))
                    {

                        DbExtendedPropertyParameter value = new DbExtendedPropertyParameter()
                        {
                            //DatabaseName = aliasItem.DatabaseName,
                            //PropertyName = propertyName,
                            //PropertyValue = propertyItem.PropertyValue,
                            //Level0Type = aliasItem.CatalogScope.ToString(),
                            //Level0Name = aliasItem.SchemaName,
                            //Level1Type = aliasItem.ObjectScope.ToString(),
                            //Level1Name = aliasItem.ObjectName,
                            //Level2Type = aliasItem.ElementScope.ToString(),
                            //Level2Name = aliasItem.ElementName
                        };

                        result.Add(value);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the MS Description Extended Properties for Entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<DbExtendedPropertyParameter> GetEntityDescriptions(ModelData data)
        {
            throw new NotImplementedException("This needs to be re-factored");
            List<DbExtendedPropertyParameter> result = new List<DbExtendedPropertyParameter>();

            foreach (DomainEntityItem entityItem in data.DomainEntities)
            {
                DomainEntityKey entityKey = new DomainEntityKey(entityItem);

                foreach (DomainEntityPropertyItem propertyItem in data.GetProperties(entityKey))
                {
                    PropertyKey propertyKey = new PropertyKey(propertyItem);
                    String propertyName = String.Empty ;

                    PropertyItem? propertypeType = data.Properties.FirstOrDefault(
                         w => propertyKey.Equals(w)
                         && w.IsExtendedProperty == true
                         && !String.IsNullOrWhiteSpace(w.ExtendedProperty));

                    if (propertypeType is not null && propertypeType.ExtendedProperty is String)
                    { propertyName = propertypeType.ExtendedProperty; }

                    foreach (DomainEntityAliasItem aliasItem in data.GetAliases(entityKey))
                    {
                        DbExtendedPropertyParameter value = new DbExtendedPropertyParameter()
                        {
                            //DatabaseName = aliasItem.DatabaseName,
                            //PropertyName = propertyName,
                            //PropertyValue = propertyItem.PropertyValue,
                            //Level0Type = aliasItem.CatalogScope.ToString(),
                            //Level0Name = aliasItem.SchemaName,
                            //Level1Type = aliasItem.ObjectScope.ToString(),
                            //Level1Name = aliasItem.ObjectName,
                        };

                        result.Add(value);
                    }
                }
            }

            return result;
        }
    }
}
