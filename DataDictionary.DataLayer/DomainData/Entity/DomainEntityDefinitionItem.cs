using DataDictionary.DataLayer.DomainData.Definition;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Definition Items
    /// </summary>
    public interface IDomainEntityDefinitionItem : IDomainEntityKey, IDomainDefinition, IScopeType
    { }

    /// <summary>
    /// Implementation for Domain Entity Definition Items
    /// </summary>
    [Serializable]
    public class DomainEntityDefinitionItem : BindingTableRow, IDomainEntityDefinitionItem
    {

        /// <inheritdoc/>
        public Guid? EntityId
        { get { return GetValue<Guid>(nameof(EntityId)); } protected set { SetValue(nameof(EntityId), value); } }

        /// <inheritdoc/>
        public Guid? DefinitionId { get { return GetValue<Guid>(nameof(DefinitionId)); } set { SetValue(nameof(DefinitionId), value); } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelEntityDefinition;

        /// <inheritdoc/>
        public String? DefinitionSummary { get { return GetValue(nameof(DefinitionSummary)); } set { SetValue(nameof(DefinitionSummary), value); } }

        /// <inheritdoc/>
        public String? DefinitionText { get { return GetValue(nameof(DefinitionText)); } set { SetValue(nameof(DefinitionText), value); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(EntityId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionSummary), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionText), typeof(String)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for Domain Entity Definition Items
        /// </summary>
        public DomainEntityDefinitionItem() : base() { }

        /// <summary>
        /// Constructor for Domain Entity Definition Items
        /// </summary>
        /// <param name="key"></param>
        public DomainEntityDefinitionItem(IDomainEntityKey key) : this()
        { EntityId = key.EntityId; }


        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Entity Definition Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainEntityDefinitionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
