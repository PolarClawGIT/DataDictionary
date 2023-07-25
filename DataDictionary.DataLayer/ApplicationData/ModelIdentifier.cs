using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IModelIdentifier
    {
        Nullable<Guid> ModelId { get; }
    }

    public class ModelIdentifier : IModelIdentifier, IEquatable<IModelIdentifier>
    {
        public Nullable<Guid> ModelId { get; init; } = Guid.Empty;

        public ModelIdentifier() : base() { }

        public ModelIdentifier(IModelIdentifier source) : base()
        {
            if (source.ModelId is Guid) { ModelId = source.ModelId; }
            else { ModelId = Guid.Empty; }
        }

        #region IEquatable
        public Boolean Equals(IModelIdentifier? other)
        { return (other is IModelIdentifier && this.ModelId.Equals(other.ModelId)); }

        public override bool Equals(object? obj)
        { if (obj is IModelIdentifier value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(ModelIdentifier left, IModelIdentifier right)
        { return left.Equals(right); }

        public static bool operator !=(ModelIdentifier left, IModelIdentifier right)
        { return !left.Equals(right); }

        public override Int32 GetHashCode()
        {
            if (ModelId is Guid) { return (ModelId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
