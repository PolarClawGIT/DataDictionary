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

    public class DomainAttributeParentKey : IDomainAttributeParentKey, IEquatable<DomainAttributeParentKey>, IEquatable<DomainAttributeKey>
    {

        public Nullable<Guid> ParentAttributeId { get; init; } = Guid.Empty;

        public DomainAttributeParentKey(IDomainAttributeParentKey source) : base()
        {
            if (source.ParentAttributeId is Guid) { ParentAttributeId = source.ParentAttributeId; }
            else { ParentAttributeId = Guid.Empty; }
        }

        public DomainAttributeParentKey(IDomainAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { ParentAttributeId = source.AttributeId; }
            else { ParentAttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DomainAttributeParentKey? other)
        { return other is DomainAttributeParentKey key && EqualityComparer<Guid?>.Default.Equals(ParentAttributeId, key.ParentAttributeId); }

        public Boolean Equals(DomainAttributeKey? other)
        { return other is DomainAttributeKey key && EqualityComparer<Guid?>.Default.Equals(ParentAttributeId, key.AttributeId); }

        public override bool Equals(object? obj)
        {
            return (obj is IDomainAttributeParentKey value && this.Equals(new DomainAttributeParentKey(value)))
                || (obj is IDomainAttributeKey keyValue && this.Equals(new DomainAttributeParentKey(keyValue)));
        }

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
