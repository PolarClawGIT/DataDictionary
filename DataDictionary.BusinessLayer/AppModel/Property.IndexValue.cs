using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{

    /// <summary>
    /// Interface for the Domain Property Index by Value.
    /// </summary>
    [Obsolete("Not Used", true)]
    public interface IPropertyIndexValue : IKey, IDomainPropertyType
    {
        [Obsolete("Not Used", true)]
        String? PropertyName { get; } }

    /// <summary>
    /// Implementation for the Domain Property Index by Value.
    /// </summary>
    [Obsolete("Not Used", true)]
    public class PropertyIndexValue : IPropertyIndexValue, IKeyEquality<IPropertyIndexValue>, IKeyEquality<IPropertyValue>
    {
        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
        public DomainPropertyType PropertyType { get; } = DomainPropertyType.Null;

        /// <inheritdoc/>
        /// <remarks>Domain</remarks>
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
        public PropertyIndexValue(AppCatalog.IPropertyValue source)
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
                PropertyType == other.PropertyType &&
                String.Equals(PropertyName, other.PropertyName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyValue? other)
        { return other is IPropertyValue && Equals(new PropertyIndexValue(other)); }

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
