using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for Database Column Item
    /// </summary>
    public interface ITableItem : ITable, ITableKey, ICatalogKey,
        IDbIsSystem, IDbTableType, ITemporalItem
    {
        /// <inheritdoc cref="IDbTableType.TableType"/>
        new DbTableType TableType { get; }
    }

    /// <summary>
    /// Implementation of Database Column Item
    /// </summary>
    [Serializable]
    public class TableItem : BindingTableRow, ITableItem, INotifyPropertyChanged, ISerializable,
        IInfomationSchemaItem<ITable, TableItem>
    {
        /// <inheritdoc/>
        public Guid? CatalogId
        {
            get { return GetValue<Guid>(nameof(CatalogId)); }
            init { SetValue<Guid>(nameof(CatalogId), value); }
        }

        /// <inheritdoc/>
        public Guid? TableId
        {
            get { return GetValue<Guid>(nameof(TableId)); }
            private init { SetValue<Guid>(nameof(TableId), value); }
        }

        /// <inheritdoc/>
        public String? DatabaseName
        {
            get { return GetValue(nameof(DatabaseName)); }
            init { SetValue(nameof(DatabaseName), value); }
        }

        /// <inheritdoc/>
        public String? SchemaName
        {
            get { return GetValue(nameof(SchemaName)); }
            init { SetValue(nameof(SchemaName), value); }
        }

        /// <inheritdoc/>
        public String? TableName
        {
            get { return GetValue(nameof(TableName)); }
            init { SetValue(nameof(TableName), value); }
        }

        /// <inheritdoc/>
        public Boolean IsSystem
        {
            get
            {
                var list = Table.IsSystem.Split(',').Select(s =>
                    {
                        if (s.EndsWith(".*") && TableName is String)
                        { s.Substring(0, s.Length - 1).Concat(TableName); }

                        return s.Trim();
                    });

                return list.Any(w => w.Trim().Equals(
                        String.Format("{0}.{1}", SchemaName, TableName),
                        KeyExtension.CompareString));
            }
        }

        /// <inheritdoc/>
        public DbTableType TableType
        {
            get
            {
                String? value = GetValue(nameof(TableType));
                if (DbTableEnumeration.TryParse(value, null, out DbTableEnumeration? result))
                { return result.Value; }
                else { return DbTableType.Null; }
            }
            init
            { SetValue(nameof(TableType), DbTableEnumeration.Cast(value).Name); }
        }

        /// <inheritdoc/>
        String? ITableType.TableType { get { return GetValue(nameof(ITable.TableType)); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CatalogId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(TableId), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DatabaseName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableName), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(TableType), typeof(string)){ AllowDBNull = false},
            .. TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Database Column 
        /// </summary>
        public TableItem() : base()
        {
            TableId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <inheritdoc/>
        public static TResult Create<TResult>(ICatalogKey catalog, ITable source)
            where TResult : TableItem, new()
        {
            DbTableType tableType = DbTableType.Null;
            if (DbTableEnumeration.TryParse(source.TableType, null, out DbTableEnumeration? result))
            { tableType = result.Value; }

            return new TResult()
            {
                CatalogId = catalog.CatalogId,
                DatabaseName = source.DatabaseName,
                SchemaName = source.SchemaName,
                TableName = source.TableName,
                TableType = tableType,
            };
        }

        /// <inheritdoc/>
        public virtual void Update(ITable source)
        {
            // Nothing to actually do. All values are fixed.
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TableItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new TableKeyName(this).ToString(); }


    }
}
