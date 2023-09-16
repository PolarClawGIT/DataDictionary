using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Property
{
    /// <summary>
    /// Interface for the Primary Key for the Property.
    /// </summary>
    public interface IPropertyKey : IKey
    {
        /// <summary>
        /// Property Id of the Property.
        /// </summary>
        Nullable<Guid> PropertyId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Property.
    /// </summary>
    public class PropertyKey : IPropertyKey, IKeyEquality<IPropertyKey>
    {
        /// <inheritdoc/>
        public Nullable<Guid> PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public PropertyKey(IPropertyKey source) : base()
        {
            if (source.PropertyId is Guid) { PropertyId = source.PropertyId; }
            else { PropertyId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IPropertyKey? other)
        { return other is IPropertyKey && EqualityComparer<Guid?>.Default.Equals(this.PropertyId, other.PropertyId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IPropertyKey value && this.Equals(new PropertyKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(PropertyKey left, PropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(PropertyKey left, PropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (PropertyId is Guid) { return (PropertyId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}

