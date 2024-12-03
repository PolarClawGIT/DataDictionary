using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{

    /// <summary>
    /// Interface for the Database RoutineColumn  Key
    /// </summary>
    public interface IRoutineColumnKeyName : IKey, IRoutineKeyName
    {
        /// <summary>
        /// Name of the Database RoutineColumn (Type)
        /// </summary>
        String? ColumnName { get; }
    }

    /// <summary>
    /// Implementation of the Database RoutineColumnKey
    /// </summary>
    public class RoutineColumnKeyName : RoutineKeyName, IRoutineColumnKeyName,
        IKeyComparable<IRoutineColumnKeyName>, IKeyComparable<RoutineColumnKeyName>
    {
        /// <inheritdoc/>
        public String ColumnName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database RoutineColumn Key
        /// </summary>
        protected internal RoutineColumnKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database RoutineColumn Key
        /// </summary>
        /// <param name="source"></param>
        public RoutineColumnKeyName(IRoutineColumnKeyName source) : base(source)
        {
            if (source.ColumnName is string) { ColumnName = source.ColumnName; }
            else { ColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(RoutineColumnKeyName? other)
        {
            return
                other is SchemaKeyName &&
                new SchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IRoutineColumnKeyName? other)
        { return other is IRoutineColumnKeyName value && Equals(new RoutineColumnKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IRoutineColumnKeyName value && Equals(new RoutineColumnKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(RoutineColumnKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new SchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ColumnName, other.ColumnName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IRoutineColumnKeyName? other)
        { if (other is IRoutineColumnKeyName value) { return CompareTo(new RoutineColumnKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IRoutineColumnKeyName value) { return CompareTo(new RoutineColumnKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(RoutineColumnKeyName left, RoutineColumnKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(RoutineColumnKeyName left, RoutineColumnKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(RoutineColumnKeyName left, RoutineColumnKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(RoutineColumnKeyName left, RoutineColumnKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(RoutineColumnKeyName left, RoutineColumnKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(RoutineColumnKeyName left, RoutineColumnKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }


        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ColumnName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return DbObjectName.Format(DatabaseName, SchemaName, ColumnName); }

    }

}
