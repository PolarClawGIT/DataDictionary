using DataDictionary.DataLayer.DbMetaData;
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

    public class DomainAttributeKey : IEquatable<DomainAttributeKey>
    {

        public Nullable<Guid> AttributeId { get; init; } = Guid.Empty;

        public DomainAttributeKey(IDomainAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DomainAttributeKey? other)
        {
            return (
                other is IDomainAttributeKey &&
                AttributeId != Guid.Empty &&
                other.AttributeId != Guid.Empty &&
                AttributeId == other.AttributeId);
        }

        public override bool Equals(object? obj)
        { if (obj is DomainAttributeKey value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DomainAttributeKey left, DomainAttributeKey right)
        { return left.Equals(right); }

        public static bool operator !=(DomainAttributeKey left, DomainAttributeKey right)
        { return !left.Equals(right); }


        public override Int32 GetHashCode()
        { return AttributeId.GetHashCode(); }
        #endregion

        public override string ToString()
        {
            if (AttributeId is Guid && AttributeId.ToString() is String value) { return value; }
            else { return String.Empty; }
        }
    }
}
