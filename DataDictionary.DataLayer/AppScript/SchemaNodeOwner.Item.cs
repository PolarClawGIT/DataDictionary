using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting SchemaNodeOwner
    /// </summary>
    public interface ISchemaNodeOwnerItem : ISchemaNodeOwnerKey, ISchemaDefinitionKey, ITemplateKey
    {

    }

    /// <summary>
    /// Implementation for the Scripting SchemaNodeOwner.
    /// </summary>
    [Serializable]
    public class SchemaNodeOwnerItem : BindingTableRow, ISchemaNodeOwnerItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? NodeOwnerId
        {
            get { return GetValue<Guid>(nameof(NodeId)); }
            set { SetValue(nameof(NodeId), value); }
        }

        /// <inheritdoc/>
        public Guid? NodeId
        {
            get { return GetValue<Guid>(nameof(NodeId)); }
            set { SetValue(nameof(NodeId), value); }
        }

        /// <inheritdoc/>
        public Guid? SchemaId
        {
            get { return GetValue<Guid>(nameof(SchemaId)); }
            set { SetValue(nameof(SchemaId), value); }
        }

        /// <inheritdoc/>
        public Guid? TemplateId
        {
            get { return GetValue<Guid>(nameof(TemplateId)); }
            set { SetValue(nameof(TemplateId), value); }
        }
        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting SchemaNodeOwner
        /// </summary>
        protected SchemaNodeOwnerItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting SchemaNodeOwner
        /// </summary>
        public SchemaNodeOwnerItem(ISchemaNodeItem node, ISchemaNodeKey parentNode) : this()
        {
            NodeOwnerId = parentNode.NodeId;
            NodeId = node.NodeId;
            SchemaId = node.SchemaId;
            TemplateId = node.TemplateId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeOwnerId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(SchemaId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},

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
        protected SchemaNodeOwnerItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
