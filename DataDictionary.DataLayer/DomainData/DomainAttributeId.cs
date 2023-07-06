using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeId
    {
        Nullable<Guid> AttributeId { get; }
    }

    public class DomainAttributeId: IDomainAttributeId, IEquatable<IDomainAttributeId>
    {
        public Nullable<Guid> AttributeId { get; init; }

        public DomainAttributeId() : base() { }

        public DomainAttributeId(IDomainAttributeId source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = null; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDomainAttributeId? other)
        {
            return (
                other is IDbSchemaName &&
                AttributeId is null &&
                other.AttributeId is null &&
                AttributeId.Equals(other.AttributeId));
        }

        public override bool Equals(object? obj)
        { if (obj is IDomainAttributeId value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DomainAttributeId left, IDomainAttributeId right)
        { return left.Equals(right); }

        public static bool operator !=(DomainAttributeId left, IDomainAttributeId right)
        { return !left.Equals(right); }


        public override Int32 GetHashCode()
        {
            if (AttributeId is Guid value)
            { return (value).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (AttributeId is Guid value)
            { return value.ToString(); }
            else { return String.Empty; }
        }
    }
}
