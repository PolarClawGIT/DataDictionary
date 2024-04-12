using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Instance.
    /// </summary>
    public interface IInstanceKey : IKey
    {
        /// <summary>
        /// Instance Id of the Scripting Instance.
        /// </summary>
        Guid? InstanceId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Instance.
    /// </summary>
    public class InstanceKey : IInstanceKey, IKeyEquality<IInstanceKey>
    {
        /// <inheritdoc/>
        public Guid? InstanceId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public InstanceKey(IInstanceKey source) : base()
        {
            if (source.InstanceId is Guid) { InstanceId = source.InstanceId; }
            else { InstanceId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IInstanceKey? other)
        { return other is IInstanceKey && EqualityComparer<Guid?>.Default.Equals(this.InstanceId, other.InstanceId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IInstanceKey value && this.Equals(new InstanceKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(InstanceKey left, InstanceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(InstanceKey left, InstanceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (InstanceId is Guid) { return (InstanceId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
