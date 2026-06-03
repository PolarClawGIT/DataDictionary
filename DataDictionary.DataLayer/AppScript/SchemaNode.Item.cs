using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Base SchemaNode (common to sub-types)
    /// </summary>
    public interface ISchemaNodeBaseValue
    {
        /// <summary>
        /// Title of the Scripting Schema (aka Name of the Schema)
        /// </summary>
        String? NodeName { get; }

        /// <summary>
        /// Order that the Node appears in.
        /// </summary>
        Int32? NodeOrder { get; }

        /// <summary>
        /// How the Value of the Node is to be rendered.
        /// </summary>
        NodeRenderAsType RenderValueAs { get; }
    }

    /// <summary>
    /// Interface for the Scripting Fixed Value SchemaNode (sub-type)
    /// </summary>
    public interface ISchemaNodeFixedValue : ISchemaNodeBaseValue
    {
        /// <summary>
        /// Render the value as a fixed value instead of a Object drive value.
        /// </summary>
        String? FixedValue { get; }
    }

    /// <summary>
    /// Interface for the Scripting Object Scope Value SchemaNode (sub-type)
    /// </summary>
    /// <remarks>Use ISchemaNodeObjectValue or ISchemaNodePropertyValue</remarks>
    public interface ISchemaNodeObjectScopeValue : ISchemaNodeBaseValue
    {
        /// <summary>
        /// Object Scope of the item to be rendered. (not fixed value)
        /// </summary>
        ScopeType ObjectScope { get; }
    }

    /// <summary>
    /// Interface for the Scripting Object Value SchemaNode (sub-type)
    /// </summary>
    public interface ISchemaNodeObjectValue : ISchemaNodeObjectScopeValue
    {
        /// <summary>
        /// The Property within the Object to render. (not fixed value)
        /// </summary>
        String? ObjectProperty { get; }
    }

    /// <summary>
    /// Interface for the Scripting Property Value SchemaNode (sub-type)
    /// </summary>
    public interface ISchemaNodePropertyValue : ISchemaNodeObjectScopeValue
    {
        /// <summary>
        /// The PropertyID of the Model Property for the object to be rendered.
        /// </summary>
        Guid? ModelPropertyId { get; }
    }

    /// <summary>
    /// Interface for the Scripting SchemaNode (super-type)
    /// </summary>
    public interface ISchemaNodeItem : ISchemaNodeKey, ISchemaDefinitionKey, ITemplateKey,
        ISchemaNodeBaseValue, ISchemaNodeFixedValue, ISchemaNodeObjectValue, ISchemaNodePropertyValue
    { }

    /// <summary>
    /// Implementation for the Scripting SchemaNode.
    /// </summary>
    [Serializable]
    public class SchemaNodeItem : BindingTableRow, ISchemaNodeItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            protected set { SetValue(nameof(TemplateId), value); }
        }

        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            protected set { SetValue(nameof(SchemaId), value); }
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
                else { return NodeRenderAsType.none; }
            }
            set
            {
                if (value is NodeRenderAsType.none)
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
            set
            { SetValue(nameof(ObjectScope), value.GetEnumeration().Name); }
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
        /// Constructor for Scripting Schema Definition
        /// </summary>
        /// <remarks>This is an incomplete initialization for use in derived classes that require the new() constraint.</remarks>
        protected SchemaNodeItem() : base()
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
        /// Constructor for Scripting Schema Definition
        /// </summary>
        public SchemaNodeItem(ITemplateKey template, ISchemaDefinitionKey schema) : this()
        {
            TemplateId = template.TemplateId;
            SchemaId = schema.SchemaId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(NodeName), typeof(String)){ AllowDBNull = false},
            new DataColumn(nameof(NodeOrder), typeof(Int32)){ AllowDBNull = true},
            new DataColumn(nameof(RenderValueAs), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(FixedValue), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectProperty), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ModelPropertyId), typeof(Guid)){ AllowDBNull =true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Database Column 
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SchemaNodeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
