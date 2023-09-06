using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbConstraintColumnItem: IDbConstraintKey, IDbCatalogKey, IDbTableColumnKey, IBindingTableRow
    {
        String? ReferenceSchemaName { get; }
        String? ReferenceTableName { get; }
        String? ReferenceColumnName { get; }
        Int32? OrdinalPosition { get; } 
    }

    [Serializable]
    public class DbConstraintColumnItem : BindingTableRow, IDbConstraintColumnItem, INotifyPropertyChanged, ISerializable
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? ConstraintName { get { return GetValue("ConstraintName"); } }
        public String? TableName { get { return GetValue("TableName"); } }
        public String? ColumnName { get { return GetValue("ColumnName"); } }
        public Int32? OrdinalPosition { get { return GetValue<Int32>("OrdinalPosition"); } }
        public String? ReferenceSchemaName { get { return GetValue("ReferenceSchemaName"); } }
        public String? ReferenceTableName { get { return GetValue("ReferenceTableName"); } }
        public String? ReferenceColumnName { get { return GetValue("ReferenceColumnName"); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ConstraintName", typeof(String)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ColumnName", typeof(String)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("ReferenceSchemaName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ReferenceTableName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ReferenceColumnName", typeof(String)){ AllowDBNull = true},
        };

        public DbConstraintColumnItem() : base() { }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbConstraintColumnItem;
            return command;
        }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName, String? constraintName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseConstraintColumn]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@ConstraintName", parameters.constraintName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbConstraintColumnItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseConstraintColumn]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseConstraintColumn]", source);
            return command;
        }

        #region ISerializable
        protected DbConstraintColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

    }

}
