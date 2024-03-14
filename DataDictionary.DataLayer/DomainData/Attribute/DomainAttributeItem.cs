// Ignore Spelling: Nullable

using DataDictionary.DataLayer.ApplicationData.Scope;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Item
    /// </summary>
    public interface IDomainAttributeItem : IDomainAttributeKey, IDomainAttributeKeyName, IScopeKey, IDataItem
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
        /// Is Attribute Single Valued (has only one value, not multi-valued)
        /// </summary>
        Boolean IsSingleValue { get; set; }

        /// <summary>
        /// Is Attribute Multi Valued (has multiple values, not single)
        /// </summary>
        Boolean IsMultiValue { get; set; }

        /// <summary>
        /// Is Attribute a Simple Type (cannot be decomposed, not composite)
        /// </summary>
        Boolean IsSimpleType { get; set; }

        /// <summary>
        /// Is Attribute a Composite Type (composed of multiple Simple Types, not Simple)
        /// </summary>
        Boolean IsCompositeType { get; set; }

        /// <summary>
        /// Is Attribute a Derived Value (Computed value, not Integral)
        /// </summary>
        Boolean IsDerived { get; set; }

        /// <summary>
        /// Is Attribute an Integral value (distinct or basic value, not Derived)
        /// </summary>
        Boolean IsIntegral { get; set; }

        /// <summary>
        /// Is the Attribute a Null-able value (allows Null value)
        /// </summary>
        Boolean IsNullable { get; set; }

        /// <summary>
        /// Is the Attribute a valued (not null) attribute.
        /// </summary>
        Boolean IsValued { get; set; }

        /// <summary>
        /// Is the Attribute is used as a Key (part of a PK, FK, or AK)
        /// </summary>
        Boolean IsKey { get; set; }

        /// <summary>
        /// Is the Attribute a Non-Key item (not part of a PK, FK or AK)
        /// </summary>
        Boolean IsNonKey { get; set; }
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
        public ScopeType Scope { get { return ScopeType.ModelAttribute; } }

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
        public Boolean IsSingleValue
        {
            get
            {
                if (GetValue<bool>("IsSingleValue", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsSingleValue", value);
                if (value == true) { SetValue<Boolean>("IsMultiValue", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsMultiValue
        {
            get
            {
                if (GetValue<bool>("IsMultiValue", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsMultiValue", value);
                if (value == true) { SetValue<Boolean>("IsSingleValue", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsSimpleType
        {
            get
            {
                if (GetValue<bool>("IsSimpleType", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsSimpleType", value);
                if (value == true) { SetValue<Boolean>("IsCompositeType", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCompositeType
        {
            get
            {
                if (GetValue<bool>("IsCompositeType", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsCompositeType", value);
                if (value == true) { SetValue<Boolean>("IsSimpleType", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsIntegral
        {
            get
            {
                if (GetValue<bool>("IsIntegral", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsIntegral", value);
                if (value == true) { SetValue<Boolean>("IsDerived", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsDerived
        {
            get
            {
                if (GetValue<bool>("IsDerived", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsDerived", value);
                if (value == true) { SetValue<Boolean>("IsIntegral", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsValued
        {
            get
            {
                if (GetValue<bool>("IsValued", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsValued", value);
                if (value == true) { SetValue<Boolean>("IsNullable", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsNullable
        {
            get
            {
                if (GetValue<bool>("IsNullable", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsNullable", value);
                if (value == true) { SetValue<Boolean>("IsValued", !value); }
            }
        }



        /// <inheritdoc/>
        public Boolean IsKey
        {
            get
            {
                if (GetValue<bool>("IsKey", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsKey", value);
                if (value == true) { SetValue<Boolean>("IsNonKey", !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsNonKey
        {
            get
            {
                if (GetValue<bool>("IsNonKey", BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>("IsNonKey", value);
                if (value == true) { SetValue<Boolean>("IsKey", !value); }
            }
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
            new DataColumn("IsSimpleType", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsCompositeType", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsIntegral", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsDerived", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsValued", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsNullable", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsKey", typeof(bool)){ AllowDBNull = true},
            new DataColumn("IsNonKey", typeof(bool)){ AllowDBNull = true},
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
