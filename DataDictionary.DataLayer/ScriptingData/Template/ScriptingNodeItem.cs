using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Scripting Template Node data.
    /// </summary>
    public interface IScriptingNodeItem : IScriptingNodeKeyComposite, IScriptingNodeKeyName, INodeValueAsType, IScopeType
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
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set { SetValue(nameof(PropertyScope), ScopeEnumeration.Cast(value).Name); }
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
        public TemplateNodeValueAsType NodeValueAs
        {
            get
            {
                String? value = GetValue(nameof(NodeValueAs));
                if (TemplateNodeValueAsEnumeration.TryParse(value, null, out TemplateNodeValueAsEnumeration? result))
                { return result.Value; }
                else { return TemplateNodeValueAsType.none; }
            }
            set
            {
                if (value is TemplateNodeValueAsType.none)
                { SetValue(nameof(NodeValueAs), null); }
                else { SetValue(nameof(NodeValueAs), TemplateNodeValueAsEnumeration.Values[value].Name); }
            }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ScriptingTemplateNode;

        /// <summary>
        /// Constructor for Scripting Transform Element
        /// </summary>
        protected ScriptingNodeItem() : base()
        { NodeId = Guid.NewGuid(); }

        /// <summary>
        /// Constructor for Scripting Transform Element
        /// </summary>
        public ScriptingNodeItem(IScriptingTemplateKey template) : this()
        { TemplateId = template.TemplateId; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyScope), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NodeName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(NodeValueAs), typeof(String)){ AllowDBNull = true},
        };

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
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return PropertyName ?? String.Empty; }
    }
}
