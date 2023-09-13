using DataDictionary.DataLayer.DatabaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    /// <summary>
    /// Interface for the Primary Key of the Model.
    /// </summary>
    public interface IModelKey
    {
        /// <summary>
        /// The Id of the Model.
        /// </summary>
        Nullable<Guid> ModelId { get; }
    }

    /// <summary>
    /// Primary Key of the Model.
    /// </summary>
    public class ModelKey : IModelKey, IEquatable<ModelKey>
    {
        /// <inheritdoc/>
        public Nullable<Guid> ModelId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the ModelKey.
        /// </summary>
        /// <param name="source"></param>
        public ModelKey(IModelKey source) : base()
        {
            if (source.ModelId is Guid) { ModelId = source.ModelId; }
            else { ModelId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ModelKey? other)
        { return (other is ModelKey && this.ModelId.Equals(other.ModelId)); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { if (obj is IModelKey value) { return this.Equals(new ModelKey(value)); } else { return false; } }

        /// <inheritdoc/>
        public static bool operator ==(ModelKey left, ModelKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ModelKey left, ModelKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (ModelId is Guid) { return (ModelId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
