using DataDictionary.DataLayer.ApplicationData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbRoutineDependencyItem : IDbRoutineDependencyKey, IDbCatalogKey
    {
        //Guid? CatalogId { get; }
        //String? SchemaName { get; }
        //String? RoutineName { get; }
        //String? ReferenceSchemaName { get; }
        //String? ReferenceObjectName { get; }
        String? ReferenceObjectType { get; }
        //String? ReferenceColumnName { get; }
        Nullable<Boolean> IsCallerDependent { get; }
        Nullable<Boolean> IsAmbiguous { get; }
        Nullable<Boolean> IsSelected { get; }
        Nullable<Boolean> IsUpdated { get; }
        Nullable<Boolean> IsSelectAll { get; }
        Nullable<Boolean> IsAllColumnsFound { get; }
        Nullable<Boolean> IsInsertAll { get; }
        Nullable<Boolean> IsIncomplete { get; }

    }

    public class DbRoutineDependencyItem : BindingTableRow, IDbRoutineDependencyItem, INotifyPropertyChanged
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? RoutineName { get { return GetValue("RoutineName"); } }
        public String? ReferenceSchemaName { get { return GetValue("ReferenceSchemaName"); } }
        public String? ReferenceObjectName { get { return GetValue("ReferenceObjectName"); } }
        public String? ReferenceObjectType { get { return GetValue("ReferenceObjectType"); } }
        public String? ReferenceColumnName { get { return GetValue("ReferenceColumnName"); } }
        public Nullable<Boolean> IsCallerDependent { get { return GetValue<Boolean>("IsCallerDependent", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsAmbiguous { get { return GetValue<Boolean>("IsAmbiguous", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsSelected { get { return GetValue<Boolean>("IsSelected", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsUpdated { get { return GetValue<Boolean>("IsUpdated", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsSelectAll { get { return GetValue<Boolean>("IsSelectAll", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsAllColumnsFound { get { return GetValue<Boolean>("IsAllColumnsFound", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsInsertAll { get { return GetValue<Boolean>("IsInsertAll", BindingItemParsers.BooleanTryParse); } }
        public Nullable<Boolean> IsIncomplete { get { return GetValue<Boolean>("IsIncomplete", BindingItemParsers.BooleanTryParse); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("RoutineName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ReferenceSchemaName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ReferenceObjectName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ReferenceObjectType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ReferenceColumnName", typeof(String)){ AllowDBNull = true},
            new DataColumn("IsCallerDependent", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsAmbiguous", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsSelected", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsUpdated", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsSelectAll", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsAllColumnsFound", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsInsertAll", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsIncomplete", typeof(Boolean)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection, IDbRoutineKey key)
        {
            Command result = connection.CreateCommand();
            result.CommandType = CommandType.Text;
            result.CommandText = DbScript.DbRoutineDependencyItem;
            result.Parameters.Add(new SqlParameter("@ObjectName", SqlDbType.VarChar, 210));
            result.Parameters["@ObjectName"].Value = String.Format("[{0}].[{1}]", key.SchemaName, key.RoutineName);
            return result;
        }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName, String? routineName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseRoutineDependency]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@RoutineName", parameters.routineName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbRoutineDependencyItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseRoutineDependency]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseRoutineDependency]", source);
            return command;
        }
    }


}
