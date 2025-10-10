using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template Node Owner item.
    /// </summary>
    public interface ITemplateNodeOwnerItem :
        ITemplateKey, ITemplateNodeKey, ITemplateNodeOwnerKey,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for the Scripting Template Node Owner item.
    /// </summary>
    public class TemplateNodeOwnerItem : BindingTableRow, ITemplateNodeOwnerItem, ISerializable
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
        public Guid? NodeOwnerId
        {
            get { return GetValue<Guid>(nameof(NodeOwnerId)); }
            set { SetValue(nameof(NodeOwnerId), value); }
        }

        /// <summary>
        /// The Path of the Node Owner, as determined by Db.
        /// Alternate way of setting the NodeOwnerId.
        /// </summary>
        protected String? NodeOwnerPath
        {
            get { return GetValue(nameof(NodeOwnerPath)); }
            set { SetValue(nameof(NodeOwnerPath), value); }
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
        /// <param name="node"></param>
        public TemplateNodeOwnerItem(ITemplateNodeItem node) : this()
        {
            TemplateId = node.TemplateId;
            NodeId = node.NodeId;
        }

        /// <summary>
        /// Constructor for Template Node Owner
        /// </summary>
        /// <param name="node"></param>
        /// <param name="owner"></param>
        public TemplateNodeOwnerItem(ITemplateNodeItem node, ITemplateNodeItem owner) : this(node)
        {   NodeOwnerId = owner.NodeId; }


        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeOwnerId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(NodeOwnerPath), typeof(Guid)){ AllowDBNull = true},
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
