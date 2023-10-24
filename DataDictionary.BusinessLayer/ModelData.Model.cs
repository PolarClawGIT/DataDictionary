using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {

        /// <summary>
        /// Default method to import a Database Schema into the Attributes/Entities of the Model.
        /// </summary>
        public void ImportDbSchemaToDomain()
        {
            //TODO: This needs to be broken down and geared to run as a background task.
            //Be able to call by CatalogKey, SchemaKey, Table/View Key, RoutineKey, ColumnKey, LibrarySourceKey, LibraryMemberKey

            ModelData data = this;
            
            IEnumerable<IDbTableColumnItem> newAttributes = data.DbTableColumns.Where(
                w => w.GetTable(data.DbTables) is IDbTableItem table && !table.IsSystem && // Do not want System Tables
                w.GetSchema(data.DbSchemta) is IDbSchemaItem schema && !schema.IsSystem && // Do not want System Schemta
                data.DomainAttributeAliases.FirstOrDefault(a => // Do not want Columns already aliased to an attribute
                {
                    DomainAttributeAliasKey key = new DomainAttributeAliasKey(w);
                    return key.Equals(a);
                }) is null).ToList();

            List<DbTableItem> newEntites = data.DbTables.Where(
               w => !w.IsSystem && // Do not want System Tables
               w.GetSchema(data.DbSchemta) is IDbSchemaItem schema && !schema.IsSystem && // Do not want System Schemta
               data.DomainEntityAliases.FirstOrDefault(a => // Do not want Tables already aliased to an entity
               {
                   DomainEntityAliasKey key = new DomainEntityAliasKey(w);
                   return key.Equals(a);
               }) is null).ToList();

            // Get the Column information and create Attributes
            foreach (IGrouping<DomainAttributeKeyUnique, IDbTableColumnItem> columnItem in newAttributes.GroupBy(g => new DomainAttributeKeyUnique(g)))
            {
                IDbTableColumnItem columnSource = columnItem.First();
                DomainAttributeItem newAttribute = new DomainAttributeItem() { AttributeTitle = columnSource.ColumnName };
                List<IDbExtendedPropertyItem> propeties = new List<IDbExtendedPropertyItem>();

                data.DomainAttributes.Add(newAttribute);

                foreach (IDbTableColumnItem aliasSource in columnItem)
                {
                    DbCatalogScope catalogScope = DbCatalogScope.NULL;
                    DbObjectScope objectScope = DbObjectScope.NULL;

                    if (columnSource.GetSchema(data.DbSchemta) is DbSchemaItem schema) { catalogScope = schema.CatalogScope; }
                    if (columnSource.GetTable(data.DbTables) is DbTableItem table) { objectScope = table.ObjectScope; }

                    data.DomainAttributeAliases.Add(
                        new DomainAttributeAliasItem(newAttribute, aliasSource)
                        {
                            CatalogScope = catalogScope,
                            ObjectScope = objectScope,
                            ElementScope = columnSource.ElementScope
                        });

                    propeties.AddRange(data.GetExtendedProperty(aliasSource));
                }

                foreach (IGrouping<String?, IDbExtendedPropertyItem> propertyItem in propeties.GroupBy(g => g.PropertyName))
                {
                    IDbExtendedPropertyItem propertySource = propertyItem.First();

                    if (data.Properties.FirstOrDefault(w =>
                            w.ExtendedProperty is not null &&
                            w.ExtendedProperty.Equals(propertySource.PropertyName, KeyExtension.CompareString)) is IPropertyItem property)
                    {
                        data.DomainAttributeProperties.
                            Add(new DomainAttributePropertyItem(newAttribute, property, propertySource));

                        if (property.IsExtendedProperty == true
                            && property.ExtendedProperty == "MS_Description"
                            && String.IsNullOrWhiteSpace(newAttribute.AttributeDescription))
                        { newAttribute.AttributeDescription = propertySource.PropertyValue; }
                    }
                }
            }

            // Get the Table Information and create Entities
            foreach (IGrouping<DomainEntityKeyUnique, DbTableItem> tableItem in newEntites.GroupBy(g => new DomainEntityKeyUnique(g)))
            {
                IDbTableItem tableSource = tableItem.First();
                DomainEntityItem newEntity = new DomainEntityItem() { EntityTitle = tableSource.TableName };
                List<IDbExtendedPropertyItem> propeties = new List<IDbExtendedPropertyItem>();

                data.DomainEntities.Add(newEntity);

                foreach (IDbTableItem aliasSource in tableItem)
                {
                    DbCatalogScope catalogScope = DbCatalogScope.NULL;
                    DbObjectScope objectScope = DbObjectScope.NULL;

                    if (tableSource.GetSchema(data.DbSchemta) is DbSchemaItem schema) { catalogScope = schema.CatalogScope; }
                    if (tableSource.GetTable(data.DbTables) is DbTableItem table) { objectScope = table.ObjectScope; }

                    data.DomainEntityAliases.Add(
                        new DomainEntityAliasItem(newEntity, aliasSource)
                        {
                            CatalogScope = catalogScope,
                            ObjectScope = objectScope
                        });

                    propeties.AddRange(data.GetExtendedProperty(aliasSource));
                }

                foreach (IGrouping<String?, IDbExtendedPropertyItem> propertyItem in propeties.GroupBy(g => g.PropertyName))
                {
                    IDbExtendedPropertyItem propertySource = propertyItem.First();

                    if (data.Properties.FirstOrDefault(w =>
                            w.ExtendedProperty is not null &&
                            w.ExtendedProperty.Equals(propertySource.PropertyName, KeyExtension.CompareString)) is IPropertyItem property)
                    {
                        data.DomainEntityProperties.
                            Add(new DomainEntityPropertyItem(newEntity, property, propertySource));

                        if (property.IsExtendedProperty == true
                            && property.ExtendedProperty == "MS_Description"
                            && String.IsNullOrWhiteSpace(newEntity.EntityDescription))
                        { newEntity.EntityDescription = propertySource.PropertyValue; }
                    }
                }
            }
        }
    }
}
