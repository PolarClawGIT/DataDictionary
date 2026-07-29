using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Template Node Interface. Represents an XML Attribute or XML Element.
    /// </summary>
    [Obsolete]
    public interface ITemplateNodeItem :
        ITemplateKey, ITemplateNodeKey,
        ITemporalItem
    {
        /// <summary>
        /// Name of the Node (Attribute or Element)
        /// </summary>
        String? NodeName { get; }

        /// <summary>
        /// Order of the Node to be rendered.
        /// </summary>
        Int32? NodeOrder { get; }

        /// <summary>
        /// How the node is to be Rendered
        /// </summary>
        NodeRenderAsType RenderValueAs { get; }

        /// <summary>
        /// Fixed value for the Node Value.
        /// </summary>
        String? FixedValue { get; }

        /// <summary>
        /// Scope of the object whos Property value is to be rendered.
        /// </summary>
        ScopeType ObjectScope { get; }

        /// <summary>
        /// Property of the Object whos value is to be rendered.
        /// </summary>
        String? ObjectProperty { get; }

        /// <summary>
        /// The ID of the Model Property whos value is to be rendered.
        /// </summary>
        Guid? ModelPropertyId { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Node item.
    /// </summary>
    [Serializable, Obsolete]
    public class TemplateNodeItem : BindingTableRow, ITemplateNodeItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public Guid? NodeId
        {
            get { return GetValue<Guid>(nameof(NodeId)); }
            protected set { SetValue(nameof(NodeId), value); }
        }

        /// <inheritdoc/>
        public String? NodeName
        {
            get { return GetValue(nameof(NodeName)); }
            set { SetValue(nameof(NodeName), value); }
        }

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
        /// Constructor for Template Node Item
        /// </summary>
        protected TemplateNodeItem() : base()
        {
            if (NodeId is null) { NodeId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(NodeName)) { NodeName = "(new Node)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Template Node Item
        /// </summary>
        /// <param name="template"></param>
        public TemplateNodeItem(ITemplateKey template) : this()
        { TemplateId = template.TemplateId; }


        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeName), typeof(String)){ AllowDBNull = false},
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
        protected TemplateNodeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return NodeName ?? String.Empty; }
    }
}
