using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Subject Area Item
    /// </summary>
    public interface ISubjectAreaItem : ISubjectArea, ISubjectAreaKey, ISubjectAreaUniqueKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Subject Area Item
    /// </summary>
    [Serializable]
    public class SubjectAreaItem : BindingTableRow, ISubjectAreaItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get { return GetValue<Guid>(nameof(SubjectAreaId)); } protected set { SetValue(nameof(SubjectAreaId), value); } }

        /// <inheritdoc/>
        public String? SubjectAreaTitle { get { return GetValue(nameof(SubjectAreaTitle)); } set { SetValue(nameof(SubjectAreaTitle), value); } }

        /// <inheritdoc/>
        public String? SubjectAreaDescription { get { return GetValue(nameof(SubjectAreaDescription)); } set { SetValue(nameof(SubjectAreaDescription), value); } }

        /// <inheritdoc/>
        public String? SubjectName { get { return GetValue(nameof(SubjectName)); } set { SetValue(nameof(SubjectName), value); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Model Subject Area Item
        /// </summary>
        public SubjectAreaItem() : base()
        {
            if (SubjectAreaId is null) { SubjectAreaId = Guid.NewGuid(); }
            if (string.IsNullOrWhiteSpace(SubjectAreaTitle)) { SubjectAreaTitle = "(new Subject Area)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)) { AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaTitle), typeof(String)) { AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaDescription), typeof(String)) { AllowDBNull = true},
            new DataColumn(nameof(SubjectName), typeof(String)){ AllowDBNull = true},
            .. TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { if (SubjectAreaTitle is not null) { return SubjectAreaTitle; } else { return string.Empty; } }
    }
}
