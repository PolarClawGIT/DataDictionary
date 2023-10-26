using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
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

        void CreateAttribute(IDbTableColumnKey columnKey)
        {
            DbTableColumnKey key = new DbTableColumnKey(columnKey);
            DomainAttributeKeyUnique attributeKeyUnique = new DomainAttributeKeyUnique(columnKey);
            DomainAttributeAliasKey aliasKey = new DomainAttributeAliasKey(columnKey);

            if (this.DbTableColumns.FirstOrDefault(w => key.Equals(w)) is DbTableColumnItem columnItem)
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
                    aliasItem = new DomainAttributeAliasItem(attributeKey, columnItem);
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

        void CreateAttribute(IDbRoutineParameterKey parameterKey)
        {
            DbRoutineParameterKey key = new DbRoutineParameterKey(parameterKey);
            DomainAttributeKeyUnique attributeKeyUnique = new DomainAttributeKeyUnique(parameterKey);
            DomainAttributeAliasKey aliasKey = new DomainAttributeAliasKey(parameterKey);

            if (this.DbRoutineParameters.FirstOrDefault(w => key.Equals(w)) is DbRoutineParameterItem parameterItem)
            {
                DomainAttributeItem? attributeItem = this.DomainAttributes.FirstOrDefault(w => attributeKeyUnique.Equals(w));
                DomainAttributeAliasItem? aliasItem = this.DomainAttributeAliases.FirstOrDefault(w => aliasKey.Equals(w));
                DomainAttributeKey attributeKey;

                if (attributeItem is null && aliasItem is not null)
                {
                    attributeKey = new DomainAttributeKey(aliasItem);
                    attributeItem = this.DomainAttributes.FirstOrDefault(w => attributeKey.Equals(w));
                }

                if (attributeItem is null
                    && !String.IsNullOrWhiteSpace(parameterItem.ParameterName)
                    && parameterItem.ParameterName is not "@Return")
                {
                    attributeItem = new DomainAttributeItem() { AttributeTitle = parameterItem.ParameterName.Replace("@", "") };
                    this.DomainAttributes.Add(attributeItem);
                }

                if (attributeItem is not null)
                {
                    attributeKey = new DomainAttributeKey(attributeItem);

                    if (aliasItem is null)
                    {
                        aliasItem = new DomainAttributeAliasItem(attributeKey, parameterItem);
                        this.DomainAttributeAliases.Add(aliasItem);
                    }

                    foreach (DbExtendedPropertyItem propertyItem in this.GetExtendedProperty(parameterItem))
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
        public IReadOnlyList<WorkItem> ImportAttribute(IDbTableColumnKey columnKey)
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
        /// Creates the Work Items to import a Database Parameter into an Attribute
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbRoutineParameterKey parameterKey)
        {

            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem()
            {
                WorkName = String.Format("Import {0}", parameterKey.ToString()),
                DoWork = () => CreateAttribute(parameterKey)
            });

            return work;
        }

        /// <summary>
        /// Imports all the Attributes for a given Database Table
        /// </summary>
        /// <param name="tableKey"></param>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbTableKey tableKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            DbTableKey key = new DbTableKey(tableKey);
            foreach (DbTableColumnItem item in this.DbTableColumns.Where(w => key.Equals(w)))
            { work.AddRange(ImportAttribute(item)); }

            return work;
        }

        /// <summary>
        /// Imports all the Attributes for a given Database Routine
        /// </summary>
        /// <param name="routineKey"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbRoutineKey routineKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            DbRoutineKey key = new DbRoutineKey(routineKey);
            foreach (DbRoutineParameterItem item in this.DbRoutineParameters.Where(w => key.Equals(w)))
            { work.AddRange(ImportAttribute(item)); }

            return work;
        }

        /// <summary>
        /// Imports all the Attributes for a given Database Schema
        /// </summary>
        /// <param name="schemaKey"></param>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbSchemaKey schemaKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            DbSchemaKey key = new DbSchemaKey(schemaKey);
            foreach (DbTableItem item in this.DbTables.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportAttribute(item)); }

            foreach (DbRoutineItem item in this.DbRoutines.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportAttribute(item)); }

            return work;
        }

        /// <summary>
        /// Imports all the Attributes for a given Database Schema
        /// </summary>
        /// <param name="catalogKey"></param>
        public IReadOnlyList<WorkItem> ImportAttribute(IDbCatalogKeyUnique catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKeyUnique key = new DbCatalogKeyUnique(catalogKey);

            foreach (DbSchemaItem item in this.DbSchemta.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportAttribute(item)); }

            return work;
        }

        void CreateEntity(IDbTableKey tableKey)
        {
            DbTableKey key = new DbTableKey(tableKey);
            DomainEntityKeyUnique entityKeyUnique = new DomainEntityKeyUnique(tableKey);
            DomainEntityAliasKey aliasKey = new DomainEntityAliasKey(tableKey);

            if (this.DbTables.FirstOrDefault(w => key.Equals(w)) is DbTableItem tableItem)
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
                    aliasItem = new DomainEntityAliasItem(entityKey, tableItem);
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

        void CreateEntity(IDbSchemaKey schemaKey) { }

        void CreateEntity(IDbDomainKey domainKey) { }

        void CreateEntity(ILibraryMemberKey memberKey) { }

        void CreateProcess(IDbRoutineKey routineKey) { }

        void CreateProcess(ILibraryMemberKey memberKey) { }

        /// <summary>
        /// Imports the Entity for a given Database Table
        /// </summary>
        /// <param name="tableKey"></param>
        public IReadOnlyList<WorkItem> ImportEntity(IDbTableKey tableKey)
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
        public IReadOnlyList<WorkItem> ImportEntity(IDbSchemaKey schemaKey)
        {
            List<WorkItem> work = new List<WorkItem>();

            DbSchemaKey key = new DbSchemaKey(schemaKey);
            foreach (DbTableItem item in this.DbTables.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportEntity(item)); }

            return work;
        }

        /// <summary>
        /// Imports all the Entities for a given Database Schema
        /// </summary>
        /// <param name="catalogKey"></param>
        public IReadOnlyList<WorkItem> ImportEntity(IDbCatalogKeyUnique catalogKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            DbCatalogKeyUnique key = new DbCatalogKeyUnique(catalogKey);

            foreach (DbSchemaItem item in this.DbSchemta.Where(w => key.Equals(w) && w.IsSystem == false))
            { work.AddRange(ImportEntity(item)); }

            return work;
        }
    }
}

