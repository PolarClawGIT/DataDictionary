using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Attribute Subject Area Item
    /// </summary>
    public interface IAttributeSubjectAreaItem : IAttributeKey, ISubjectAreaKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Attribute Subject Area Item
    /// </summary>
    public class AttributeSubjectAreaItem : BindingTableRow, IAttributeSubjectAreaItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId
        {
            get { return GetValue<Guid>(nameof(AttributeId)); }
            protected set { SetValue(nameof(AttributeId), value); }
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
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SubjectAreaId), typeof(Guid)){ AllowDBNull = false},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Attribute Subject Area Items
        /// </summary>
        public AttributeSubjectAreaItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Attribute Subject Area Items
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="subject"></param>
        public AttributeSubjectAreaItem(IAttributeKey attribute, ISubjectAreaKey subject) : this()
        {
            AttributeId = attribute.AttributeId;
            SubjectAreaId = subject.SubjectAreaId;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AttributeSubjectAreaItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
