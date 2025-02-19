// Ignore Spelling: Utc

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Interface for a Temporal Table Key (DateTime part)
    /// </summary>
    public interface ITemporalKey
    {
        /// <summary>
        /// As of Date expressed as a UTC date.
        /// </summary>
        DateTime AsOfUtcDate { get; }
    }

    /// <summary>
    /// Implementation for a Temporal Table Key (DateTime part)
    /// </summary>
    public class TemporalKey : ITemporalKey,
        IEquatable<ITemporal>, IComparable<ITemporal>,
        IEquatable<ITemporalItem>, IComparable<ITemporalItem>, IComparable
    {
        /// <inheritdoc/>
        public DateTime AsOfUtcDate { get; } = DateTime.MaxValue;

        /// <summary>
        /// Constructor for a blank Temporal Key 
        /// </summary>
        /// <remarks>Set to DateTime.UtcNow.</remarks>
        public TemporalKey() : base()
        { AsOfUtcDate = DateTime.UtcNow; }

        /// <summary>
        /// Constructor for the Temporal Key.
        /// </summary>
        /// <param name="source"></param>
        public TemporalKey(ITemporal source) : this()
        {
            if (source.CreatedOn is DateTime value)
            { AsOfUtcDate = value; }
        }

        /// <summary>
        /// Constructor for the Temporal Key.
        /// </summary>
        /// <param name="source"></param>
        public TemporalKey(ITemporalKey source) : this()
        {
            if (source.AsOfUtcDate is DateTime value)
            { AsOfUtcDate = value; }
        }

        /// <summary>
        /// Constructor for the Temporal Key.
        /// </summary>
        /// <param name="source"></param>
        public TemporalKey(ITemporalItem source) : this()
        {
            if (source.Temporal.CreatedOn is DateTime value)
            { AsOfUtcDate = value; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ITemporal? other)
        {
            return
                (other is TemporalKey
                    && AsOfUtcDate is DateTime thisValue
                    && other.CreatedOn is DateTime otherValue
                    && DateTime.Equals(thisValue, otherValue));
        }

        /// <inheritdoc/>
        public Boolean Equals(ITemporalItem? other)
        {
            return
                (other is TemporalKey
                    && AsOfUtcDate is DateTime thisValue
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
                AsOfUtcDate is DateTime thisValue &&
                other.CreatedOn is DateTime otherValue)
            { return DateTime.Compare(thisValue, otherValue); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(ITemporalItem? other)
        {
            if (other is ITemporal value &&
                AsOfUtcDate is DateTime thisValue &&
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
        { return AsOfUtcDate.GetHashCode(); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        {
            if (AsOfUtcDate is DateTime thisValue)
            { return thisValue.ToString(); }
            else { return String.Empty; }
        }
    }
}
