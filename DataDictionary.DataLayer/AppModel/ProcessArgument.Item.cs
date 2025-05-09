// Ignore Spelling: Nullable

using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model ProcessArgument Item
    /// </summary>
    public interface IProcessArgumentItem : IProcessKey,
        ITemporalItem
    {
        /// <summary>
        /// Title of the Process Argument, such as the Parameter Name.
        /// </summary>
        String? ArgumentTitle { get; }

        /// <summary>
        /// Description of the Process Argument
        /// </summary>
        String? ArgumentDescription { get; }

        /// <summary>
        /// Process Argument Name within the Subject Area.
        /// </summary>
        String? ArgumentName { get; }

        /// <summary>
        /// An Entity, Attribute, or system Name
        /// </summary>
        String? ArgumentType { get; }

        /// <summary>
        /// The Position/Order of the Argument
        /// </summary>
        Int32? OrdinalPosition { get; }

        /// <summary>
        /// Is the Argument an Input value
        /// </summary>
        Boolean? IsInput { get; }

        /// <summary>
        /// Is the Argument an Output value
        /// </summary>
        Boolean? IsOutput { get; }


    }

    /// <summary>
    /// Implementation for Model ProcessArgument Item
    /// </summary>
    public class ProcessArgumentItem : BindingTableRow, IProcessArgumentItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? ProcessId
        {
            get { return GetValue<Guid>(nameof(ProcessId)); }
            protected set { SetValue(nameof(ProcessId), value); }
        }

        /// <inheritdoc/>
        public String? ArgumentTitle
        {
            get { return GetValue(nameof(ArgumentTitle)); }
            set { SetValue(nameof(ArgumentTitle), value); }
        }

        /// <inheritdoc/>
        public String? ArgumentDescription
        {
            get { return GetValue(nameof(ArgumentDescription)); }
            set { SetValue(nameof(ArgumentDescription), value); }
        }

        /// <inheritdoc/>
        public String? ArgumentName
        {
            get { return GetValue(nameof(ArgumentName)); }
            set { SetValue(nameof(ArgumentName), value); }
        }

        /// <inheritdoc/>
        public String? ArgumentType
        {
            get { return GetValue(nameof(ArgumentType)); }
            set { SetValue(nameof(ArgumentType), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsInput
        {
            get { return GetValue<Boolean>(nameof(IsInput), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsInput), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsOutput
        {
            get { return GetValue<Boolean>(nameof(IsOutput), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(IsOutput), value); }
        }

        /// <inheritdoc/>
        public Int32? OrdinalPosition
        {
            get { return GetValue<Int32>(nameof(OrdinalPosition)); }
            set { SetValue(nameof(OrdinalPosition), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for ProcessArgument Item
        /// </summary>
        public ProcessArgumentItem() : base()
        {
            ArgumentTitle = "{new Argument}";

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for ProcessArgument Item
        /// </summary>
        /// <param name="Process"></param>
        public ProcessArgumentItem(IProcessKey Process) : this()
        { ProcessId = Process.ProcessId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ProcessId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ArgumentTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ArgumentDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ArgumentName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ArgumentType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(IsInput), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsOutput), typeof(Boolean)){ AllowDBNull = true},
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
        protected ProcessArgumentItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
