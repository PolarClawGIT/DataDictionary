using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template Attribute item.
    /// </summary>
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
    [Serializable]
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
        public Int32? RenderOrder
        {
            get { return GetValue<Int32>(nameof(RenderOrder)); }
            set { SetValue(nameof(RenderOrder), value); }
        }

        /// <inheritdoc/>
        public TemplateNodeValueAsType RenderValueAs
        {
            get
            {
                String? value = GetValue(nameof(RenderValueAs));
                if (TemplateNodeValueAsEnumeration.TryParse(value, null, out TemplateNodeValueAsEnumeration? result))
                { return result.Value; }
                else { return TemplateNodeValueAsType.none; }
            }
            set
            {
                if (value is TemplateNodeValueAsType.none)
                { SetValue(nameof(RenderValueAs), null); }
                else { SetValue(nameof(RenderValueAs), TemplateNodeValueAsEnumeration.Cast(value).Name); }
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
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null)
                { SetValue(nameof(ObjectScope), null); }
                else { SetValue(nameof(ObjectScope), ScopeEnumeration.Cast(value).Name); }
            }
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
            new DataColumn(nameof(RenderOrder), typeof(String)){ AllowDBNull = true},
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
