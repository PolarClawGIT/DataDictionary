using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for Domain Property Item
    /// </summary>
    public interface IPropertyItem : IPropertyKey, IPropertyKeyName, IDomainPropertyType,
        ITemporalItem
    {
        /// <summary>
        /// Description of the Domain Property
        /// </summary>
        String? PropertyDescription { get; }

        /// <summary>
        /// Definition Item is shared (common) across the application.
        /// </summary>
        Boolean? IsCommon { get; }

        /// <summary>
        /// Contains the Data for the property.
        /// </summary>
        /// <remarks>This is PropertyType specific.</remarks>
        String? PropertyData { get; }
    }

    /// <summary>
    /// Implementation for Model Property Item
    /// </summary>
    [Serializable]
    public class PropertyItem : BindingTableRow, IPropertyItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PropertyId
        {
            get { return GetValue<Guid>(nameof(PropertyId)); }
            protected set { SetValue(nameof(PropertyId), value); }
        }

        /// <inheritdoc/>
        public String? PropertyTitle
        {
            get { return GetValue(nameof(PropertyTitle)); }
            set { SetValue(nameof(PropertyTitle), value); }
        }

        /// <inheritdoc/>
        public String? PropertyDescription
        {
            get { return GetValue(nameof(PropertyDescription)); }
            set { SetValue(nameof(PropertyDescription), value); }
        }

        /// <inheritdoc/>
        public Boolean? IsCommon
        { get { return GetValue<bool>(nameof(IsCommon), BindingItemParsers.BooleanTryParse); } }

        /// <inheritdoc/>
        public DomainPropertyType PropertyType
        {
            get
            {
                String? value = GetValue(nameof(DataType));
                if (DomainPropertyEnumeration.TryParse(value, null, out DomainPropertyEnumeration? result))
                { return result.Value; }
                else { return DomainPropertyType.Null; }
            }
            set
            { SetValue(nameof(DataType), DomainPropertyEnumeration.Cast(value).Name); }
        }

        /// <summary>
        /// Internal PropertyType
        /// </summary>
        protected String? DataType
        {
            get { return GetValue(nameof(DataType)); }
            set { SetValue(nameof(DataType), value); }
        }

        /// <inheritdoc/>
        public String? PropertyData
        {
            get { return GetValue(nameof(PropertyData)); }
            set { SetValue(nameof(PropertyData), value); }
        }

        #region ITemporalItem
        TemporalItem temporal; // Backing field for Temporal Data.

        /// <inheritdoc/>
        public DateTime? CreatedOn { get { return temporal.CreatedOn; } }

        /// <inheritdoc/>
        public String? CreatedBy { get { return temporal.CreatedBy; } }

        /// <inheritdoc/>
        public DateTime? RemovedOn { get { return temporal.RemovedOn; } }

        /// <inheritdoc/>
        public String? RemovedBy { get { return temporal.RemovedBy; } }

        /// <inheritdoc/>
        public Boolean? IsInserted { get { return temporal.IsInserted; } }

        /// <inheritdoc/>
        public Boolean? IsUpdated { get { return temporal.IsUpdated; } }

        /// <inheritdoc/>
        public Boolean? IsDeleted { get { return temporal.IsDeleted; } }

        /// <inheritdoc/>
        public Boolean? IsCurrent { get { return temporal.IsCurrent; } }

        /// <inheritdoc/>
        public DbModificationType Modification { get { return temporal.Modification; } }
        #endregion

        /// <summary>
        /// Constructor for Domain Property Item
        /// </summary>
        public PropertyItem() : base()
        {
            if (PropertyId is null) { PropertyId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(PropertyTitle)) { PropertyTitle = "(new Property)"; }

            temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(PropertyId), typeof(Guid)) { AllowDBNull = false },
            new DataColumn(nameof(PropertyTitle), typeof(String)) { AllowDBNull = false },
            new DataColumn(nameof(PropertyDescription), typeof(String)) { AllowDBNull = true },
            new DataColumn(nameof(IsCommon), typeof(Boolean)) { AllowDBNull = true },
            new DataColumn(nameof(DataType), typeof(String)) { AllowDBNull = true },
            new DataColumn(nameof(PropertyData), typeof(String)) { AllowDBNull = true },
            ..TemporalItem.columnDefinitions,
        ];

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Property Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected PropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            temporal = new TemporalItem()
            {
                GetBoolean = (name) => GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse),
                GetDate = GetValue<DateTime>,
                GetString = GetValue,
            };
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (PropertyTitle is not null) { return PropertyTitle; } else { return string.Empty; } }
    }
}
