using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IDefinitionKey
    {
        Nullable<Guid> DefinitionId { get; }
    }

    public class DefinitionKey : IDefinitionKey, IEquatable<DefinitionKey>
    {
        public Nullable<Guid> DefinitionId { get; init; } = Guid.Empty;

        public DefinitionKey(IDefinitionKey source) : base()
        {
            if (source.DefinitionId is Guid) { DefinitionId = source.DefinitionId; }
            else { DefinitionId = Guid.Empty; }
        }

        #region IEquatable
        public Boolean Equals(DefinitionKey? other)
        { return other is DefinitionKey && EqualityComparer<Guid?>.Default.Equals(this.DefinitionId, other.DefinitionId); }

        public override bool Equals(object? obj)
        { return obj is IDefinitionKey value && this.Equals(new DefinitionKey(value)); }

        public static bool operator ==(DefinitionKey left, DefinitionKey right)
        { return left.Equals(right); }

        public static bool operator !=(DefinitionKey left, DefinitionKey right)
        { return !left.Equals(right); }

        public override Int32 GetHashCode()
        {
            if (DefinitionId is Guid) { return (DefinitionId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
