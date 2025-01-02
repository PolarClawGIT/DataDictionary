using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// Interface for the Database Object Reference Key
    /// </summary>
    public interface IConstraintKeyReferenced : IKey, ICatalogKeyName
    {
        /// <summary>
        /// Name of the Database Schema being Referenced
        /// </summary>
        String? ReferencedSchemaName { get; }

        /// <summary>
        /// Name of the Database Table being Referenced
        /// </summary>
        String? ReferencedTableName { get; }
    }

    /// <summary>
    /// Implementation of the Database Object Reference Key
    /// </summary>
    public class ConstraintKeyReferenced : CatalogKeyName, IConstraintKeyReferenced,
        IKeyComparable<IConstraintKeyReferenced>, IKeyEquality<ITableKeyName>
    {
        /// <inheritdoc/>
        public String ReferencedSchemaName { get; init; }

        /// <inheritdoc/>
        public String ReferencedTableName { get; init; }

        /// <summary>
        /// Constructor for the Database Object Reference Key
        /// </summary>
        /// <param name="source"></param>
        public ConstraintKeyReferenced(IConstraintKeyReferenced source) : base(source)
        {
            if (source.ReferencedSchemaName is string) { ReferencedSchemaName = source.ReferencedSchemaName; }
            else { ReferencedSchemaName = string.Empty; }

            if (source.ReferencedTableName is string) { ReferencedTableName = source.ReferencedTableName; }
            else { ReferencedTableName = string.Empty; }
        }

        /// <summary>
        /// Converts the Constraint Referenced Key into a Table Key.
        /// </summary>
        /// <returns></returns>
        public virtual TableKeyName AsTableName()
        {
            return new TableKeyName()
            {
                DatabaseName = DatabaseName,
                SchemaName = ReferencedSchemaName,
                TableName = ReferencedTableName
            };
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(IConstraintKeyReferenced? other)
        {
            return
                other is IConstraintKeyReferenced &&
                new CatalogKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferencedSchemaName) &&
                !string.IsNullOrEmpty(other.ReferencedSchemaName) &&
                !string.IsNullOrEmpty(ReferencedTableName) &&
                !string.IsNullOrEmpty(other.ReferencedTableName) &&
                ReferencedSchemaName.Equals(other.ReferencedSchemaName, KeyExtension.CompareString) &&
                ReferencedTableName.Equals(other.ReferencedTableName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(ITableKeyName? other)
        {
            return
                other is ITableKeyName &&
                new CatalogKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferencedSchemaName) &&
                !string.IsNullOrEmpty(other.SchemaName) &&
                !string.IsNullOrEmpty(ReferencedTableName) &&
                !string.IsNullOrEmpty(other.TableName) &&
                ReferencedSchemaName.Equals(other.SchemaName, KeyExtension.CompareString) &&
                ReferencedTableName.Equals(other.TableName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        {
            return obj is IConstraintKeyReferenced value && Equals(new ConstraintKeyReferenced(value))
                || obj is ITableKeyName talbeValue && Equals(new TableKeyName(talbeValue));
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IConstraintKeyReferenced? other)
        {
            if (other is null) { return 1; }
            else if (new CatalogKeyName(this).CompareTo(other) is int value && value != 0)
            { return value; }
            else
            {
                if (string.Compare(ReferencedSchemaName,
                                   other.ReferencedSchemaName,
                                   true) is int schemaValue && schemaValue != 0)
                { return schemaValue; }
                else { return string.Compare(ReferencedTableName, other.ReferencedTableName, true); }
            }
        }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IConstraintKeyReferenced value) { return CompareTo(new ConstraintKeyReferenced(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ConstraintKeyReferenced left, ConstraintKeyReferenced right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ConstraintKeyReferenced left, ConstraintKeyReferenced right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ConstraintKeyReferenced left, ConstraintKeyReferenced right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ConstraintKeyReferenced left, ConstraintKeyReferenced right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ConstraintKeyReferenced left, ConstraintKeyReferenced right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ConstraintKeyReferenced left, ConstraintKeyReferenced right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                ReferencedSchemaName.GetHashCode(KeyExtension.CompareString),
                ReferencedTableName.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return DbObjectName.Format(DatabaseName, ReferencedSchemaName, ReferencedTableName); }
    }
}
