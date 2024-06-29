using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Property
{
    /// <summary>
    /// Interface for the ScopeKey
    /// </summary>
    public interface IDomainPropertyTypeKey : IKey
    {
        /// <summary>
        /// Domain Property Type for the Item.
        /// </summary>
        DomainPropertyType PropertyType { get; }
    }

    /// <summary>
    /// Implementation for Database PropertyType Key.
    /// </summary>
    public class DomainPropertyTypeKey : IDomainPropertyTypeKey, IKeyComparable<IDomainPropertyTypeKey>, IParsable<DomainPropertyTypeKey>
    {
        /// <inheritdoc/>
        public DomainPropertyType PropertyType { get; init; } = DomainPropertyType.Null;

        /// <summary>
        /// Basic Constructor for the PropertyType Key.
        /// </summary>
        protected DomainPropertyTypeKey() : base() { }

        /// <summary>
        /// Constructor for the PropertyType Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainPropertyTypeKey(DomainPropertyType source) : this()
        { PropertyType = source; }

        /// <summary>
        /// Constructor for the PropertyType Key.
        /// </summary>
        /// <param name="source"></param>
        public DomainPropertyTypeKey(IDomainPropertyTypeKey source) : this()
        { if (source is IDomainPropertyTypeKey) { PropertyType = source.PropertyType; } }

        /// <summary>
        /// Converts a DomainPropertyType into a DomainPropertyTypeKey.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DomainPropertyTypeKey(DomainPropertyType source) { return new DomainPropertyTypeKey(source); }

        /// <summary>
        /// Converts a DomainPropertyTypeKey into a DomainPropertyType.
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DomainPropertyType(DomainPropertyTypeKey source) { return source.PropertyType; }

        #region IEquatable
        /// <inheritdoc/>
        public virtual Boolean Equals(IDomainPropertyTypeKey? other)
        {
            return other is IDomainPropertyTypeKey
                && this.PropertyType != DomainPropertyType.Null
                && other.PropertyType != DomainPropertyType.Null
                && this.PropertyType.Equals(other.PropertyType);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(IDomainPropertyTypeKey? other)
        {
            if (other is IDomainPropertyTypeKey value)
            { return string.Compare(this.ToString(), value.ToString(), true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        { if (obj is IDomainPropertyTypeKey value) { return CompareTo(new DomainPropertyTypeKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainPropertyTypeKey value && this.Equals(new DomainPropertyTypeKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(DomainPropertyTypeKey left, DomainPropertyTypeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainPropertyTypeKey left, DomainPropertyTypeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainPropertyTypeKey left, DomainPropertyTypeKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainPropertyTypeKey left, DomainPropertyTypeKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainPropertyTypeKey left, DomainPropertyTypeKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainPropertyTypeKey left, DomainPropertyTypeKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return PropertyType.GetHashCode(); }
        #endregion

        #region IParsable
        /// <summary>
        /// This is the list that translates the DomainPropertyType Enum to what the Database uses as PropertyType Name.
        /// </summary>
        static Dictionary<DomainPropertyType, String> parseName = new Dictionary<DomainPropertyType, String>()
        {
            { DomainPropertyType.String,              "String"},
            { DomainPropertyType.Integer,             "Integer"},
            { DomainPropertyType.List,                "List"},
            { DomainPropertyType.Xml,                 "Xml"},
            { DomainPropertyType.MS_ExtendedProperty, "MS_ExtendedProperty"},
        };

        /// <inheritdoc/>
        public static DomainPropertyTypeKey Parse(String source, IFormatProvider? provider = null)
        {
            if (String.IsNullOrEmpty(source))
            { throw new ArgumentNullException(nameof(source)); }

            if (DomainPropertyTypeKey.TryParse(source, provider, out DomainPropertyTypeKey? result))
            { return result; }
            else
            { return DomainPropertyType.Null; }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] String? source, IFormatProvider? provider, [MaybeNullWhen(false)] out DomainPropertyTypeKey result)
        { return TryParse(source, out result); }

        /// <summary>
        /// Tries to parse a string into a value.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="result">When this method returns, contains the result of successfully parsing</param>
        /// <returns>true if s was successfully parsed; otherwise, false.</returns>
        public static bool TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out DomainPropertyTypeKey result)
        {
            if (parseName.FirstOrDefault(w => w.Value.Equals(source, KeyExtension.CompareString))
                is KeyValuePair<DomainPropertyType, String> dbItem
                && dbItem.Key != DomainPropertyType.Null)
            { result = new DomainPropertyTypeKey() { PropertyType = dbItem.Key }; return true; }
            else { result = null; return false; }
        }
        #endregion

        /// <summary>
        /// Returns an IEnumerable of DomainPropertyTypeKey for each DomainPropertyType.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DomainPropertyTypeKey> Items()
        { return Enum.GetValues(typeof(DomainPropertyType)).Cast<DomainPropertyType>().Select(s => new DomainPropertyTypeKey() { PropertyType = s }); }

        /// <summary>
        /// Returns the PropertyType Name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (parseName.ContainsKey(PropertyType)) { return parseName[PropertyType]; }
            else { return PropertyType.ToString(); }
        }
    }
}
