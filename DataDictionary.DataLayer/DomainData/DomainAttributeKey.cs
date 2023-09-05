using DataDictionary.DataLayer.DatabaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeKey
    {
        Nullable<Guid> AttributeId { get; }
    }

    public class DomainAttributeKey : IDomainAttributeKey, IEquatable<DomainAttributeKey>
    {

        public Nullable<Guid> AttributeId { get; init; } = Guid.Empty;

        public DomainAttributeKey(IDomainAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DomainAttributeKey? other)
        { return other is IDomainAttributeKey key && EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId); }

        public override bool Equals(object? obj)
        { return obj is IDomainAttributeKey value && this.Equals(new DomainAttributeKey(value)); }

        public static bool operator ==(DomainAttributeKey left, DomainAttributeKey right)
        { return left.Equals(right); }

        public static bool operator !=(DomainAttributeKey left, DomainAttributeKey right)
        { return !left.Equals(right); }


        public override Int32 GetHashCode()
        { return HashCode.Combine(AttributeId); }
        #endregion

        public override string ToString()
        {
            if (AttributeId is Guid && AttributeId.ToString() is String value) { return value; }
            else { return String.Empty; }
        }
    }
}
