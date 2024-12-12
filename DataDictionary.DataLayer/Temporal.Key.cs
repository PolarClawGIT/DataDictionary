using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Implementation for a Temporal Table Key (DateTime part)
    /// </summary>
    public class TemporalKey :
        IEquatable<ITemporal>, IComparable<ITemporal>,
        IEquatable<ITemporalItem>, IComparable<ITemporalItem>, IComparable
    {
        /// <inheritdoc cref="ITemporal.CreatedOn"/>
        public DateTime? CreatedOn { get; } = DateTime.MaxValue;

        /// <summary>
        /// Constructor for a blank Temporal Key
        /// </summary>
        protected internal TemporalKey() : base() { }

        /// <summary>
        /// Constructor for the Temporal Key.
        /// </summary>
        /// <param name="source"></param>
        public TemporalKey(ITemporal source) : base()
        {
            if (source.CreatedOn is DateTime value)
            { CreatedOn = value; }
        }

        /// <summary>
        /// Constructor for the Temporal Key.
        /// </summary>
        /// <param name="source"></param>
        public TemporalKey(ITemporalItem source) : base()
        {
            if (source.Temporal.CreatedOn is DateTime value)
            { CreatedOn = value; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ITemporal? other)
        {
            return
                (other is TemporalKey
                    && CreatedOn is null
                    && other.CreatedOn is null) ||
                (other is TemporalKey
                    && CreatedOn is DateTime thisValue
                    && other.CreatedOn is DateTime otherValue
                    && DateTime.Equals(thisValue, otherValue));
        }

        /// <inheritdoc/>
        public Boolean Equals(ITemporalItem? other)
        {
            return
                (other is TemporalKey
                    && CreatedOn is null
                    && other.Temporal.CreatedOn is null) ||
                (other is TemporalKey
                    && CreatedOn is DateTime thisValue
                    && other.Temporal.CreatedOn is DateTime otherValue
                    && DateTime.Equals(thisValue, otherValue));
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        {
            return (obj is ITemporal value && Equals(new TemporalKey(value)) ||
                (obj is ITemporalItem item && Equals(new TemporalKey(item))));
        }

        /// <inheritdoc/>
        public Int32 CompareTo(ITemporal? other)
        {
            if (other is ITemporal value &&
                CreatedOn is DateTime thisValue &&
                other.CreatedOn is DateTime otherValue)
            { return DateTime.Compare(thisValue, otherValue); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(ITemporalItem? other)
        {
            if (other is ITemporal value &&
                CreatedOn is DateTime thisValue &&
                other.Temporal.CreatedOn is DateTime otherValue)
            { return DateTime.Compare(thisValue, otherValue); }
            else { return 1; }
        }


        /// <inheritdoc/>
        public Int32 CompareTo(Object? obj)
        { if (obj is ITemporal value) { return CompareTo(new TemporalKey(value)); } else { return 1; } }

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
        { return CreatedOn.GetHashCode(); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (CreatedOn is DateTime thisValue)
            { return thisValue.ToString(); }
            else { return String.Empty; }
        }
    }
}
