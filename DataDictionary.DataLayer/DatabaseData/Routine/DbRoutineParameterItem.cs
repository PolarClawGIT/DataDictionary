using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Parameter
    /// </summary>
    public interface IDbRoutineParameterItem : IDbRoutineParameterKeyName, IDbRoutineParameterKey, IDbDomainReferenceKey, IDbColumn, IDbCatalogKey, IScopeKeyName, IDataItem
    { }

    /// <summary>
    /// Implementation for the Database Routine Parameter
    /// </summary>
    [Serializable]
    public class DbRoutineParameterItem : BindingTableRow, IDbRoutineParameterItem, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public Guid? ParameterId { get { return GetValue<Guid>("ParameterId"); } }
        
        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("DatabaseName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? RoutineName { get { return GetValue("RoutineName"); } }

        /// <inheritdoc/>
        public string? ParameterName { get { return GetValue("ParameterName"); } }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } }

        /// <inheritdoc/>
        public int? OrdinalPosition { get { return GetValue<int>("OrdinalPosition"); } }

        /// <inheritdoc/>
        public string? DataType { get { return GetValue("DataType"); } }

        /// <inheritdoc/>
        public int? CharacterMaximumLength { get { return GetValue<int>("CharacterMaximumLength"); } }

        /// <inheritdoc/>
        public int? CharacterOctetLength { get { return GetValue<int>("CharacterOctetLength"); } }

        /// <inheritdoc/>
        public byte? NumericPrecision { get { return GetValue<byte>("NumericPrecision"); } }

        /// <inheritdoc/>
        public short? NumericPrecisionRadix { get { return GetValue<short>("NumericPrecisionRadix"); } }

        /// <inheritdoc/>
        public int? NumericScale { get { return GetValue<int>("NumericScale"); } }

        /// <inheritdoc/>
        public short? DateTimePrecision { get { return GetValue<short>("DateTimePrecision"); } }

        /// <inheritdoc/>
        public string? CharacterSetCatalog { get { return GetValue("CharacterSetCatalog"); } }

        /// <inheritdoc/>
        public string? CharacterSetSchema { get { return GetValue("CharacterSetSchema"); } }

        /// <inheritdoc/>
        public string? CharacterSetName { get { return GetValue("CharacterSetName"); } }

        /// <inheritdoc/>
        public string? CollationCatalog { get { return GetValue("CollationCatalog"); } }

        /// <inheritdoc/>
        public string? CollationSchema { get { return GetValue("CollationSchema"); } }

        /// <inheritdoc/>
        public string? CollationName { get { return GetValue("CollationName"); } }

        /// <inheritdoc/>
        public string? DomainCatalog { get { return GetValue("DomainCatalog"); } }

        /// <inheritdoc/>
        public string? DomainSchema { get { return GetValue("DomainSchema"); } }

        /// <inheritdoc/>
        public string? DomainName { get { return GetValue("DomainName"); } }

        /// <inheritdoc/>
        //public DbElementScope ElementScope { get; } = DbElementScope.Parameter;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("ParameterId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DatabaseName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("RoutineName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ParameterName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ScopeName", typeof(string)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(int)){ AllowDBNull = true},
            new DataColumn("DataType", typeof(string)){ AllowDBNull = true},
            new DataColumn("CharacterMaximumLength", typeof(int)){ AllowDBNull = true},
            new DataColumn("CharacterOctetLength", typeof(int)){ AllowDBNull = true},
            new DataColumn("NumericPrecision", typeof(byte)){ AllowDBNull = true},
            new DataColumn("NumericPrecisionRadix", typeof(short)){ AllowDBNull = true},
            new DataColumn("NumericScale", typeof(int)){ AllowDBNull = true},
            new DataColumn("DateTimePrecision", typeof(short)){ AllowDBNull = true},
            new DataColumn("CharacterSetCatalog", typeof(string)){ AllowDBNull = true},
            new DataColumn("CharacterSetSchema", typeof(string)){ AllowDBNull = true},
            new DataColumn("CharacterSetName", typeof(string)){ AllowDBNull = true},
            new DataColumn("CollationCatalog", typeof(string)){ AllowDBNull = true},
            new DataColumn("CollationSchema", typeof(string)){ AllowDBNull = true},
            new DataColumn("CollationName", typeof(string)){ AllowDBNull = true},
            new DataColumn("DomainCatalog", typeof(string)){ AllowDBNull = true},
            new DataColumn("DomainSchema", typeof(string)){ AllowDBNull = true},
            new DataColumn("DomainName", typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <summary>
        /// Constructor for the Database Routine Parameter
        /// </summary>
        public DbRoutineParameterItem() : base() { }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            {
                if (new ScopeKey(this).TryScope() is IDbElementScopeKey scopeKey)
                {
                    return new DbExtendedPropertyGetCommand(connection)
                    {
                        CatalogId = CatalogId,
                        Level0Name = SchemaName,
                        Level0Type = scopeKey.CatalogScope.ToString(),
                        Level1Name = RoutineName,
                        Level1Type = scopeKey.ObjectScope.ToString(),
                        Level2Name = ParameterName,
                        Level2Type = scopeKey.ElementScope.ToString(),
                    }.GetCommand();
                }
                else
                {
                    Exception ex = new InvalidOperationException("Could not determine LevelType");
                    ex.Data.Add(nameof(ScopeName), ScopeName);
                    ex.Data.Add(nameof(DatabaseName), DatabaseName);
                    ex.Data.Add(nameof(SchemaName), SchemaName);
                    ex.Data.Add(nameof(RoutineName), RoutineName);
                    ex.Data.Add(nameof(ParameterName), ParameterName);
                    throw ex;
                }
            }
        }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Routine Parameter
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbRoutineParameterItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbRoutineParameterKeyName(this).ToString(); }
    }
}
