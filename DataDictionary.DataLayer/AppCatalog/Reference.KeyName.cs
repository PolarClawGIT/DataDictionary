using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Reference Key
    /// </summary>
    public interface IReferenceKeyName : IKey, ISchemaKeyName
    {
        /// <summary>
        /// Name of the Database Reference Object (Table, View, Procedure, Function, ...)
        /// </summary>
        String? ObjectName { get; }
    }

    /// <summary>
    /// Implementation of the Database Reference Key
    /// </summary>
    public class ReferenceKeyName : SchemaKeyName, IReferenceKeyName,
        IKeyComparable<IReferenceKeyName>, IKeyComparable<ReferenceKeyName>
    {
        /// <inheritdoc/>
        public String ObjectName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Reference Key
        /// </summary>
        protected internal ReferenceKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database Reference Key
        /// </summary>
        /// <param name="source"></param>
        public ReferenceKeyName(IReferenceKeyName source) : base(source)
        {
            if (source.ObjectName is string) { ObjectName = source.ObjectName; }
            else { ObjectName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Reference Key
        /// </summary>
        /// <param name="source"></param>
        public ReferenceKeyName(ITableKeyName source) : base(source)
        {
            if (source.TableName is string) { ObjectName = source.TableName; }
            else { ObjectName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Reference Key
        /// </summary>
        /// <param name="source"></param>
        public ReferenceKeyName(IRoutineKeyName source) : base(source)
        {
            if (source.RoutineName is string) { ObjectName = source.RoutineName; }
            else { ObjectName = string.Empty; }
        }

        /// <summary>
        /// Converts Reference Object Key into a Table Key.
        /// </summary>
        /// <returns></returns>
        public TableKeyName AsTable()
        {
            return new TableKeyName()
            {
                DatabaseName = DatabaseName,
                SchemaName = SchemaName,
                TableName = ObjectName
            };
        }

        /// <summary>
        /// Converts Reference Object Key into a Routine Key.
        /// </summary>
        /// <returns></returns>
        public RoutineKeyName AsRoutine()
        {
            return new RoutineKeyName()
            {
                DatabaseName = DatabaseName,
                SchemaName = SchemaName,
                RoutineName = ObjectName
            };
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ReferenceKeyName? other)
        {
            return
                other is ISchemaKeyName &&
                new SchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ObjectName) &&
                !string.IsNullOrEmpty(other.ObjectName) &&
                ObjectName.Equals(other.ObjectName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IReferenceKeyName? other)
        { return other is IReferenceKeyName value && Equals(new ReferenceKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IReferenceKeyName value && Equals(new ReferenceKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ReferenceKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new SchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ObjectName, other.ObjectName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IReferenceKeyName? other)
        { if (other is IReferenceKeyName value) { return CompareTo(new ReferenceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IReferenceKeyName value) { return CompareTo(new ReferenceKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ReferenceKeyName left, ReferenceKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ReferenceKeyName left, ReferenceKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ReferenceKeyName left, ReferenceKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ReferenceKeyName left, ReferenceKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ReferenceKeyName left, ReferenceKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ReferenceKeyName left, ReferenceKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ObjectName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return DbObjectName.Format(DatabaseName, SchemaName, ObjectName); }
    }
}
