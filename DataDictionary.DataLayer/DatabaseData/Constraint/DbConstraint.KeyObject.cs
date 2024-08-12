using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Constraint
{
    /// <summary>
    /// Interface for the Database Object Reference Key
    /// </summary>
    public interface IDbConstraintKeyObject : IKey, IDbCatalogKeyName
    {
        /// <summary>
        /// Name of the Database Schema being Referenced
        /// </summary>
        String? ReferenceSchemaName { get; }

        /// <summary>
        /// Name of the Database Table being Referenced
        /// </summary>
        String? ReferenceTableName { get; }
    }

    /// <summary>
    /// Implementation of the Database Object Reference Key
    /// </summary>
    public class DbConstraintKeyObject : DbCatalogKeyName, IDbConstraintKeyObject, IKeyComparable<IDbConstraintKeyObject>, IKeyEquality<IDbTableKeyName>
    {
        /// <inheritdoc/>
        public String ReferenceSchemaName { get; init; }

        /// <inheritdoc/>
        public String ReferenceTableName { get; init; }

        /// <summary>
        /// Constructor for the Database Object Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintKeyObject(IDbConstraintKeyObject source) : base(source)
        {
            if (source.ReferenceSchemaName is string) { ReferenceSchemaName = source.ReferenceSchemaName; }
            else { ReferenceSchemaName = string.Empty; }

            if (source.ReferenceTableName is string) { ReferenceTableName = source.ReferenceTableName; }
            else { ReferenceTableName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Database Object (Table) Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbConstraintKeyObject(DbTableKeyName source) : base(source)
        {
            if (source.SchemaName is string) { ReferenceSchemaName = source.SchemaName; }
            else { ReferenceSchemaName = string.Empty; }

            if (source.TableName is string) { ReferenceTableName = source.TableName; }
            else { ReferenceTableName = string.Empty; }
        }


        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbConstraintKeyObject? other)
        {
            return
                other is IDbConstraintKeyObject &&
                new DbCatalogKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceSchemaName) &&
                !string.IsNullOrEmpty(other.ReferenceSchemaName) &&
                !string.IsNullOrEmpty(ReferenceTableName) &&
                !string.IsNullOrEmpty(other.ReferenceTableName) &&
                ReferenceSchemaName.Equals(other.ReferenceSchemaName, KeyExtension.CompareString) &&
                ReferenceTableName.Equals(other.ReferenceTableName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public bool Equals(IDbTableKeyName? other)
        {
            return
                other is IDbTableKeyName &&
                new DbCatalogKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceSchemaName) &&
                !string.IsNullOrEmpty(other.SchemaName) &&
                !string.IsNullOrEmpty(ReferenceTableName) &&
                !string.IsNullOrEmpty(other.TableName) &&
                ReferenceSchemaName.Equals(other.SchemaName, KeyExtension.CompareString) &&
                ReferenceTableName.Equals(other.TableName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is IDbConstraintKeyObject value && Equals(new DbConstraintKeyObject(value))
                || obj is IDbTableKeyName talbeValue && Equals(new DbTableKeyName(talbeValue));
        }

        /// <inheritdoc/>
        public int CompareTo(IDbConstraintKeyObject? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogKeyName(this).CompareTo(other) is int value && value != 0)
            { return value; }
            else
            {
                if (string.Compare(ReferenceSchemaName,
                                   other.ReferenceSchemaName,
                                   true) is int schemaValue && schemaValue != 0)
                { return schemaValue; }
                else { return string.Compare(ReferenceTableName, other.ReferenceTableName, true); }
            }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbConstraintKeyObject value) { return CompareTo(new DbConstraintKeyObject(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbConstraintKeyObject left, DbConstraintKeyObject right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbConstraintKeyObject left, DbConstraintKeyObject right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbConstraintKeyObject left, DbConstraintKeyObject right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbConstraintKeyObject left, DbConstraintKeyObject right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbConstraintKeyObject left, DbConstraintKeyObject right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbConstraintKeyObject left, DbConstraintKeyObject right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                ReferenceSchemaName.GetHashCode(KeyExtension.CompareString),
                ReferenceTableName.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ReferenceSchemaName is string && ReferenceTableName is string)
            { return string.Format("{0}.{1}.{2}", base.ToString(), ReferenceSchemaName, ReferenceTableName); }
            else { return string.Empty; }
        }
    }
}
