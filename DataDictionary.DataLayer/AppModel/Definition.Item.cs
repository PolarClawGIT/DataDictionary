using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Model Definition Item
    /// </summary>
    public interface IDefinitionItem : IDefinitionKey, IDefinitionKeyName,
        ITemporalItem
    {
        /// <summary>
        /// Description of the Model Definition
        /// </summary>
        String? DefinitionDescription { get; set; }

        /// <summary>
        /// Definition Item is shared (common) across the application.
        /// </summary>
        Boolean? IsCommon { get; }
    }

    /// <summary>
    /// Implementation for Model Definition Item
    /// </summary>
    [Serializable]
    public class DefinitionItem : BindingTableRow, IDefinitionItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? DefinitionId
        {
            get { return GetValue<Guid>(nameof(DefinitionId)); }
            protected set { SetValue(nameof(DefinitionId), value); }
        }

        /// <inheritdoc/>
        public String? DefinitionTitle
        {
            get { return GetValue(nameof(DefinitionTitle)); }
            set { SetValue(nameof(DefinitionTitle), value); }
        }

        /// <inheritdoc/>
        public String? DefinitionDescription
        {
            get { return GetValue(nameof(DefinitionDescription)); }
            set { SetValue(nameof(DefinitionDescription), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsCommon
        { get { return GetValue<bool>(nameof(IsCommon), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public ITemporal Temporal { get; }

        /// <summary>
        /// Constructor for Domain Definition Item
        /// </summary>
        public DefinitionItem() : base()
        {
            if (DefinitionId is null) { DefinitionId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(DefinitionTitle)) { DefinitionTitle = "(new Definition)"; }

            Temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(DefinitionId), typeof(Guid)) { AllowDBNull = false },
            new DataColumn(nameof(DefinitionTitle), typeof(String)) { AllowDBNull = false },
            new DataColumn(nameof(DefinitionDescription), typeof(String)) { AllowDBNull = true },
            new DataColumn(nameof(IsCommon), typeof(Boolean)) { AllowDBNull = true },
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Definition Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DefinitionItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
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
        { if (DefinitionTitle is not null) { return DefinitionTitle; } else { return string.Empty; } }
    }
}
