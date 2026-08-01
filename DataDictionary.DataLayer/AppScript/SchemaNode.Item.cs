using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Base SchemaNode (common to sub-types)
    /// </summary>
    public interface ISchemaNodeObject: ISchemaNodeObjectName
    {
        /// <summary>
        /// Name of the Node as Rendered
        /// </summary>
        String? NodeName { get; }

        /// <summary>
        /// The XmlNodeType of Node to be Rendered.
        /// </summary>
        /// <see cref="XmlNodeType"/>
        XmlNodeType RenderNodeType { get; }

        /// <summary>
        /// The XmlTypeCode of the data type of the Node to be Rendered.
        /// </summary>
        /// <see cref="XmlTypeCode"/>
        XmlTypeCode RenderTypeCode { get; }

        /// <summary>
        /// Order that the Node appears in.
        /// </summary>
        Int32? RenderOrder { get; }
    }

    /// <summary>
    /// Interface for the Scripting Fixed Value SchemaNode (sub-type)
    /// </summary>
    [Obsolete("Not being supported/needed")]
    public interface ISchemaNodeFixedValue : ISchemaNodeObject
    {
        /// <summary>
        /// Render the value as a fixed value instead of a Object drive value.
        /// </summary>
        String? FixedValue { get; }
    }


    /// <summary>
    /// Interface for the Scripting SchemaNode (super-type)
    /// </summary>
    public interface ISchemaNodeItem : ISchemaNodeKey, ISchemaNodeKeyName, ITemplateKey,
        ISchemaNodeObject//, ISchemaNodeObjectValue
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
        public virtual String? NodeName
        {
            get { return GetValue(nameof(NodeName)); }
            set { SetValue(nameof(NodeName), value); }
        }

        /// <inheritdoc/>
        public virtual Int32? RenderOrder
        {
            get { return GetValue<Int32>(nameof(RenderOrder)); }
            set { SetValue(nameof(RenderOrder), value); }
        }

        /// <inheritdoc/>
        public XmlNodeType RenderNodeType
        {
            get
            {
                String? value = GetValue(nameof(RenderNodeType));
                if (value.TryParse(out XmlNodeType result))
                { return result; }
                else { return XmlNodeType.None; }
            }
            set
            {
                if (value is XmlNodeType.None)
                { SetValue(nameof(RenderNodeType), null); }
                else { SetValue(nameof(RenderNodeType), value.GetName()); }
            }
        }

        /// <inheritdoc/>
        public XmlTypeCode RenderTypeCode 
        {
            get
            {
                String? value = GetValue(nameof(RenderTypeCode));
                if (value.TryParse(out XmlTypeCode result))
                { return result; }
                else { return XmlTypeCode.None; }
            }
            set
            {
                if (value is XmlTypeCode.None)
                { SetValue(nameof(RenderTypeCode), null); }
                else { SetValue(nameof(RenderTypeCode), value.GetName()); }
            }
        }

        /// <inheritdoc/>
        public virtual ScopeType ObjectScope
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
        public virtual String? ObjectProperty
        {
            get { return GetValue(nameof(ObjectProperty)); }
            set { SetValue(nameof(ObjectProperty), value); }
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
            RenderOrder = 0;

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
            new DataColumn(nameof(ObjectScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ObjectProperty), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NodeName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RenderNodeType), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RenderTypeCode), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RenderOrder), typeof(Int32)){ AllowDBNull = true},
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
