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
    public interface IDbRoutineParameterItem : IDbRoutineParameterKeyName, IDbRoutineParameterKey, IDbDomainReferenceKey, IDbColumn, IDbCatalogKey, IDbRoutineType, IScopeKey
    { }

    /// <summary>
    /// Implementation for the Database Routine Parameter
    /// </summary>
    [Serializable]
    public class DbRoutineParameterItem : BindingTableRow, IDbRoutineParameterItem, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>(nameof(CatalogId)); } }

        /// <inheritdoc/>
        public Guid? ParameterId { get { return GetValue<Guid>(nameof(ParameterId)); } }
        
        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue(nameof(DatabaseName)); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue(nameof(SchemaName)); } }

        /// <inheritdoc/>
        public string? RoutineName { get { return GetValue(nameof(RoutineName)); } }

        /// <inheritdoc/>
        public string? ParameterName { get { return GetValue(nameof(ParameterName)); } }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue(nameof(ScopeName)); } }

        /// <inheritdoc/>
        public int? OrdinalPosition { get { return GetValue<int>(nameof(OrdinalPosition)); } }

        /// <inheritdoc/>
        public string? DataType { get { return GetValue(nameof(DataType)); } }

        /// <inheritdoc/>
        public int? CharacterMaximumLength { get { return GetValue<int>(nameof(CharacterMaximumLength)); } }

        /// <inheritdoc/>
        public int? CharacterOctetLength { get { return GetValue<int>(nameof(CharacterOctetLength)); } }

        /// <inheritdoc/>
        public byte? NumericPrecision { get { return GetValue<byte>(nameof(NumericPrecision)); } }

        /// <inheritdoc/>
        public short? NumericPrecisionRadix { get { return GetValue<short>(nameof(NumericPrecisionRadix)); } }

        /// <inheritdoc/>
        public int? NumericScale { get { return GetValue<int>(nameof(NumericScale)); } }

        /// <inheritdoc/>
        public short? DateTimePrecision { get { return GetValue<short>(nameof(DateTimePrecision)); } }

        /// <inheritdoc/>
        public string? CharacterSetCatalog { get { return GetValue(nameof(CharacterSetCatalog)); } }

        /// <inheritdoc/>
        public string? CharacterSetSchema { get { return GetValue(nameof(CharacterSetSchema)); } }

        /// <inheritdoc/>
        public string? CharacterSetName { get { return GetValue(nameof(CharacterSetName)); } }

        /// <inheritdoc/>
        public string? CollationCatalog { get { return GetValue(nameof(CollationCatalog)); } }

        /// <inheritdoc/>
        public string? CollationSchema { get { return GetValue(nameof(CollationSchema)); } }

        /// <inheritdoc/>
        public string? CollationName { get { return GetValue(nameof(CollationName)); } }

        /// <inheritdoc/>
        public string? DomainCatalog { get { return GetValue(nameof(DomainCatalog)); } }

        /// <inheritdoc/>
        public string? DomainSchema { get { return GetValue(nameof(DomainSchema)); } }

        /// <inheritdoc/>
        public string? DomainName { get { return GetValue(nameof(DomainName)); } }

        /// <inheritdoc/>
        public DbRoutineType RoutineType
        { get { return DbRoutineTypeKey.Parse(GetValue(nameof(RoutineType)) ?? String.Empty).RoutineType; } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (RoutineType)
                {
                    case DbRoutineType.Null: return ScopeType.Null;
                    case DbRoutineType.Function: return ScopeType.DatabaseFunctionParameter;
                    case DbRoutineType.Procedure: return ScopeType.DatabaseProcedureParameter;
                    default: return ScopeType.Null;
                }
            }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(ParameterId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(RoutineType), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(ParameterName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(OrdinalPosition), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(DataType), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterMaximumLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterOctetLength), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecision), typeof(byte)){ AllowDBNull = true},
            new DataColumn(nameof(NumericPrecisionRadix), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(NumericScale), typeof(int)){ AllowDBNull = true},
            new DataColumn(nameof(DateTimePrecision), typeof(short)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CharacterSetName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(CollationName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainCatalog), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainSchema), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DomainName), typeof(string)){ AllowDBNull = true},
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
                if (this.Scope.ToDbLevel() is IDbLevelElementKey scopeKey)
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
