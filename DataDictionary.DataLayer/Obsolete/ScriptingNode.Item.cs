using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Scripting Template Node data.
    /// </summary>
    [Obsolete("replace", true)]
    public interface IScriptingNodeItem : 
        IScriptingNodeKeyComposite, IScriptingNodeKeyName, INodeRenderAs,
        ITemporalItem
    {
        /// <summary>
        /// Name to apply to the Node (default is PropertyName)
        /// </summary>
        String? NodeName { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Node data.
    /// </summary>
    [Serializable]
    [Obsolete("replace", true)]
    public class ScriptingNodeItem : BindingTableRow, IScriptingNodeItem, ISerializable
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
        public ScopeType PropertyScope
        {
            get
            {
                String value = GetValue(nameof(PropertyScope)) ?? String.Empty;
                if (value.TryParse(out ScopeType result))
                { return result; }
                else { return ScopeType.Null; }
            }
            set { SetValue(nameof(PropertyScope), value.GetEnumeration().Name); }
        }

        /// <inheritdoc/>
        public String? PropertyName
        {
            get { return GetValue(nameof(PropertyName)); }
            set { SetValue(nameof(PropertyName), value); }
        }

        /// <inheritdoc/>
        public String? NodeName
        {
            get { return GetValue(nameof(NodeName)); }
            set { SetValue(nameof(NodeName), value); }
        }

        /// <inheritdoc/>
        public NodeRenderAsType NodeRenderAs
        {
            get
            {
                String? value = GetValue(nameof(NodeRenderAs));
                if (value.TryParse(out NodeRenderAsType result))
                { return result; }
                else { return NodeRenderAsType.None; }
            }
            set
            {
                if (value is NodeRenderAsType.None)
                { SetValue(nameof(NodeRenderAs), null); }
                else { SetValue(nameof(NodeRenderAs), value.GetEnumeration().Name); }
            }
        }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Scripting Transform Element
        /// </summary>
        protected ScriptingNodeItem() : base()
        { NodeId = Guid.NewGuid();

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Scripting Transform Element
        /// </summary>
        public ScriptingNodeItem(IScriptingTemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NodeName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NodeRenderAs), typeof(String)){ AllowDBNull = true},
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
        protected ScriptingNodeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { return PropertyName ?? String.Empty; }
    }
}
