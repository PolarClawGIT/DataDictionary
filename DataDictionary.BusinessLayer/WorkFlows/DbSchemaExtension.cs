﻿using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.WorkFlows
{
    /// <summary>
    /// Methods and Function dealing with working the Database Schema.
    /// </summary>
    public static class DbSchemaExtension
    {
        /// <summary>
        /// Reads the Database Schema from a specified context into the data model.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadDbSchema(this ModelData data, DbSchemaContext context)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(context);
            workItems.Add(openConnection);

            DbCatalogKey catalogKey;
            if (data.DbCatalogs.FirstOrDefault(w => w.SourceDatabaseName == context.DatabaseName) is DbCatalogItem existing)
            { catalogKey = new DbCatalogKey(existing); }
            else { catalogKey = new DbCatalogKey(new DbCatalogItem()); }

            workItems.AddRange(data.RemoveCatalog(context));

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbCatalogs",
                Command = (conn) => data.DbCatalogs.SchemaCommand(conn, catalogKey),
                Target = data.DbCatalogs
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbSchemta",
                Command = (conn) => data.DbSchemta.SchemaCommand(conn, catalogKey),
                Target = data.DbSchemta
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbDomains",
                Command = (conn) => data.DbDomains.SchemaCommand(conn, catalogKey),
                Target = data.DbDomains
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbTables",
                Command = (conn) => data.DbTables.SchemaCommand(conn, catalogKey),
                Target = data.DbTables
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbColumns",
                Command = (conn) => data.DbTableColumns.SchemaCommand(conn, catalogKey),
                Target = data.DbTableColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraints",
                Command = (conn) => data.DbConstraints.SchemaCommand(conn, catalogKey),
                Target = data.DbConstraints
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbConstraintColumns",
                Command = (conn) => data.DbConstraintColumns.SchemaCommand(conn, catalogKey),
                Target = data.DbConstraintColumns
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutines",
                Command = (conn) => data.DbRoutines.SchemaCommand(conn, catalogKey),
                Target = data.DbRoutines
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load DbRoutineParameters",
                Command = (conn) => data.DbRoutineParameters.SchemaCommand(conn, catalogKey),
                Target = data.DbRoutineParameters
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Load DbRoutineDependencies",
                DoWork = () =>
                {
                    foreach (DbRoutineItem item in data.DbRoutines)
                    {
                        data.DbRoutineDependencies.Load(
                            openConnection.Connection.ExecuteReader(
                                data.DbRoutineDependencies.SchemaCommand(
                                    openConnection.Connection, item)));
                    }
                },
                IsCanceling = openConnection.IsCanceling
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

            workItems.Add(new LoadExtendedProperties<DbTableColumnItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbColumns",
                Source = data.DbTableColumns,
                Target = data.DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbConstraintItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbConstraints",
                Source = data.DbConstraints,
                Target = data.DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbDomainItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbDomains",
                Source = data.DbDomains,
                Target = data.DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbRoutineItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbRoutines",
                Source = data.DbRoutines,
                Target = data.DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbRoutineParameterItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbRoutineParameters",
                Source = data.DbRoutineParameters,
                Target = data.DbExtendedProperties
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Import Schema to Domain Model",
                DoWork = () => ImportDbSchemaToDomain(data),
                IsCanceling = openConnection.IsCanceling
            });

            return workItems.AsReadOnly();
        }

        /// <summary>
        /// Creates work items that Loads all the Catalogs from the Database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadCatalog<T>(this T target)
            where T : IBindingTable<DbCatalogItem>, IReadData
        {
            List<WorkItem> workItems = new List<WorkItem>();

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Catalogs",
                Command = target.LoadCommand,
                Target = target
            });

            return workItems;
        }

        /// <summary>
        /// Removes the Database Schema using the specified context from the Model (by Database Name)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> RemoveCatalog(this ModelData data, DbSchemaContext context)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            workItems.Add(new RemoveCatalog<DbExtendedPropertyItem>()
            {
                WorkName = "Remove DbExtendedProperties by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbExtendedProperties
            });

            workItems.Add(new RemoveCatalog<DbTableItem>()
            {
                WorkName = "Remove DbTables by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbTables
            });

            workItems.Add(new RemoveCatalog<DbTableColumnItem>()
            {
                WorkName = "Remove DbTableColumns by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbTableColumns
            });

            workItems.Add(new RemoveCatalog<DbRoutineItem>()
            {
                WorkName = "Remove DbRoutine by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbRoutines
            });

            workItems.Add(new RemoveCatalog<DbRoutineParameterItem>()
            {
                WorkName = "Remove DbRoutine Parameters by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbRoutineParameters
            });

            workItems.Add(new RemoveCatalog<DbRoutineDependencyItem>()
            {
                WorkName = "Remove DbRoutine Dependencies by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbRoutineDependencies
            });

            workItems.Add(new RemoveCatalog<DbSchemaItem>()
            {
                WorkName = "Remove DbSchemta by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbSchemta
            });

            workItems.Add(new RemoveCatalog<DbCatalogItem>()
            {
                WorkName = "Remove DbCatalogs by Catalog",
                Catalog = new DbCatalogKeyUnique(context),
                Target = data.DbCatalogs
            });

            return workItems.AsReadOnly();
        }

        /// <summary>
        /// Default method to import a Database Schema into the Attributes/Entities of the Model.
        /// </summary>
        /// <param name="data"></param>
        public static void ImportDbSchemaToDomain(this ModelData data)
        {
            IEnumerable<IDbTableColumnItem> newAttributes = data.DbTableColumns.Where(
                w => w.GetTable(data.DbTables) is IDbTableItem table && !table.IsSystem && // Do not want System Tables
                w.GetSchema(data.DbSchemta) is IDbSchemaItem schema && !schema.IsSystem && // Do not want System Schemta
                data.DomainAttributeAliases.FirstOrDefault( // Do not want Columns already aliased to an attribute
                    a => w.DatabaseName == a.DatabaseName &&
                    w.SchemaName == a.SchemaName &&
                    w.TableName == a.ObjectName &&
                    w.ColumnName == a.ElementName)
                is null).ToList();

            List<DbTableItem> newEntites = data.DbTables.Where(
                w => !w.IsSystem && // Do not want System Tables
                w.GetSchema(data.DbSchemta) is IDbSchemaItem schema && !schema.IsSystem && // Do not want System Schemta
                data.DomainEntityAliases.FirstOrDefault( // Do not want Tables already aliased to an entity
                    a => w.DatabaseName == a.DatabaseName &&
                    w.SchemaName == a.SchemaName &&
                    w.TableName == a.ObjectName)
                is null).ToList();

            // Get the Column information and create Attributes
            foreach (IGrouping<String?, IDbTableColumnItem> columnItem in newAttributes.GroupBy(g => g.ColumnName))
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
            foreach (IGrouping<String?, IDbTableItem> tableItem in newEntites.GroupBy(g => g.TableName))
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

        /// <summary>
        /// Gets the Database information.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> GetDatabaseSchema(this DbSchemaContext context, IBindingTable<DbDatabaseItem> target)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(
                new GetInformationSchema<DbDatabaseItem>(context)
                {
                    Collection = DbDatabaseItem.Schema,
                    Target = target,
                    WorkName = "Get Databases"
                });
            return workItems;
        }
    }
}
