using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Property
{
    /// <summary>
    /// Interface for Domain Property Item
    /// </summary>
    public interface IDomainPropertyItem : IDomainPropertyKey, IDomainPropertyKeyName, IDomainPropertyType, IScopeKey
    {
        /// <summary>
        /// Description of the Domain Property
        /// </summary>
        String? PropertyDescription { get; }

        /// <summary>
        /// Contains the Data for the property.
        /// </summary>
        /// <remarks>This is PropertyType specific.</remarks>
        String? PropertyData { get; }
    }

    /// <summary>
    /// Implementation for Domain Property Item
    /// </summary>
    [Serializable]
    public class DomainPropertyItem : BindingTableRow, IDomainPropertyItem, ISerializable
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
        public DomainPropertyType PropertyType
        {
            get
            {
                String? value = GetValue(nameof(PropertyType));
                if (DomainPropertyEnumeration.TryParse(value, null, out DomainPropertyEnumeration? result))
                { return result.Value; }
                else { return DomainPropertyType.Null; }
            }
            set
            { SetValue(nameof(PropertyType), value.Data().Name); }
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

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelProperty; } }


        /// <summary>
        /// Constructor for Domain Property Item
        /// </summary>
        public DomainPropertyItem() : base()
        {
            if (PropertyId is null) { PropertyId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(PropertyTitle)) { PropertyTitle = "(new Property)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyTitle), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyDescription), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(DataType), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyData), typeof(string)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Property Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainPropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (PropertyTitle is not null) { return PropertyTitle; } else { return string.Empty; } }
    }
}
