namespace DataDictionary.DataLayer.AppModel;

/// <summary>
/// Interface for the Model Attribute
/// </summary>
public interface IAttribute : IAttributeKeyName
{
    /// <summary>
    /// Description of the Model Attribute
    /// </summary>
    String? AttributeDescription { get; set; }

    /// <summary>
    /// Name within the Subject Area.
    /// </summary>
    String? AttributeName { get; set; }

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