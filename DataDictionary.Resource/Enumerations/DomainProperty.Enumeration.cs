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
    DomainPropertyEnumeration() : base() { }

    static DomainPropertyEnumeration()
    {
        List<DomainPropertyEnumeration> data = new List<DomainPropertyEnumeration>()
        {
            new DomainPropertyEnumeration() { Value = DomainPropertyType.Null, Name = String.Empty, DisplayName = "not defined" },
            new DomainPropertyEnumeration() { Value = DomainPropertyType.String, Name = "String", DisplayName = "String" },
            new DomainPropertyEnumeration() { Value = DomainPropertyType.Integer, Name = "Integer", DisplayName = "Integer" },
            new DomainPropertyEnumeration() { Value = DomainPropertyType.List, Name = "List", DisplayName = "List" },
            new DomainPropertyEnumeration() { Value = DomainPropertyType.Xml, Name = "Xml", DisplayName = "Xml" },
            new DomainPropertyEnumeration() { Value = DomainPropertyType.MS_ExtendedProperty, Name = "MS_ExtendedProperty", DisplayName = "MS Extended Property" },
        };

        BuildDictionary(data);
    }
}