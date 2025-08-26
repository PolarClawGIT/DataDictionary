using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Data Object data.
    /// </summary>
    public interface IDataObjectItem : IDataSourceKey,
        ITemporalItem
    {
        /// <summary>
        /// The NameSpace Path of the Data Object.
        /// </summary>
        String? DataPath { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Data Object data.
    /// </summary>
    [Serializable]
    public class DataObjectItem : BindingTableRow, IDataObjectItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? DataSourceId
        {
            get { return GetValue<Guid>(nameof(DataSourceId)); }
            protected set { SetValue(nameof(DataSourceId), value); }
        }

        /// <inheritdoc/>
        public virtual String? DataPath
        {
            get { return GetValue(nameof(DataPath)); }
            set { SetValue(nameof(DataPath), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Data Object
        /// </summary>
        public DataObjectItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting Data Object
        /// </summary>
        /// <param name="key"></param>
        public DataObjectItem(IDataSourceKey key) : this()
        { DataSourceId = key.DataSourceId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DataSourceId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(DataPath), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DataObjectItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        {
            if (DataPath is String) { return DataPath; }
            else { return String.Empty; }
        }
    }
}
