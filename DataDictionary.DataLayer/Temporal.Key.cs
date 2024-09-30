using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Interface describes a Temporal Table Key (DateTime part)
    /// </summary>
    public interface ITemporalKey : IKey
    {
        /// <summary>
        /// Date (UTC converted to Local) that the record was Modified
        /// </summary>
        DateTime? ModifiedOn { get; }
    }

    /// <summary>
    /// Implementation for a Temporal Table Key (DateTime part)
    /// </summary>
    public class TemporalKey : ITemporalKey, IKeyComparable<ITemporalKey>
    {
        /// <inheritdoc/>
        public DateTime? ModifiedOn { get; } = DateTime.MaxValue;

        /// <summary>
        /// Constructor for a blank Temporal Key
        /// </summary>
        protected internal TemporalKey() : base() { }

        /// <summary>
        /// Constructor for the Temporal Key.
        /// </summary>
        /// <param name="source"></param>
        public TemporalKey(ITemporalKey source) : base()
        {
            if (source.ModifiedOn is DateTime value)
            { ModifiedOn = value; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ITemporalKey? other)
        {
            return
                (other is TemporalKey 
                    && ModifiedOn is null 
                    && other.ModifiedOn is null ) ||
                ( other is TemporalKey
                    && ModifiedOn is DateTime thisValue
                    && other.ModifiedOn is DateTime otherValue
                    && DateTime.Equals(thisValue, otherValue));
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemporalKey value && Equals(new TemporalKey(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ITemporalKey? other)
        {
            if (other is ITemporalKey value &&
                ModifiedOn is DateTime thisValue &&
                other.ModifiedOn is DateTime otherValue)
            { return DateTime.Compare(thisValue, otherValue); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(Object? obj)
        { if (obj is ITemporalKey value) { return CompareTo(new TemporalKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(TemporalKey left, TemporalKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemporalKey left, TemporalKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(TemporalKey left, TemporalKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(TemporalKey left, TemporalKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(TemporalKey left, TemporalKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(TemporalKey left, TemporalKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return ModifiedOn.GetHashCode(); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (ModifiedOn is DateTime thisValue)
            { return thisValue.ToString(); }
            else { return String.Empty; }
        }
    }
}
