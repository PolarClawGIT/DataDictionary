using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.Domain
{

    /// <summary>
    /// Interface for the Domain Property Index by Value.
    /// </summary>
    public interface IPropertyIndexValue : IKey, IDomainPropertyType, IDbExtendedPropertyName
    { }

    /// <summary>
    /// Implementation for the Domain Property Index by Value.
    /// </summary>
    public class PropertyIndexValue : IPropertyIndexValue, IKeyEquality<IPropertyIndexValue>, IKeyEquality<IPropertyValue>
    {
        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public DomainPropertyType PropertyType { get; } = DomainPropertyType.Null;

        /// <inheritdoc/>
        /// <remarks>Database</remarks>
        public String? PropertyName { get; } = String.Empty;

        /// <summary>
        /// Constructor to build PropertyIndexValue
        /// </summary>
        /// <param name="source"></param>
        public PropertyIndexValue(IPropertyIndexValue source)
        {
            PropertyType = source.PropertyType;
            PropertyName = source.PropertyName;
        }

        /// <summary>
        /// Constructor to build PropertyIndexValue
        /// </summary>
        /// <param name="source"></param>
        public PropertyIndexValue(IExtendedPropertyValue source)
        {
            PropertyType = DomainPropertyType.MS_ExtendedProperty;
            PropertyName = source.PropertyName;
        }

        /// <summary>
        /// Constructor to build PropertyIndexValue
        /// </summary>
        /// <param name="source"></param>
        public PropertyIndexValue(IPropertyValue source)
        {
            if (source.PropertyType is DomainPropertyType.MS_ExtendedProperty)
            {
                PropertyType = source.PropertyType;
                PropertyName = source.PropertyData;
            }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IPropertyIndexValue? other)
        {
            return
                other is IPropertyValue &&
                this.PropertyType == other.PropertyType &&
                String.Equals(this.PropertyName, other.PropertyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyValue? other)
        { return other is IPropertyValue && this.Equals(new PropertyIndexValue(other)); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IPropertyIndexValue value && Equals(new PropertyIndexValue(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyIndexValue left, PropertyIndexValue right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyIndexValue left, PropertyIndexValue right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(PropertyType.GetHashCode(), (PropertyName ?? String.Empty).GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            String result = DomainPropertyEnumeration.Cast(PropertyType).Name;
            if (!String.IsNullOrWhiteSpace(PropertyName))
            { result = String.Format("{0}.{1}", result, PropertyName); }

            return result;
        }

    }
}
