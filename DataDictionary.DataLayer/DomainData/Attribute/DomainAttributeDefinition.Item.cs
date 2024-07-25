using DataDictionary.DataLayer.DomainData.Definition;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Definition Items
    /// </summary>
    public interface IDomainAttributeDefinitionItem : IDomainAttributeKey, IDomainDefinition, IScopeType
    { }

    /// <summary>
    /// Implementation for Domain Attribute Definition Items
    /// </summary>
    [Serializable]
    public class DomainAttributeDefinitionItem : BindingTableRow, IDomainAttributeDefinitionItem
    {

        /// <inheritdoc/>
        public Guid? AttributeId
        { get { return GetValue<Guid>(nameof(AttributeId)); } protected set { SetValue(nameof(AttributeId), value); } }

        /// <inheritdoc/>
        public Guid? DefinitionId { get { return GetValue<Guid>(nameof(DefinitionId)); } set { SetValue(nameof(DefinitionId), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelAttributeDefinition;

        /// <inheritdoc/>
        public String? DefinitionSummary { get { return GetValue(nameof(DefinitionSummary)); } set { SetValue(nameof(DefinitionSummary), value); } }

        /// <inheritdoc/>
        public String? DefinitionText { get { return GetValue(nameof(DefinitionText)); } set { SetValue(nameof(DefinitionText), value); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionSummary), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionText), typeof(String)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for Domain Attribute Definition Items
        /// </summary>
        public DomainAttributeDefinitionItem() : base() { }

        /// <summary>
        /// Constructor for Domain Attribute Definition Items
        /// </summary>
        /// <param name="key"></param>
        public DomainAttributeDefinitionItem(IDomainAttributeKey key) : this()
        { AttributeId = key.AttributeId; }


        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Definition Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainAttributeDefinitionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (DefinitionSummary is String) { return DefinitionSummary; }
            else { return String.Empty; }
        }

    }
}