using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbConstraintItem : IDbConstraintKey, IDbCatalogKey, IDbTableKey, IBindingTableRow
    {
        String? ConstraintType { get; }
    }

    public class DbConstraintItem : BindingTableRow, IDbConstraintItem, INotifyPropertyChanged, IDbExtendedProperties
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? ConstraintName { get { return GetValue("ConstraintName"); } }
        public String? TableName { get { return GetValue("TableName"); } }
        public String? ConstraintType { get { return GetValue("ConstraintType"); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ConstraintName", typeof(String)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ConstraintType", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbConstraintItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            {
                Level0Name = SchemaName,
                Level0Type = "SCHEMA",
                Level1Name = TableName,
                Level1Type = "TABLE",
                Level2Name = ConstraintName,
                Level2Type = "CONSTRAINT"
            }).GetCommand();
        }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName, String? constraintName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseConstraint]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@ConstraintName", parameters.constraintName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbConstraintItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseConstraint]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseConstraint]", source);
            return command;
        }

        public override String ToString()
        { return new DbConstraintKey(this).ToString(); }
    }
}
