using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ModelData
{
    /// <summary>
    /// Interface for the Primary Key of the Model.
    /// </summary>
    public interface IModelKey : IKey
    {
        /// <summary>
        /// The Id of the Model.
        /// </summary>
        Guid? ModelId { get; }
    }

    /// <summary>
    /// Primary Key of the Model.
    /// </summary>
    public class ModelKey : IModelKey,
        IKeyEquality<IModelKey>, IKeyEquality<ModelKey>
    {
        /// <inheritdoc/>
        public Guid? ModelId { get; init; } = Guid.Empty;

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
        { return other is ModelKey && ModelId.Equals(other.ModelId); }

        /// <inheritdoc/>
        public Boolean Equals(IModelKey? other)
        { if (other is IModelKey value) { return Equals(new ModelKey(value)); } else { return false; } }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { if (obj is IModelKey value) { return Equals(new ModelKey(value)); } else { return false; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ModelKey left, ModelKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ModelKey left, ModelKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (ModelId is Guid) { return ModelId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
