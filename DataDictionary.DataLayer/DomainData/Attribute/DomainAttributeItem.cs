using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Item
    /// </summary>
    public interface IDomainAttributeItem : IDomainAttributeKey, IDomainAttributeUniqueKey, IDataItem
    {
        /// <summary>
        /// Description of the Domain Attribute
        /// </summary>
        String? AttributeDescription { get; set; }

        /// <summary>
        /// Type of Attribute in terms of a parent Attribute.
        /// </summary>
        /// <remarks>
        /// Not required to be in the same Model.
        /// </remarks>
        Guid? TypeOfAttributeId { get; set; }

        /// <summary>
        /// Title of the Type of Attribute
        /// </summary>
        String? TypeOfAttributeTitle { get; set; }

        /// <summary>
        /// Is Attribute Single Valued (false = MultiValued, null = unknown)
        /// </summary>
        Boolean? IsSingleValue { get; set; }

        /// <summary>
        /// Is Attribute a Simple Type (false = composite, null = unknown)
        /// </summary>
        Boolean? IsSimple { get; set; }

        /// <summary>
        /// Is Attribute a Derived Value (Computed value, null = unknown)
        /// </summary>
        Boolean? IsDerived { get; set; }

        /// <summary>
        /// Is Attribute a Null-able value (Allows Null value, exceptions allowed)
        /// </summary>
        Boolean? IsNullable { get; set; }

        /// <summary>
        /// Is Attribute is used as a Key (exceptions allowed)
        /// </summary>
        Boolean? IsKey { get; set; }
    }

    /// <summary>
    /// Implementation for Domain Attribute Item
    /// </summary>
    [Serializable]
    public class DomainAttributeItem : BindingTableRow, IDomainAttributeItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? AttributeId
        {
            get { return GetValue<Guid>("AttributeId"); }
            protected set { SetValue("AttributeId", value); }
        }

        /// <inheritdoc/>
        public string? AttributeTitle
        {
            get { return GetValue("AttributeTitle"); }
            set { SetValue("AttributeTitle", value); }
        }

        /// <inheritdoc/>
        public string? AttributeDescription
        {
            get { return GetValue("AttributeDescription"); }
            set { SetValue("AttributeDescription", value); }
        }

        /// <inheritdoc/>
        public Guid? TypeOfAttributeId
        {
            get { return GetValue<Guid>("TypeOfAttributeId"); }
            set { SetValue("TypeOfAttributeId", value); }
        }

        /// <inheritdoc/>
        public String? TypeOfAttributeTitle
        {
            get { return GetValue("TypeOfAttributeTitle"); }
            set { SetValue("TypeOfAttributeTitle", value); }
        }

        /// <inheritdoc/>
        public Boolean? IsSingleValue
        {
            get { return GetValue<bool>("IsSingleValue", BindingItemParsers.BooleanTryParse); }
            set
            {
                SetValue<Boolean>("IsSingleValue", value);
                if (value is null) { SetValue<Boolean>("IsMultiValue", null); }
                else { SetValue<Boolean>("IsMultiValue", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean? IsSimple
        {
            get { return GetValue<bool>("IsSimple", BindingItemParsers.BooleanTryParse); }
            set
            {
                SetValue<Boolean>("IsSimple", value);
                if (value is null) { SetValue<Boolean>("IsComposite", null); }
                else { SetValue<Boolean>("IsComposite", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean? IsDerived
        {
            get { return GetValue<bool>("IsDerived", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("IsDerived", value); }
        }

        /// <inheritdoc/>
        public Boolean? IsNullable
        {
            get { return GetValue<bool>("IsNullable", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("IsNullable", value); }
        }

        /// <inheritdoc/>
        public Boolean? IsKey
        {
            get { return GetValue<bool>("IsKey", BindingItemParsers.BooleanTryParse); }
            set { SetValue<Boolean>("IsKey", value); }
        }

        /// <summary>
        /// Constructor for Domain Attribute Item
        /// </summary>
        public DomainAttributeItem() : base()
        {
            if (AttributeId is null) { AttributeId = Guid.NewGuid(); }
            if (String.IsNullOrWhiteSpace(AttributeTitle)) { AttributeTitle = "(new Attribute)"; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("AttributeTitle", typeof(string)){ AllowDBNull = false},
            new DataColumn("AttributeDescription", typeof(string)){ AllowDBNull = true},

            new DataColumn("TypeOfAttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("TypeOfAttributeTitle", typeof(string)){ AllowDBNull = true},

            new DataColumn("IsSingleValue", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsMultiValue", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsSimple", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsComposite", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsDerived", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsNullable", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsKey", typeof(bool)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Item
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainAttributeItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { if (AttributeTitle is not null) { return AttributeTitle; } else { return string.Empty; } }
    }
}
