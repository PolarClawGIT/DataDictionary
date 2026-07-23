using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Scripting Template Attribute item.
    /// </summary>
    [Obsolete]
    public interface ITemplateAttributeItem :
        ITemplateKey, ITemplateAttributeKey, ITemplateNodeItem,
        ITemporalItem
    {
        /// <summary>
        /// Name of the XML Attribute.
        /// </summary>
        String? AttributeName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Attribute item.
    /// </summary>
    [Serializable, Obsolete]
    public class TemplateAttributeItem : BindingTableRow, ITemplateAttributeItem, ISerializable
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
        public String? AttributeName
        {
            get { return GetValue(nameof(AttributeName)); }
            set { SetValue(nameof(AttributeName), value); }
        }

        /// <inheritdoc/>
        Guid? ITemplateNodeKey.NodeId { get { return AttributeId; } }

        /// <inheritdoc/>
        String? ITemplateNodeItem.NodeName { get { return AttributeName; } }

        /// <inheritdoc/>
        public Int32? NodeOrder
        {
            get { return GetValue<Int32>(nameof(NodeOrder)); }
            set { SetValue(nameof(NodeOrder), value); }
        }

        /// <inheritdoc/>
        public NodeRenderAsType RenderValueAs
        {
            get
            {
                String? value = GetValue(nameof(RenderValueAs));
                if (value.TryParse(out NodeRenderAsType result))
                { return result; }
                else { return NodeRenderAsType.None; }
            }
            set
            {
                if (value is NodeRenderAsType.None)
                { SetValue(nameof(RenderValueAs), null); }
                else { SetValue(nameof(RenderValueAs), value.GetEnumeration().Name); }
            }
        }

        /// <inheritdoc/>
        public String? FixedValue
        {
            get { return GetValue(nameof(FixedValue)); }
            set { SetValue(nameof(FixedValue), value); }
        }

        /// <inheritdoc/>
        public ScopeType ObjectScope
        {
            get
            {
                String? value = GetValue(nameof(ObjectScope));
                if (value.TryParse(out ScopeType result))
                { return result; }
                else { return ScopeType.Null; }
            }
            set { SetValue(nameof(ObjectScope), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public String? ObjectProperty
        {
            get { return GetValue(nameof(ObjectProperty)); }
            set { SetValue(nameof(ObjectProperty), value); }
        }

        /// <inheritdoc/>
        public Guid? ModelPropertyId
        {
            get { return GetValue<Guid>(nameof(ModelPropertyId)); }
            set { SetValue(nameof(ModelPropertyId), value); }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }


        /// <summary>
        /// Constructor for Template Attribute Item
        /// </summary>
        protected TemplateAttributeItem() : base()
        {
            if (AttributeId is null) { AttributeId = Guid.NewGuid(); }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Template Attribute Item
        /// </summary>
        /// <param name="template"></param>
        public TemplateAttributeItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }


        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(NodeOrder), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RenderValueAs), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RenderValueAs), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(FixedValue), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectProperty), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ModelPropertyId), typeof(Guid)){ AllowDBNull = true},
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
        protected TemplateAttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return AttributeName ?? String.Empty; }
    }
}
