using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Process Item
    /// </summary>
    public interface IProcessItem : IProcess, IProcessKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Process Item
    /// </summary>
    [Serializable]
    public class ProcessItem : BindingTableRow, IProcessItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? ProcessId
        {
            get { return GetValue<Guid>(nameof(ProcessId)); }
            protected set { SetValue(nameof(ProcessId), value); }
        }

        /// <inheritdoc/>
        public String? ProcessTitle
        {
            get { return GetValue(nameof(ProcessTitle)); }
            set { SetValue(nameof(ProcessTitle), value); }
        }

        /// <inheritdoc/>
        public String? ProcessDescription
        {
            get { return GetValue(nameof(ProcessDescription)); }
            set { SetValue(nameof(ProcessDescription), value); }
        }

        /// <inheritdoc/>
        public string? ProcessName
        {
            get { return GetValue(nameof(ProcessName)); }
            set { SetValue(nameof(ProcessName), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Domain Process Item
        /// </summary>
        public ProcessItem() : base()
        {
            if (ProcessId is null) { ProcessId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(ProcessTitle)) { ProcessTitle = "(new Process)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ProcessId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ProcessTitle), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(ProcessDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ProcessName), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Process Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ProcessItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        public override String ToString()
        { if (ProcessTitle is not null) { return ProcessTitle; } else { return string.Empty; } }
    }
}
