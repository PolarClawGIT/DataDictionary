using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeParentKey
    {
        Nullable<Guid> ParentAttributeId { get; }
    }

    public class DomainAttributeParentKey : IDomainAttributeParentKey, IEquatable<IDomainAttributeParentKey>, IEquatable<IDomainAttributeKey>
    {

        public Nullable<Guid> ParentAttributeId { get; init; } = Guid.Empty;

        public DomainAttributeParentKey(IDomainAttributeParentKey source) : base()
        {
            if (source.ParentAttributeId is Guid) { ParentAttributeId = source.ParentAttributeId; }
            else { ParentAttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDomainAttributeParentKey? other)
        {
            return other is IDomainAttributeParentKey key &&
                EqualityComparer<Guid?>.Default.Equals(ParentAttributeId, key.ParentAttributeId);
        }

        public Boolean Equals(IDomainAttributeKey? other)
        {
            return other is IDomainAttributeKey key &&
                EqualityComparer<Guid?>.Default.Equals(ParentAttributeId, key.AttributeId);
        }

        public override bool Equals(object? obj)
        { return obj is IDomainAttributeKey key && this.Equals(key); }

        public static bool operator ==(DomainAttributeParentKey left, DomainAttributeParentKey right)
        { return left.Equals(right); }

        public static bool operator !=(DomainAttributeParentKey left, DomainAttributeParentKey right)
        { return !left.Equals(right); }


        public override Int32 GetHashCode()
        { return HashCode.Combine(ParentAttributeId); }
        #endregion

        public override string ToString()
        {
            if (ParentAttributeId is Guid && ParentAttributeId.ToString() is String value) { return value; }
            else { return String.Empty; }
        }
    }
}
