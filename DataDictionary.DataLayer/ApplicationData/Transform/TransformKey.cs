using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData.Transform
{
    /// <summary>
    /// Interface for the Primary Key for the Transform.
    /// </summary>
    public interface ITransformKey : IKey
    {
        /// <summary>
        /// Transform Id of the Transform.
        /// </summary>
        Guid? TransformId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Transform.
    /// </summary>
    public class TransformKey : ITransformKey, IKeyEquality<ITransformKey>
    {
        /// <inheritdoc/>
        public Guid? TransformId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public TransformKey(ITransformKey source) : base()
        {
            if (source.TransformId is Guid) { TransformId = source.TransformId; }
            else { TransformId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ITransformKey? other)
        { return other is ITransformKey && EqualityComparer<Guid?>.Default.Equals(this.TransformId, other.TransformId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ITransformKey value && this.Equals(new TransformKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(TransformKey left, TransformKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(TransformKey left, TransformKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (TransformId is Guid) { return (TransformId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
