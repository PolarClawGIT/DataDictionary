using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeDefinitionKey : IDomainAttributeKey, IDefinitionKey
    { }

    public class DomainAttributeDefinitionKey : IDomainAttributeDefinitionKey, IEquatable<DomainAttributeDefinitionKey>
    {
        public Nullable<Guid> AttributeId { get; init; } = Guid.Empty;
        public Nullable<Guid> DefinitionId { get; init; } = Guid.Empty;

        public DomainAttributeDefinitionKey(IDomainAttributeDefinitionKey source) : base()
        {
            if (source.DefinitionId is Guid) { DefinitionId = source.DefinitionId; }
            else { DefinitionId = Guid.Empty; }

            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable
        public Boolean Equals(DomainAttributeDefinitionKey? other)
        {
            return other is DomainAttributeDefinitionKey
                && EqualityComparer<Guid?>.Default.Equals(this.DefinitionId, other.DefinitionId)
                && EqualityComparer<Guid?>.Default.Equals(this.AttributeId, other.AttributeId);
        }

        public override bool Equals(object? obj)
        { if (obj is IDomainAttributeDefinitionKey value) { return this.Equals(new DomainAttributeDefinitionKey(value)); } else { return false; } }

        public static bool operator ==(DomainAttributeDefinitionKey left, DomainAttributeDefinitionKey right)
        { return left.Equals(right); }

        public static bool operator !=(DomainAttributeDefinitionKey left, DomainAttributeDefinitionKey right)
        { return !left.Equals(right); }

        public override Int32 GetHashCode()
        { return HashCode.Combine(AttributeId, DefinitionId); }
        #endregion
    }
}
