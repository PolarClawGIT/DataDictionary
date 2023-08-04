using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IModelKey
    {
        Nullable<Guid> ModelId { get; }
    }

    public class ModelKey : IModelKey, IEquatable<ModelKey>
    {
        public Nullable<Guid> ModelId { get; init; } = Guid.Empty;

        public ModelKey(IModelKey source) : base()
        {
            if (source.ModelId is Guid) { ModelId = source.ModelId; }
            else { ModelId = Guid.Empty; }
        }

        #region IEquatable
        public Boolean Equals(ModelKey? other)
        { return (other is ModelKey && this.ModelId.Equals(other.ModelId)); }

        public override bool Equals(object? obj)
        { if (obj is IModelKey value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(ModelKey left, ModelKey right)
        { return left.Equals(right); }

        public static bool operator !=(ModelKey left, ModelKey right)
        { return !left.Equals(right); }

        public override Int32 GetHashCode()
        {
            if (ModelId is Guid) { return (ModelId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
