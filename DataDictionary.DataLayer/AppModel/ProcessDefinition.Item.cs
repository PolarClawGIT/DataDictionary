using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Process Definition Items
    /// </summary>
    public interface IProcessDefinitionItem : IProcessKey, IDefinition,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Process Definition Items
    /// </summary>
    [Serializable]
    public class ProcessDefinitionItem : BindingTableRow, IProcessDefinitionItem
    {

        /// <inheritdoc/>
        public Guid? ProcessId
        { get { return GetValue<Guid>(nameof(ProcessId)); } protected set { SetValue(nameof(ProcessId), value); } }

        /// <inheritdoc/>
        public Guid? DefinitionId { get { return GetValue<Guid>(nameof(DefinitionId)); } set { SetValue(nameof(DefinitionId), value); } }

        /// <inheritdoc/>
        public String? DefinitionSummary { get { return GetValue(nameof(DefinitionSummary)); } set { SetValue(nameof(DefinitionSummary), value); } }

        /// <inheritdoc/>
        public String? DefinitionText { get { return GetValue(nameof(DefinitionText)); } set { SetValue(nameof(DefinitionText), value); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ProcessId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionSummary), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionText), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Process Definition Items
        /// </summary>
        public ProcessDefinitionItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Process Definition Items
        /// </summary>
        /// <param name="key"></param>
        public ProcessDefinitionItem(IProcessKey key) : this()
        { ProcessId = key.ProcessId; }


        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Process Definition Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ProcessDefinitionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        {
            if (DefinitionSummary is String) { return DefinitionSummary; }
            else { return String.Empty; }
        }

    }
}
