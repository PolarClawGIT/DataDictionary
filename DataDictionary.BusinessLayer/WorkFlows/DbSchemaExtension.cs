using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    public static class DbSchemaExtension
    {

        public static IReadOnlyList<WorkItem> LoadDbSchema(this ModelData data, DbSchemaContext context)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(context);
            workItems.Add(openConnection);

            workItems.AddRange(data.RemoveCatalog(context));

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbCatalogs",
                Command = DbCatalogItem.GetSchema,
                Target = data.DbCatalogs
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbSchemta",
                Command = DbSchemaItem.GetSchema,
                Target = data.DbSchemta
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbTables",
                Command = DbTableItem.GetSchema,
                Target = data.DbTables
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbColumns",
                Command = DbColumnItem.GetSchema,
                Target = data.DbColumns
            });

            workItems.Add(new LoadExtendedProperties<DbSchemaItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbSchemta",
                Source = data.DbSchemta,
                Target = data.DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbTableItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbTables",
                Source = data.DbTables,
                Target = data.DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbColumnItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbColumns",
                Source = data.DbColumns,
                Target = data.DbExtendedProperties
            });

            return workItems.AsReadOnly();
        }

        public static IReadOnlyList<WorkItem> RemoveCatalog(this ModelData data, DbSchemaContext context)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            workItems.Add(new RemoveCatalog<DbExtendedPropertyItem>()
            {
                WorkName = "Remove DbExtendedProperties by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = data.DbExtendedProperties
            });

            workItems.Add(new RemoveCatalog<DbColumnItem>()
            {
                WorkName = "Remove DbColumns by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = data.DbColumns
            });

            workItems.Add(new RemoveCatalog<DbTableItem>()
            {
                WorkName = "Remove DbTables by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = data.DbTables
            });

            workItems.Add(new RemoveCatalog<DbSchemaItem>()
            {
                WorkName = "Remove DbSchemta by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = data.DbSchemta
            });

            workItems.Add(new RemoveCatalog<DbCatalogItem>()
            {
                WorkName = "Remove DbCatalogs by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = data.DbCatalogs
            });

            return workItems.AsReadOnly();
        }

        public static void ImportDbSchemaToDomain(this ModelData data)
        {
            IEnumerable<IDbColumnItem> newAttributes = data.DbColumns.Where(
                w => w.GetTable(data.DbTables) is IDbTableItem table && !table.IsSystem && // Do not want System Tables
                w.GetSchema(data.DbSchemta) is IDbSchemaItem schema && !schema.IsSystem && // Do not want System Schemta
                data.DomainAttributeAliases.FirstOrDefault( // Do not want Columns already aliased to an attribute
                    a => w.CatalogName == a.CatalogName &&
                    w.SchemaName == a.SchemaName &&
                    w.TableName == a.ObjectName &&
                    w.ColumnName == a.ElementName)
                is null);


            foreach (IGrouping<String?, IDbColumnItem> columnItem in newAttributes.GroupBy(g => g.ColumnName))
            {
                IDbColumnItem columnSource = columnItem.First();
                DomainAttributeItem newAttribute = new DomainAttributeItem() { AttributeTitle = columnSource.ColumnName };
                List<IDbExtendedPropertyItem> propeties = new List<IDbExtendedPropertyItem>();

                data.DomainAttributes.Add(newAttribute);

                foreach (IDbColumnItem aliasSource in columnItem)
                {
                    data.DomainAttributeAliases.Add(new DomainAttributeAliasItem()
                    {
                        AttributeId = newAttribute.AttributeId,
                        CatalogName = aliasSource.CatalogName,
                        SchemaName = aliasSource.SchemaName,
                        ObjectName = aliasSource.TableName,
                        ElementName = aliasSource.ColumnName
                    });

                    propeties.AddRange(aliasSource.GetProperties(data.DbExtendedProperties));
                }

                foreach (IGrouping<String?, IDbExtendedPropertyItem> propertyItem in propeties.GroupBy(g => g.PropertyName))
                {
                    IDbExtendedPropertyItem propertySource = propertyItem.First();

                    data.DomainAttributeProperties.Add(
                        new DomainAttributePropertyItem()
                        {
                            AttributeId = newAttribute.AttributeId,
                            PropertyName = propertySource.PropertyName,
                            PropertyValue = propertySource.PropertyValue
                        });

                    if (propertySource.PropertyName is not null &&
                        data.DomainAttributeProperties.
                        Count(w => w.PropertyName == propertySource.PropertyName) == 0)
                    {
                        data.DomainAttributeProperties.Add(
                        new DomainAttributePropertyItem()
                        {
                            AttributeId = newAttribute.AttributeId,
                            PropertyName = propertySource.PropertyName,
                            PropertyValue = propertySource.PropertyValue
                        });
                    }
                }
            }

        }
    }
}
