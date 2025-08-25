using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Data Source data.
    /// </summary>
    public interface IDataSourceItem : IDataSourceKey, IDataSource,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Scripting Data Source data.
    /// </summary>
    [Serializable]
    public class DataSourceItem : BindingTableRow, IDataSourceItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? DataSourceId
        {
            get { return GetValue<Guid>(nameof(DataSourceId)); }
            protected set { SetValue(nameof(DataSourceId), value); }
        }

        /// <inheritdoc/>
        public String? DataSourceTitle
        {
            get { return GetValue(nameof(DataSourceTitle)); }
            set { SetValue(nameof(DataSourceTitle), value); }
        }

        /// <inheritdoc/>
        public String? DataSourceDescription
        {
            get { return GetValue(nameof(DataSourceDescription)); }
            set { SetValue(nameof(DataSourceDescription), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Data Source
        /// </summary>
        public DataSourceItem () : base()
        {
            if (DataSourceId is null) { DataSourceId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(DataSourceTitle)) { DataSourceTitle = "(new DataSource)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DataSourceId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(DataSourceTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(DataSourceDescription), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DataSourceItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return DataSourceTitle ?? String.Empty; }
    }
}
