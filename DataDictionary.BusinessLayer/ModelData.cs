using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Represents the main data object used by the application.
    /// </summary>
    public class ModelData
    {
        public IModelItem Model { get { return Models.First(); } }
        public FileInfo? ModelFile { get; private set; }
        Context modelContext = new Context();

        // Database Model
        public BindingTable<DbCatalogItem> DbCatalogs { get; } = ModelFactory.Create<DbCatalogItem>();
        public BindingTable<DbSchemaItem> DbSchemta { get; } = ModelFactory.Create<DbSchemaItem>();
        public BindingTable<DbTableItem> DbTables { get; } = ModelFactory.Create<DbTableItem>();
        public BindingTable<DbColumnItem> DbColumns { get; } = ModelFactory.Create<DbColumnItem>();
        public BindingTable<DbExtendedPropertyItem> DbExtendedProperties = ModelFactory.Create<DbExtendedPropertyItem>();

        // Domain Model
        public BindingTable<DomainAttributeItem> DomainAttributes = ModelFactory.Create<DomainAttributeItem>();
        public BindingTable<DomainAttributeAliasItem> DomainAttributeAliases = ModelFactory.Create<DomainAttributeAliasItem>();
        public BindingTable<DomainAttributePropertyItem> DomainAttributeProperties = ModelFactory.Create<DomainAttributePropertyItem>();

        // Application Data
        public BindingTable<ModelItem> Models { get; } = ModelFactory.Create<ModelItem>();
        public BindingTable<HelpItem> HelpSubjects { get; } = ModelFactory.Create<HelpItem>();

        // Connection Data
        public String ServerName { get { return modelContext.ServerName; } }
        public String DatabaseName { get { return modelContext.DatabaseName; } }

        public ModelData() : base()
        { Models.Add(new ModelItem()); }

        public ModelData(Context context) : this()
        {
            modelContext = new Context()
            {
                ServerName = context.ServerName,
                DatabaseName = context.DatabaseName,
                ApplicationRole = context.ApplicationRole,
                ApplicationRolePassword = context.ApplicationRolePassword
            };
        }

        public IReadOnlyList<WorkItem> LoadDbSchema(DbSchemaContext context)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(context);
            workItems.Add(openConnection);

            workItems.AddRange(RemoveCatalog(context));

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbCatalogs",
                Command = DbCatalogItem.GetSchema,
                Target = DbCatalogs
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbSchemta",
                Command = DbSchemaItem.GetSchema,
                Target = DbSchemta
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbTables",
                Command = DbTableItem.GetSchema,
                Target = DbTables
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load DbColumns",
                Command = DbColumnItem.GetSchema,
                Target = DbColumns
            });

            workItems.Add(new LoadExtendedProperties<DbSchemaItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbSchemta",
                Source = DbSchemta,
                Target = DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbTableItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbTables",
                Source = DbTables,
                Target = DbExtendedProperties
            });

            workItems.Add(new LoadExtendedProperties<DbColumnItem>(openConnection)
            {
                WorkName = "Load DbExtendedProperties, DbColumns",
                Source = DbColumns,
                Target = DbExtendedProperties
            });

            return workItems.AsReadOnly();
        }

        public IReadOnlyList<WorkItem> LoadModel(IModelIdentifier modelId)
        { //TODO: Load from Database by Model ID
            return new List<WorkItem>().AsReadOnly();
        }

        public IReadOnlyList<WorkItem> LoadModel(FileInfo file)
        { //TODO: Load from File System

            using (DataSet data = new DataSet())
            {
                data.ReadXml(file.FullName, XmlReadMode.ReadSchema); // background work

                // Repeat for each data object, background work
                if (data.Tables.Contains(nameof(ModelItem)) && data.Tables[nameof(ModelItem)] is DataTable source)
                {
                    Models.Clear();
                    Models.Load(source.CreateDataReader());
                }
            }
            return new List<WorkItem>().AsReadOnly();
        }

        public IReadOnlyList<WorkItem> LoadHelp()
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(modelContext);
            workItems.Add(openConnection);

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Help",
                DoWork = HelpSubjects.Clear
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load Help",
                Command = HelpItem.GetData,
                Target = HelpSubjects
            });

            return workItems;
        }

        public IReadOnlyList<WorkItem> SaveModel()
        { //TODO: Save to Database
            return new List<WorkItem>().AsReadOnly();
        }

        public IReadOnlyList<WorkItem> SaveModel(FileInfo file)
        { //TODO: Save to File System
            ModelFile = file;

            using (DataSet data = new DataSet())
            {
                // repeat for each data object
                using (DataTable model = new DataTable())
                {
                    model.Load(Models.CreateDataReader()); // Background work
                    data.Tables.Add(model);
                }

                data.WriteXml(file.FullName, XmlWriteMode.WriteSchema); // Background work
            }

            return new List<WorkItem>().AsReadOnly();
        }

        public IReadOnlyList<WorkItem> SaveHelp()
        {
            List<WorkItem> workItems = new List<WorkItem>();
            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(modelContext);
            workItems.Add(openConnection);

            workItems.Add(new SaveBindingTable(openConnection)
            {
                WorkName = "Save Help",
                Command = (conn) => HelpItem.SetData(conn, HelpSubjects)
            });

            workItems.Add(new WorkItem()
            {
                WorkName = "Clear Help",
                DoWork = HelpSubjects.Clear
            });

            workItems.Add(new LoadBindingTable(openConnection)
            {
                WorkName = "Load Help",
                Command = HelpItem.GetData,
                Target = HelpSubjects
            });

            return workItems;
        }

        public IReadOnlyList<WorkItem> RemoveCatalog(DbSchemaContext context)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            workItems.Add(new RemoveCatalog<DbExtendedPropertyItem>()
            {
                WorkName = "Remove DbExtendedProperties by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = DbExtendedProperties
            });

            workItems.Add(new RemoveCatalog<DbColumnItem>()
            {
                WorkName = "Remove DbColumns by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = DbColumns
            });

            workItems.Add(new RemoveCatalog<DbTableItem>()
            {
                WorkName = "Remove DbTables by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = DbTables
            });

            workItems.Add(new RemoveCatalog<DbSchemaItem>()
            {
                WorkName = "Remove DbSchemta by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = DbSchemta
            });

            workItems.Add(new RemoveCatalog<DbCatalogItem>()
            {
                WorkName = "Remove DbCatalogs by Catalog",
                CatalogName = new DbCatalogName() { CatalogName = context.DatabaseName },
                Target = DbCatalogs
            });

            return workItems.AsReadOnly();
        }

        public void ImportDbSchemaToDomain()
        {
            IEnumerable<IDbColumnItem> newAttributes = DbColumns.Where(
                w => w.GetTable(DbTables) is IDbTableItem table && !table.IsSystem && // Do not want System Tables
                w.GetSchema(DbSchemta) is IDbSchemaItem schema && !schema.IsSystem && // Do not want System Schemta
                DomainAttributeAliases.FirstOrDefault( // Do not want Columns already aliased to an attribute
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

                DomainAttributes.Add(newAttribute);

                foreach (IDbColumnItem aliasSource in columnItem)
                {
                    DomainAttributeAliases.Add(new DomainAttributeAliasItem()
                    {
                        AttributeId = newAttribute.AttributeId,
                        CatalogName = aliasSource.CatalogName,
                        SchemaName = aliasSource.SchemaName,
                        ObjectName = aliasSource.TableName,
                        ElementName = aliasSource.ColumnName
                    });

                    propeties.AddRange(aliasSource.GetProperties(DbExtendedProperties));
                }

                foreach (IGrouping<String?, IDbExtendedPropertyItem> propertyItem in propeties.GroupBy(g => g.PropertyName))
                {
                    IDbExtendedPropertyItem propertySource = propertyItem.First();

                    DomainAttributeProperties.Add(
                        new DomainAttributePropertyItem()
                        {
                            AttributeId = newAttribute.AttributeId,
                            PropertyName = propertySource.PropertyName,
                            PropertyValue = propertySource.PropertyValue
                        });

                    if (propertySource.PropertyName is not null &&
                        DomainAttributeProperties.
                        Count(w => w.PropertyName == propertySource.PropertyName) == 0)
                    {
                        DomainAttributeProperties.Add(
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
