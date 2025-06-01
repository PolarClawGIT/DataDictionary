using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Process Properties
    /// </summary>
    public interface IProcessPropertyItem : IProcessPropertyKey, IProperty,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Process Properties
    /// </summary>
    [Serializable]
    public class ProcessPropertyItem : BindingTableRow, IProcessPropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? ProcessId { get { return GetValue<Guid>(nameof(ProcessId)); } protected set { SetValue(nameof(ProcessId), value); } }

        /// <inheritdoc/>
        public Guid? PropertyId { get { return GetValue<Guid>(nameof(PropertyId)); } set { SetValue(nameof(PropertyId), value); } }

        /// <inheritdoc/>
        public String? PropertyValue { get { return GetValue(nameof(PropertyValue)); } set { SetValue(nameof(PropertyValue), value); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Model Process Properties
        /// </summary>
        public ProcessPropertyItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Process Properties
        /// </summary>
        /// <param name="ProcessKey"></param>
        public ProcessPropertyItem(IProcessKey ProcessKey) : this()
        { ProcessId = ProcessKey.ProcessId; }

        /// <summary>
        /// Constructor for Domain Process Properties
        /// </summary>
        /// <param name="ProcessKey"></param>
        /// <param name="propertyKey"></param>
        public ProcessPropertyItem(IProcessKey ProcessKey, IPropertyKey propertyKey) : this()
        {
            ProcessId = ProcessKey.ProcessId;
            PropertyId = propertyKey.PropertyId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ProcessId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyValue), typeof(string)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Process Properties
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ProcessPropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion
    }
}
