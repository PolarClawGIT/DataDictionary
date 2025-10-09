using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template Node Owner item.
    /// </summary>
    [Obsolete]
    public interface ITemplateNodeOwnerItem :
        ITemplateKey, ITemplateAttributeKey, ITemplateElementKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Scripting Template Node Owner item.
    /// </summary>
    [Obsolete]
    public class TemplateNodeOwnerItem : BindingTableRow, ITemplateNodeOwnerItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public Guid? AttributeId
        {
            get { return GetValue<Guid>(nameof(AttributeId)); }
            protected set { SetValue(nameof(AttributeId), value); }
        }

        /// <inheritdoc/>
        public Guid? ElementId
        {
            get { return GetValue<Guid>(nameof(ElementId)); }
            protected set { SetValue(nameof(ElementId), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Template Node Owner
        /// </summary>
        protected TemplateNodeOwnerItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Template Node Owner
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="parent"></param>
        public TemplateNodeOwnerItem(ITemplateAttributeItem attribute, ITemplateElementKey parent) : this()
        {
            TemplateId = attribute.TemplateId;
            AttributeId = attribute.AttributeId;
            ElementId = parent.ElementId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(ElementId), typeof(Guid)){ AllowDBNull = false},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected TemplateNodeOwnerItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
