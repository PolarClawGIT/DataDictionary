using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Delegate for attempting to retrieve a property value based on a property index.
    /// </summary>
    /// <param name="propertyIndex">The index of the property to retrieve.</param>
    /// <param name="propertyValue">The retrieved property value, if found; otherwise, null.</param>
    /// <returns>True if the property value was successfully retrieved; otherwise, false.</returns>
    public delegate Boolean TryGetProperty(IPropertyIndex propertyIndex, [NotNullWhen(true)] out IPropertyValue? propertyValue);
}