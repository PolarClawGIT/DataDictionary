using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
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
                        if (aliasItem.TryScope() is IDbElementScopeKey scopeKey &&
                            DbTableColumnKeyName.TryCreate(aliasItem) is DbTableColumnKeyName columnKey)
                        {
                            result.Add(new DbExtendedPropertyParameter()
                            {
                                DatabaseName = columnKey.DatabaseName,
                                PropertyName = propertyName,
                                PropertyValue = propertyItem.PropertyValue,
                                Level0Type = scopeKey.CatalogScope.ToString(),
                                Level0Name = columnKey.SchemaName,
                                Level1Type = scopeKey.ObjectScope.ToString(),
                                Level1Name = columnKey.TableName,
                                Level2Type = scopeKey.ElementScope.ToString(),
                                Level2Name = columnKey.ColumnName
                            }) ;
                        }
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
            List<DbExtendedPropertyParameter> result = new List<DbExtendedPropertyParameter>();

            foreach (DomainEntityItem entityItem in data.DomainEntities)
            {
                DomainEntityKey entityKey = new DomainEntityKey(entityItem);

                foreach (DomainEntityPropertyItem propertyItem in data.GetProperties(entityKey))
                {
                    PropertyKey propertyKey = new PropertyKey(propertyItem);
                    String propertyName = string.Empty;
                    PropertyItem? propertypeType = data.Properties.FirstOrDefault(
                        w => propertyKey.Equals(w)
                        && w.IsExtendedProperty == true
                        && !String.IsNullOrWhiteSpace(w.ExtendedProperty));

                    if (propertypeType is not null && propertypeType.ExtendedProperty is String)
                    { propertyName = propertypeType.ExtendedProperty; }

                    foreach (DomainEntityAliasItem aliasItem in data.GetAliases(entityKey))
                    {
                        if (aliasItem.TryScope() is IDbObjectScopeKey scopeKey &&
                            DbTableKeyName.TryCreate(aliasItem) is DbTableKeyName tableKey)
                        {
                            result.Add(new DbExtendedPropertyParameter()
                            {
                                DatabaseName = tableKey.DatabaseName,
                                PropertyName = propertyName,
                                PropertyValue = propertyItem.PropertyValue,
                                Level0Type = scopeKey.CatalogScope.ToString(),
                                Level0Name = tableKey.SchemaName,
                                Level1Type = scopeKey.ObjectScope.ToString(),
                                Level1Name = tableKey.TableName,
                                Level2Type = String.Empty,
                                Level2Name = String.Empty
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
