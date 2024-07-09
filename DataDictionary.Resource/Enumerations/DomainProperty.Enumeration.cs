using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for DomainProperty.
/// </summary>
public class DomainPropertyEnumeration : Enumeration<DomainPropertyType, DomainPropertyEnumeration>
{
    /// <summary>
    /// Internal Constructor for DomainProperty Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DomainPropertyEnumeration(DomainPropertyType value, String name) : base(value, name) { }

    /// <summary>
    /// Static constructor, loads data.
    /// </summary>
    static DomainPropertyEnumeration()
    {
        List<DomainPropertyEnumeration> data = new List<DomainPropertyEnumeration>()
        {
            new DomainPropertyEnumeration(DomainPropertyType.Null, String.Empty){ DisplayName = "not defined" },
            new DomainPropertyEnumeration(DomainPropertyType.String,              "String"),
            new DomainPropertyEnumeration(DomainPropertyType.Integer,             "Integer"),
            new DomainPropertyEnumeration(DomainPropertyType.List,                "List"),
            new DomainPropertyEnumeration(DomainPropertyType.Xml,                 "Xml"),
            new DomainPropertyEnumeration(DomainPropertyType.MS_ExtendedProperty, "MS_ExtendedProperty"){ DisplayName = "MS Extended Property" },
        };

        BuildDictionary(data);
    }
}