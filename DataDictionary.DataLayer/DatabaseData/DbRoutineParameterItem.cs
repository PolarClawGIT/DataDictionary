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
    public interface IDbRoutineParameterItem : IDbRoutineParameterKey, IDbDomainReferenceKey, IDbElementScope, IDbColumn, IDbCatalogKey
    {
    }

    [Serializable]
    public class DbRoutineParameterItem : BindingTableRow, IDbRoutineParameterItem, INotifyPropertyChanged, IDbExtendedProperties, ISerializable
    {
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } }
        public String? RoutineName { get { return GetValue("RoutineName"); } }
        public String? ParameterName { get { return GetValue("ParameterName"); } }
        public Nullable<Int32> OrdinalPosition { get { return GetValue<Int32>("OrdinalPosition"); } }
        public String? DataType { get { return GetValue("DataType"); } }
        public Nullable<Int32> CharacterMaximumLength { get { return GetValue<Int32>("CharacterMaximumLength"); } }
        public Nullable<Int32> CharacterOctetLength { get { return GetValue<Int32>("CharacterOctetLength"); } }
        public Nullable<Byte> NumericPrecision { get { return GetValue<Byte>("NumericPrecision"); } }
        public Nullable<Int16> NumericPrecisionRadix { get { return GetValue<Int16>("NumericPrecisionRadix"); } }
        public Nullable<Int32> NumericScale { get { return GetValue<Int32>("NumericScale"); } }
        public Nullable<Int16> DateTimePrecision { get { return GetValue<Int16>("DateTimePrecision"); } }
        public String? CharacterSetCatalog { get { return GetValue("CharacterSetCatalog"); } }
        public String? CharacterSetSchema { get { return GetValue("CharacterSetSchema"); } }
        public String? CharacterSetName { get { return GetValue("CharacterSetName"); } }
        public String? CollationCatalog { get { return GetValue("CollationCatalog"); } }
        public String? CollationSchema { get { return GetValue("CollationSchema"); } }
        public String? CollationName { get { return GetValue("CollationName"); } }
        public String? DomainCatalog { get { return GetValue("DomainCatalog"); } }
        public String? DomainSchema { get { return GetValue("DomainSchema"); } }
        public String? DomainName { get { return GetValue("DomainName"); } }
        public DbElementScope ElementScope { get; } = DbElementScope.Parameter;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(String)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = false},
            new DataColumn("RoutineName", typeof(String)){ AllowDBNull = false},
            new DataColumn("RoutineType", typeof(String)){ AllowDBNull = false},
            new DataColumn("ParameterName", typeof(String)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("DataType", typeof(String)){ AllowDBNull = true},
            new DataColumn("CharacterMaximumLength", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("CharacterOctetLength", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("NumericPrecision", typeof(Byte)){ AllowDBNull = true},
            new DataColumn("NumericPrecisionRadix", typeof(Int16)){ AllowDBNull = true},
            new DataColumn("NumericScale", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("DateTimePrecision", typeof(Int16)){ AllowDBNull = true},
            new DataColumn("CharacterSetCatalog", typeof(String)){ AllowDBNull = true},
            new DataColumn("CharacterSetSchema", typeof(String)){ AllowDBNull = true},
            new DataColumn("CharacterSetName", typeof(String)){ AllowDBNull = true},
            new DataColumn("CollationCatalog", typeof(String)){ AllowDBNull = true},
            new DataColumn("CollationSchema", typeof(String)){ AllowDBNull = true},
            new DataColumn("CollationName", typeof(String)){ AllowDBNull = true},
            new DataColumn("DomainCatalog", typeof(String)){ AllowDBNull = true},
            new DataColumn("DomainSchema", typeof(String)){ AllowDBNull = true},
            new DataColumn("DomainName", typeof(String)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public DbRoutineParameterItem() : base() { }

        public static Command GetSchema(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = DbScript.DbRoutineParameterItem;
            return command;
        }

        public virtual Command GetProperties(IConnection connection)
        {
            String? level1Type = GetValue("RoutineType");

            return (new DbExtendedPropertyGetCommand(connection)
            { Level0Name = SchemaName, Level0Type = "SCHEMA", Level1Name = RoutineName, Level1Type = level1Type, Level2Name = ParameterName, Level2Type = "PARAMETER" }).
            GetCommand();
        }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, String? catalogName, String? schemaName, String? routineName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseRoutineParameter]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@RoutineName", parameters.routineName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DbRoutineParameterItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseRoutineParameter]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseRoutineParameter]", source);
            return command;
        }

        #region ISerializable
        protected DbRoutineParameterItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        public override String ToString()
        { return new DbRoutineParameterKey(this).ToString(); }
    }
}
