// Ignore Spelling: Nullable

using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Item
    /// </summary>
    public interface IDomainAttributeItem : IDomainAttributeKey, IDomainAttributeKeyName, IScopeType
    {
        /// <summary>
        /// Description of the Domain Attribute
        /// </summary>
        String? AttributeDescription { get; set; }

        /// <summary>
        /// Member Name within the Subject Area.
        /// </summary>
        String? MemberName { get; set; }

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
            get { return GetValue<Guid>(nameof(AttributeId)); }
            protected set { SetValue(nameof(AttributeId), value); }
        }

        /// <inheritdoc/>
        public string? AttributeTitle
        {
            get { return GetValue(nameof(AttributeTitle)); }
            set { SetValue(nameof(AttributeTitle), value); }
        }

        /// <inheritdoc/>
        public string? AttributeDescription
        {
            get { return GetValue(nameof(AttributeDescription)); }
            set { SetValue(nameof(AttributeDescription), value); }
        }

        /// <inheritdoc/>
        public string? MemberName
        {
            get { return GetValue(nameof(MemberName)); }
            set { SetValue(nameof(MemberName), value); }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttribute; } }

        /// <inheritdoc/>
        public Guid? TypeOfAttributeId
        {
            get { return GetValue<Guid>(nameof(TypeOfAttributeId)); }
            set { SetValue(nameof(TypeOfAttributeId), value); }
        }

        /// <inheritdoc/>
        public String? TypeOfAttributeTitle
        {
            get { return GetValue(nameof(TypeOfAttributeTitle)); }
            set { SetValue(nameof(TypeOfAttributeTitle), value); }
        }

        /// <inheritdoc/>
        public Boolean IsSingleValue
        {
            get
            {
                if (GetValue<bool>(nameof(IsSingleValue), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsSingleValue), value);
                if (value == true) { SetValue<Boolean>(nameof(IsMultiValue), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsMultiValue
        {
            get
            {
                if (GetValue<bool>(nameof(IsMultiValue), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsMultiValue), value);
                if (value == true) { SetValue<Boolean>(nameof(IsSingleValue), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsSimpleType
        {
            get
            {
                if (GetValue<bool>(nameof(IsSimpleType), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsSimpleType), value);
                if (value == true) { SetValue<Boolean>(nameof(IsCompositeType), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsCompositeType
        {
            get
            {
                if (GetValue<bool>(nameof(IsCompositeType), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsCompositeType), value);
                if (value == true) { SetValue<Boolean>(nameof(IsSimpleType), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsIntegral
        {
            get
            {
                if (GetValue<bool>(nameof(IsIntegral), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsIntegral), value);
                if (value == true) { SetValue<Boolean>(nameof(IsDerived), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsDerived
        {
            get
            {
                if (GetValue<bool>(nameof(IsDerived), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsDerived), value);
                if (value == true) { SetValue<Boolean>(nameof(IsIntegral), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsValued
        {
            get
            {
                if (GetValue<bool>(nameof(IsValued), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsValued), value);
                if (value == true) { SetValue<Boolean>(nameof(IsNullable), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsNullable
        {
            get
            {
                if (GetValue<bool>(nameof(IsNullable), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsNullable), value);
                if (value == true) { SetValue<Boolean>(nameof(IsValued), !value); }
            }
        }



        /// <inheritdoc/>
        public Boolean IsKey
        {
            get
            {
                if (GetValue<bool>(nameof(IsKey), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsKey), value);
                if (value == true) { SetValue<Boolean>(nameof(IsNonKey), !value); }
            }
        }

        /// <inheritdoc/>
        public Boolean IsNonKey
        {
            get
            {
                if (GetValue<bool>(nameof(IsNonKey), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
            set
            {
                SetValue<Boolean>(nameof(IsNonKey), value);
                if (value == true) { SetValue<Boolean>(nameof(IsKey), !value); }
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
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeTitle), typeof(string)){ AllowDBNull = false},
            new DataColumn(nameof(AttributeDescription), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(MemberName), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(TypeOfAttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(TypeOfAttributeTitle), typeof(string)){ AllowDBNull = true},
            new DataColumn(nameof(IsSingleValue), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsMultiValue), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsSimpleType), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsCompositeType), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsIntegral), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsDerived), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsValued), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsNullable), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsKey), typeof(bool)){ AllowDBNull = true},
            new DataColumn(nameof(IsNonKey), typeof(bool)){ AllowDBNull = true},
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
