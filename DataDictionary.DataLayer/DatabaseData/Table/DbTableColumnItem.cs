// Ignore Spelling: Nullable

using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Column
    /// </summary>
    public interface IDbTableColumnItem : IDbTableColumnKeyName, IDbTableColumnKey, IDbCatalogKey, IDbDomainReferenceKey, IDbColumn, IScopeKeyName, IDataItem
    {
        /// <summary>
        /// Is the Column Nullable
        /// </summary>
        Boolean? IsNullable { get; }

        /// <summary>
        /// Column Default value
        /// </summary>
        String? ColumnDefault { get; }

        /// <summary>
        /// Is the Column an Identity
        /// </summary>
        Boolean? IsIdentity { get; }

        /// <summary>
        /// Is the Column Hidden
        /// </summary>
        Boolean? IsHidden { get; }

        /// <summary>
        /// Is the Column Computed
        /// </summary>
        Boolean? IsComputed { get; }

        /// <summary>
        /// If the Column is Computed, the Computed Definition
        /// </summary>
        String? ComputedDefinition { get; }

        /// <summary>
        /// Type of Always Generated Column. Used by System Version.
        /// </summary>
        String? GeneratedAlwayType { get; }
    }

    /// <summary>
    /// Implementation of the Database Table Column
    /// </summary>
    [Serializable]
    public class DbTableColumnItem : BindingTableRow, IDbTableColumnItem, INotifyPropertyChanged, IDbExtendedProperty, ISerializable
    {
        /// <inheritdoc/>
        public Guid? CatalogId { get { return GetValue<Guid>("CatalogId"); } }

        /// <inheritdoc/>
        public Guid? ColumnId { get { return GetValue<Guid>("ColumnId"); } }
        
        /// <inheritdoc/>
        public string? DatabaseName { get { return GetValue("DatabaseName"); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } }

        /// <inheritdoc/>
        public string? TableName { get { return GetValue("TableName"); } }

        /// <inheritdoc/>
        public string? ColumnName { get { return GetValue("ColumnName"); } }

        /// <inheritdoc/>
        public string? ScopeName { get { return GetValue("ScopeName"); } }

        /// <inheritdoc/>
        public int? OrdinalPosition { get { return GetValue<int>("OrdinalPosition"); } }

        /// <inheritdoc/>
        public bool? IsNullable { get { return GetValue<bool>("IsNullable", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public string? DataType { get { return GetValue("DataType"); } }

        /// <inheritdoc/>
        public string? ColumnDefault { get { return GetValue("ColumnDefault"); } }

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
        public bool? IsIdentity { get { return GetValue<bool>("IsIdentity", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsHidden { get { return GetValue<bool>("IsHidden", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public bool? IsComputed { get { return GetValue<bool>("IsComputed", BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public string? ComputedDefinition { get { return GetValue("ComputedDefinition"); } }

        /// <inheritdoc/>
        public string? GeneratedAlwayType { get { return GetValue("GeneratedAlwayType"); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(string)){ AllowDBNull = true},
            new DataColumn("ColumnId", typeof(string)){ AllowDBNull = true},
            new DataColumn("DatabaseName", typeof(string)){ AllowDBNull = false},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = false},
            new DataColumn("TableName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ColumnName", typeof(string)){ AllowDBNull = false},
            new DataColumn("ScopeName", typeof(string)){ AllowDBNull = false},
            new DataColumn("OrdinalPosition", typeof(int)){ AllowDBNull = false},
            new DataColumn("IsNullable", typeof(bool)){ AllowDBNull = true},
            new DataColumn("DataType", typeof(string)){ AllowDBNull = true},
            new DataColumn("ColumnDefault", typeof(string)){ AllowDBNull = true},
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
            new DataColumn("IsIdentity", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsHidden", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsComputed", typeof(bool)){ AllowDBNull = true},
            new DataColumn("ComputedDefinition", typeof(string)){ AllowDBNull = true},
            new DataColumn("GeneratedAlwayType", typeof(string)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for the Database Table Column
        /// </summary>
        public DbTableColumnItem() : base() { }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public virtual Command PropertyCommand(IConnection connection)
        {
            if (new ScopeKey(this).TryScope() is IDbElementScopeKey scopeKey)
            {
                return new DbExtendedPropertyGetCommand(connection)
                {
                    CatalogId = CatalogId,
                    Level0Name = SchemaName,
                    Level0Type = scopeKey.CatalogScope.ToString(),
                    Level1Name = TableName,
                    Level1Type = scopeKey.ObjectScope.ToString(),
                    Level2Name = ColumnName,
                    Level2Type = scopeKey.ElementScope.ToString(),
                }.GetCommand();
            }
            else
            {
                Exception ex = new InvalidOperationException("Could not determine LevelType");
                ex.Data.Add(nameof(ScopeName), ScopeName);
                ex.Data.Add(nameof(DatabaseName), DatabaseName);
                ex.Data.Add(nameof(SchemaName), SchemaName);
                ex.Data.Add(nameof(TableName), TableName);
                ex.Data.Add(nameof(ColumnName), ColumnName);
                throw ex;
            }
        }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Database Table Column
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DbTableColumnItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new DbTableColumnKeyName(this).ToString(); }
    }
}
