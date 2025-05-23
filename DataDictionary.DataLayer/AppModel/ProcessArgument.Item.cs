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
        /// The Position/Order of the Argument
        /// </summary>
        Int32? OrdinalPosition { get; }
        /*
        [IsPassed] Bit Not Null,
	[IsReturned] Bit Not Null,
	[IsContributor] Bit Not Null,
	[IsAltered] Bit Not Null,
	[AsValue] Bit Not Null,
	[AsReference] Bit Not Null,
        */

        /// <summary>
        /// Is the Argument passed, usually as a parameter
        /// </summary>
        Boolean? IsPassed { get; }

        /// <summary>
        /// Is the Argument is a returned value
        /// </summary>
        Boolean? IsReturned { get; }

        /// <summary>
        /// Is the Argument is a contributer
        /// </summary>
        Boolean? IsContributor { get; }

        /// <summary>
        /// Is the Argument is altered
        /// </summary>
        Boolean? IsAltered { get; }

        /// <summary>
        /// Is the Argument an Input value
        /// </summary>
        Boolean? IsInput { get; }

        /// <summary>
        /// Is the Argument an Output value
        /// </summary>
        Boolean? IsOutput { get; }

        /// <summary>
        /// Is the Argument is handled as a Value (read only).
        /// </summary>
        Boolean? AsValue { get; }

        /// <summary>
        /// Is the Argument is handled as a Reference (possibly read/write)
        /// </summary>
        Boolean? AsReference { get; }
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
        public Int32? OrdinalPosition
        {
            get { return GetValue<Int32>(nameof(OrdinalPosition)); }
            set { SetValue(nameof(OrdinalPosition), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsPassed
        {
            get { return GetValue<Boolean>(nameof(IsPassed), BindingItemParsers.BooleanTryParse); }
            set
            {
                SetValue(nameof(IsPassed), value);
                OnPropertyChanged(nameof(IsInput));
            }
        }

        /// <inheritdoc/>
        public Boolean? IsReturned
        {
            get { return GetValue<Boolean>(nameof(IsReturned), BindingItemParsers.BooleanTryParse); }
            set
            {
                SetValue(nameof(IsReturned), value);
                OnPropertyChanged(nameof(IsOutput));
            }
        }

        /// <inheritdoc/>
        public Boolean? IsContributor
        {
            get { return GetValue<Boolean>(nameof(IsContributor), BindingItemParsers.BooleanTryParse); }
            set
            {
                SetValue(nameof(IsContributor), value);
                OnPropertyChanged(nameof(IsInput));
            }
        }

        /// <inheritdoc/>
        public Boolean? IsAltered
        {
            get { return GetValue<Boolean>(nameof(IsAltered), BindingItemParsers.BooleanTryParse); }
            set
            {
                SetValue(nameof(IsAltered), value);
                OnPropertyChanged(nameof(IsInput));
                OnPropertyChanged(nameof(IsOutput));
            }
        }

        /// <inheritdoc/>
        public Boolean? IsInput
        { get { return IsPassed == true || IsContributor == true || IsAltered == true; } }

        /// <inheritdoc/>
        public Boolean? IsOutput
        { get { return IsReturned == true || IsAltered == true; } }


        /// <inheritdoc/>
        public Boolean? AsValue
        {
            get { return GetValue<Boolean>(nameof(AsValue), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(AsValue), value); }
        }

        /// <inheritdoc/>
        public Boolean? AsReference
        {
            get { return GetValue<Boolean>(nameof(AsReference), BindingItemParsers.BooleanTryParse); }
            set { SetValue(nameof(AsReference), value); }
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
            new DataColumn(nameof(OrdinalPosition), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(IsPassed), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsReturned), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsContributor), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsAltered), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AsValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AsReference), typeof(Boolean)){ AllowDBNull = true},
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
