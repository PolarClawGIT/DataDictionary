using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        void CreateAttribute(IDbTableColumnKeyName columnKey)
        {
            DbTableColumnKeyName key = new DbTableColumnKeyName(columnKey);
            DomainAttributeUniqueKey attributeKeyUnique = new DomainAttributeUniqueKey(columnKey);
            AliasKeyName aliasKey = new AliasKeyName(columnKey);

            if (this.DbTableColumns.FirstOrDefault(w => key.Equals(w)) is DbTableColumnItem columnItem
                && this.DbCatalogs.FirstOrDefault(w => new DbCatalogKey(columnItem).Equals(w)) is DbCatalogItem catalogItem
                )
            {
                DomainAttributeItem? attributeItem = this.DomainAttributes.FirstOrDefault(w => attributeKeyUnique.Equals(w));
                DomainAttributeAliasItem? aliasItem = this.DomainAttributeAliases.FirstOrDefault(w => aliasKey.Equals(w));
                DomainAttributeKey attributeKey;

                if (attributeItem is null && aliasItem is not null)
                {
                    attributeKey = new DomainAttributeKey(aliasItem);
                    attributeItem = this.DomainAttributes.FirstOrDefault(w => attributeKey.Equals(w));
                }

                if (attributeItem is null)
                {
                    attributeItem = new DomainAttributeItem() { AttributeTitle = columnItem.ColumnName };
                    this.DomainAttributes.Add(attributeItem);
                }

                attributeKey = new DomainAttributeKey(attributeItem);

                if (aliasItem is null)
                {
                    aliasItem = new DomainAttributeAliasItem(attributeKey)
                    {
                        AliasName = columnItem.ToAliasName(),
                        ScopeName = columnItem.ScopeName
                    };
                    this.DomainAttributeAliases.Add(aliasItem);
                }

                foreach (DbExtendedPropertyItem propertyItem in this.GetExtendedProperty(columnItem))
                {
                    PropertyKeyExtended propertyByName = new PropertyKeyExtended(propertyItem);

                    if (this.Properties.FirstOrDefault(w => propertyByName.Equals(w)) is PropertyItem property)
                    {
                        PropertyKey propertyKey = new PropertyKey(property);
                        DomainAttributePropertyItem? attributeProperty = this.DomainAttributeProperties.FirstOrDefault(w => attributeKey.Equals(w) && propertyByName.Equals(w));

                        // Special Handling, I want to copy the MS_Description to the Attribute Description if there is none.
                        if (String.IsNullOrWhiteSpace(attributeItem.AttributeDescription) && propertyByName.ExtendedProperty == "MS_Description")
                        { attributeItem.AttributeDescription = propertyItem.PropertyValue; }

                        if (attributeProperty is null)
                        {
                            attributeProperty = new DomainAttributePropertyItem(attributeItem, propertyKey, propertyItem);
                            this.DomainAttributeProperties.Add(attributeProperty);
                        }
                    }
                }
            }
        }

        void CreateAttribute(ILibraryMemberKey memberKey)
        {
            LibraryMemberKey key = new LibraryMemberKey(memberKey);
            throw new NotImplementedException("Work in Progress");
        }

        /// <summary>
        /// Creates the Work Items to import a Database Column into an Attribute
        /// </summary>
        /// <param name="columnKey"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbTableColumnKeyName columnKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = String.Format("Import {0}", columnKey.ToString()),
                DoWork = () => CreateAttribute(columnKey)
            });

            return work;
        }

        /// <summary>
        /// Imports all the Attributes for a given Database Table
        /// </summary>
        /// <param name="tableKey"></param>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbTableKeyName tableKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            DbTableKeyName key = new DbTableKeyName(tableKey);
            foreach (DbTableColumnItem item in this.DbTableColumns.Where(w => key.Equals(w)))
            { work.AddRange(ImportAttribute(item)); }

            return work;
        }

        /// <summary>
        /// Imports all the Attributes for a given Database Schema
        /// </summary>
        /// <param name="schemaKey"></param>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbSchemaKeyName schemaKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            DbSchemaKeyName key = new DbSchemaKeyName(schemaKey);
            foreach (DbTableItem item in this.DbTables.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportAttribute(item)); }

            return work;
        }

        /// <summary>
        /// Imports all the Attributes for a given Database Schema
        /// </summary>
        /// <param name="catalogKey"></param>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbCatalogKeyName catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKeyName key = new DbCatalogKeyName(catalogKey);

            foreach (DbSchemaItem item in this.DbSchemta.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportAttribute(item)); }

            return work;
        }

        void CreateEntity(IDbTableKeyName tableKey)
        {
            DbTableKeyName key = new DbTableKeyName(tableKey);
            DomainEntityUniqueKey entityKeyUnique = new DomainEntityUniqueKey(tableKey);
            AliasKeyName aliasKey = new AliasKeyName(tableKey);

            if (this.DbTables.FirstOrDefault(w => key.Equals(w)) is DbTableItem tableItem
                && this.DbCatalogs.FirstOrDefault(w => new DbCatalogKey(tableItem).Equals(w)) is DbCatalogItem catalogItem)
            {
                DomainEntityItem? entityItem = this.DomainEntities.FirstOrDefault(w => entityKeyUnique.Equals(w));
                DomainEntityAliasItem? aliasItem = this.DomainEntityAliases.FirstOrDefault(w => aliasKey.Equals(w));
                DomainEntityKey entityKey;

                if (entityItem is null && aliasItem is not null)
                {
                    entityKey = new DomainEntityKey(aliasItem);
                    entityItem = this.DomainEntities.FirstOrDefault(w => entityKey.Equals(w));
                }

                if (entityItem is null)
                {
                    entityItem = new DomainEntityItem() { EntityTitle = tableItem.TableName };
                    this.DomainEntities.Add(entityItem);
                }

                entityKey = new DomainEntityKey(entityItem);

                if (aliasItem is null)
                {
                    aliasItem = new DomainEntityAliasItem(entityKey)
                    {
                        AliasName = tableItem.ToAliasName(),
                        ScopeName = tableItem.ScopeName
                    };
                    this.DomainEntityAliases.Add(aliasItem);
                }

                foreach (DbExtendedPropertyItem propertyItem in this.GetExtendedProperty(tableItem))
                {
                    PropertyKeyExtended propertyByName = new PropertyKeyExtended(propertyItem);

                    if (this.Properties.FirstOrDefault(w => propertyByName.Equals(w)) is PropertyItem property)
                    {
                        PropertyKey propertyKey = new PropertyKey(property);
                        DomainEntityPropertyItem? entityProperty = this.DomainEntityProperties.FirstOrDefault(w => entityKey.Equals(w) && propertyByName.Equals(w));

                        // Special Handling, I want to copy the MS_Description to the Attribute Description if there is none.
                        if (String.IsNullOrWhiteSpace(entityItem.EntityDescription) && propertyByName.ExtendedProperty == "MS_Description")
                        { entityItem.EntityDescription = propertyItem.PropertyValue; }

                        if (entityProperty is null)
                        {
                            entityProperty = new DomainEntityPropertyItem(entityItem, propertyKey, propertyItem);
                            this.DomainEntityProperties.Add(entityProperty);
                        }
                    }
                }
            }
        }

        void CreateEntity(ILibraryMemberKey memberKey) { }

        /// <summary>
        /// Imports the Entity for a given Database Table
        /// </summary>
        /// <param name="tableKey"></param>
        public IReadOnlyList<WorkItem> ImportEntity(IDbTableKeyName tableKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = String.Format("Import {0}", tableKey.ToString()),
                DoWork = () => CreateEntity(tableKey)
            });

            return work;
        }

        /// <summary>
        /// Imports all the Entities for a given Database Schema
        /// </summary>
        /// <param name="schemaKey"></param>
        public IReadOnlyList<WorkItem> ImportEntity(IDbSchemaKeyName schemaKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            DbSchemaKeyName key = new DbSchemaKeyName(schemaKey);
            foreach (DbTableItem item in this.DbTables.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportEntity(item)); }

            return work;
        }

        /// <summary>
        /// Imports all the Entities for a given Database Schema
        /// </summary>
        /// <param name="catalogKey"></param>
        public IReadOnlyList<WorkItem> ImportEntity(IDbCatalogKeyName catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKeyName key = new DbCatalogKeyName(catalogKey);

            foreach (DbSchemaItem item in this.DbSchemta.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportEntity(item)); }

            return work;
        }
    }
}

