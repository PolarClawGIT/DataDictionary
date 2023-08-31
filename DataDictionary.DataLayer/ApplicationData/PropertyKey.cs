using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IPropertyKey
    {
        Nullable<Guid> PropertyId { get; }
    }

    public class PropertyKey : IPropertyKey, IEquatable<PropertyKey>
    {
        public Nullable<Guid> PropertyId { get; init; } = Guid.Empty;

        public PropertyKey(IPropertyKey source) : base()
        {
            if (source.PropertyId is Guid) { PropertyId = source.PropertyId; }
            else { PropertyId = Guid.Empty; }
        }

        #region IEquatable
        public Boolean Equals(PropertyKey? other)
        { return other is PropertyKey && EqualityComparer<Guid?>.Default.Equals(this.PropertyId, other.PropertyId); }

        public override bool Equals(object? obj)
        { return obj is IPropertyKey value && this.Equals(new PropertyKey(value)); }

        public static bool operator ==(PropertyKey left, PropertyKey right)
        { return left.Equals(right); }

        public static bool operator !=(PropertyKey left, PropertyKey right)
        { return !left.Equals(right); }

        public override Int32 GetHashCode()
        {
            if (PropertyId is Guid) { return (PropertyId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
