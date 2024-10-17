using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Object Key.
    /// </summary>
    public interface IObjectKey : IKey
    {
        /// <summary>
        /// Application ID for the Object.
        /// </summary>
        Guid? ObjectId { get; }
    }

    /// <summary>
    /// Implementation for the Security Object Key.
    /// </summary>
    public class ObjectKey : IObjectKey,
        IKeyEquality<IObjectKey>, IKeyEquality<ObjectKey>
    {
        /// <inheritdoc/>
        public Guid? ObjectId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Security Object Key.
        /// </summary>
        /// <param name="source"></param>
        public ObjectKey(IObjectKey source) : base()
        {
            if (source.ObjectId is Guid value) { ObjectId = value; }
            else { ObjectId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ObjectKey? other)
        { return other is ObjectKey && EqualityComparer<Guid?>.Default.Equals(ObjectId, other.ObjectId); }

        /// <inheritdoc/>
        public virtual Boolean Equals(IObjectKey? other)
        { return other is IObjectKey value && Equals(new ObjectKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IObjectKey value && Equals(new ObjectKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ObjectKey left, ObjectKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ObjectKey left, ObjectKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ObjectId); }
        #endregion
    }
}
