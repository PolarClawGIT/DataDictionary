using DataDictionary.Resource.Enumerations;
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
        String? ObjectPath { get; }

        /// <summary>
        /// Application Scope of the Alias.
        /// </summary>
        ScopeType ObjectScope { get; }
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
        public ScopeType ObjectScope
        {
            get
            {
                String value = GetValue(nameof(ObjectScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(ObjectScope), null); }
                else { SetValue(nameof(ObjectScope), ScopeEnumeration.Cast(value).Name); }
            }
        }

        /// <inheritdoc/>
        public virtual String? ObjectPath
        {
            get { return GetValue(nameof(ObjectPath)); }
            set { SetValue(nameof(ObjectPath), value); }
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
            new DataColumn(nameof(ObjectPath), typeof(String)){ AllowDBNull = true},
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
            if (ObjectPath is String) { return ObjectPath; }
            else { return String.Empty; }
        }
    }
}
