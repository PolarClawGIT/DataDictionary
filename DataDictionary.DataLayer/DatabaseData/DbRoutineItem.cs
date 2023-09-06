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
    public interface IDbRoutineItem : IDbRoutineKey, IDbCatalogKey, IDbObjectScope, IDbIsSystem, IBindingTableRow
    {
        String? RoutineType { get; }
    }

    [Serializable]
    public class DbRoutineItem : BindingTableRow, IDbRoutineItem, INotifyPropertyChanged, IDbExtendedProperties, ISerializable
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? RoutineName { get { return GetValue("RoutineName"); } }
        public String? RoutineType { get { return GetValue("RoutineType"); } }
        public Boolean IsSystem
        {
            get
            {
                return SchemaName is "dbo" &&
                    RoutineName is "sp_creatediagram" or
                    "sp_renamediagram" or
                    "sp_alterdiagram" or
                    "sp_dropdiagram" or
                    "fn_diagramobjects" or
                    "sp_helpdiagrams" or
                    "sp_helpdiagramdefinition" or
                    "sp_upgraddiagrams";
            }
        }
        public DbObjectScope ObjectScope
        {
            get
            {
                if (Enum.TryParse(RoutineType, true, out DbObjectScope value))
                { return value; }
                else { return DbObjectScope.NULL; }
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("RoutineName", typeof(String)){ AllowDBNull = false},
            new DataColumn("RoutineType", typeof(String)){ AllowDBNull = false},
        };

        public DbRoutineItem() : base() { }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbRoutineItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            return (new DbExtendedPropertyGetCommand(connection)
            {
                Level0Name = SchemaName,
                Level0Type = "SCHEMA",
                Level1Name = RoutineName,
                Level1Type = RoutineType
            }).GetCommand();
        }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName, String? routineName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseRoutine]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@RoutineName", parameters.routineName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbRoutineItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseRoutine]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseRoutine]", source);
            return command;
        }

        #region ISerializable
        protected DbRoutineItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        public override String ToString()
        { return new DbRoutineKey(this).ToString(); }
    }
}
