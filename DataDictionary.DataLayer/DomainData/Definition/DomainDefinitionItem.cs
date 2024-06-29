using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Definition
{
    /// <summary>
    /// Interface for Domain Definition Item
    /// </summary>
    public interface IDomainDefinitionItem : IDomainDefinitionKey, IDomainDefinitionKeyName, IScopeKey
    {
        /// <summary>
        /// Description of the Domain Definition
        /// </summary>
        String? DefinitionDescription { get; set; }
    }

    /// <summary>
    /// Implementation for Domain Definition Item
    /// </summary>
    [Serializable]
    public class DomainDefinitionItem : BindingTableRow, IDomainDefinitionItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? DefinitionId
        {
            get { return GetValue<Guid>(nameof(DefinitionId)); }
            protected set { SetValue(nameof(DefinitionId), value); }
        }

        /// <inheritdoc/>
        public string? DefinitionTitle
        {
            get { return GetValue(nameof(DefinitionTitle)); }
            set { SetValue(nameof(DefinitionTitle), value); }
        }

        /// <inheritdoc/>
        public string? DefinitionDescription
        {
            get { return GetValue(nameof(DefinitionDescription)); }
            set { SetValue(nameof(DefinitionDescription), value); }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelDefinition; } }


        /// <summary>
        /// Constructor for Domain Definition Item
        /// </summary>
        public DomainDefinitionItem() : base()
        {
            if (DefinitionId is null) { DefinitionId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(DefinitionTitle)) { DefinitionTitle = "(new Definition)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(DefinitionId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(DefinitionTitle), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(DefinitionDescription), typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Definition Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainDefinitionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (DefinitionTitle is not null) { return DefinitionTitle; } else { return string.Empty; } }
    }
}
