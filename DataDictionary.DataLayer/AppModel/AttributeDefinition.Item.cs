using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Attribute Definition Items
    /// </summary>
    public interface IAttributeDefinitionItem : IAttributeKey, IDefinition,
        ITemporalItem
    { }

    /// <summary>
    /// Implementation for Model Attribute Definition Items
    /// </summary>
    [Serializable]
    public class AttributeDefinitionItem : BindingTableRow, IAttributeDefinitionItem
    {
        /// <inheritdoc/>
        public Guid? AttributeId
        { get { return GetValue<Guid>(nameof(AttributeId)); } protected set { SetValue(nameof(AttributeId), value); } }

        /// <inheritdoc/>
        public Guid? DefinitionId { get { return GetValue<Guid>(nameof(DefinitionId)); } set { SetValue(nameof(DefinitionId), value); } }

        /// <inheritdoc/>
        public String? DefinitionSummary { get { return GetValue(nameof(DefinitionSummary)); } set { SetValue(nameof(DefinitionSummary), value); } }

        /// <inheritdoc/>
        public String? DefinitionText { get { return GetValue(nameof(DefinitionText)); } set { SetValue(nameof(DefinitionText), value); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionSummary), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(DefinitionText), typeof(String)){ AllowDBNull = true},
            ..TemporalItem.columnDefinitions,
        ];

        /// <summary>
        /// Constructor for Domain Attribute Definition Items
        /// </summary>
        public AttributeDefinitionItem() : base()
        {
            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        /// <summary>
        /// Constructor for Domain Attribute Definition Items
        /// </summary>
        /// <param name="key"></param>
        public AttributeDefinitionItem(IAttributeKey key) : this()
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
        protected AttributeDefinitionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        public override String ToString()
        {
            if (DefinitionSummary is String) { return DefinitionSummary; }
            else { return String.Empty; }
        }

    }
}