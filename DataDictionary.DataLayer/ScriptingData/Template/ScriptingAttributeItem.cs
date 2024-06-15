using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Toolbox.BindingTable;
using System.Data;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Scripting Template Node Attribute data.
    /// </summary>
    public interface IScriptingAttributeItem : IScriptingAttributeKey, IScriptingNodeKey, IScriptingTemplateKey, IDomainPropertyKey, IScopeKey
    {
        /// <summary>
        /// Name to appear for the XML Attribute
        /// </summary>
        public String? AttributeName { get; }

        /// <summary>
        /// Value to appear for the XML Attribute
        /// </summary>
        public String? AttributeValue { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Template Node Attribute data.
    /// </summary>
    [Serializable]
    public class ScriptingAttributeItem : BindingTableRow, IScriptingAttributeItem, ISerializable
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
        public String? AttributeValue
        {
            get { return GetValue(nameof(AttributeValue)); }
            set { SetValue(nameof(AttributeValue), value); }
        }

        /// <inheritdoc/>
        public Guid? PropertyId
        {
            get { return GetValue<Guid>(nameof(PropertyId)); }
            set { SetValue(nameof(PropertyId), value); }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ScriptingTemplateAttribute;

        /// <summary>
        /// Constructor for Scripting Template Node Attribute
        /// </summary>
        protected ScriptingAttributeItem() : base()
        { AttributeId = Guid.NewGuid(); }

        /// <summary>
        /// Constructor for Scripting Template Node Attribute
        /// </summary>
        public ScriptingAttributeItem(IScriptingNodeKeyComposite node) : this()
        {
            TemplateId = node.TemplateId;
            NodeId = node.NodeId;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(TemplateId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(NodeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeId), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AttributeName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AttributeValue), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = true},
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
        protected ScriptingAttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return AttributeName ?? String.Empty; }
    }
}
