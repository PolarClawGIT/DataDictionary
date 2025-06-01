using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Process Subject Area Item
    /// </summary>
    public interface IProcessSubjectAreaItem : IProcessKey, ISubjectAreaKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Process Subject Area Item
    /// </summary>
    public class ProcessSubjectAreaItem : BindingTableRow, IProcessSubjectAreaItem, ISerializable
    {

        /// <inheritdoc/>
        public Guid? ProcessId
        {
            get { return GetValue<Guid>(nameof(ProcessId)); }
            protected set { SetValue(nameof(ProcessId), value); }
        }

        /// <inheritdoc/>
        public Guid? SubjectAreaId
        {
            get { return GetValue<Guid>(nameof(SubjectAreaId)); }
            protected set { SetValue(nameof(SubjectAreaId), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(ProcessId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)){ AllowDBNull = false},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Process Subject Area Items
        /// </summary>
        public ProcessSubjectAreaItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Process Subject Area Items
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="subject"></param>
        public ProcessSubjectAreaItem(IProcessKey Process, ISubjectAreaKey subject) : this()
        {
            ProcessId = Process.ProcessId;
            SubjectAreaId = subject.SubjectAreaId;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Process Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected ProcessSubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
