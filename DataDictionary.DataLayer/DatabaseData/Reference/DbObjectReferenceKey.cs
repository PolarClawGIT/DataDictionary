using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;

namespace DataDictionary.DataLayer.DatabaseData.Reference
{
    /// <summary>
    /// Interface for the Database Table Reference Key
    /// </summary>
    public interface IDbObjectReferenceKey : IKey, IDbCatalogKeyUnique
    {
        /// <summary>
        /// Name of the Database Schema being Referenced
        /// </summary>
        string? ReferenceSchemaName { get; }

        /// <summary>
        /// Name of the Database Object being Referenced
        /// </summary>
        string? ReferenceObjectName { get; }
    }

    /// <summary>
    /// Implementation of the Database Table Reference Key
    /// </summary>
    public class DbObjectReferenceKey : DbCatalogKeyUnique, IDbObjectReferenceKey, IKeyComparable<IDbObjectReferenceKey>, IKeyEquality<IDbTableKey>
    {
        /// <inheritdoc/>
        public string ReferenceSchemaName { get; init; }

        /// <inheritdoc/>
        public string ReferenceObjectName { get; init; }

        /// <summary>
        /// Constructor for the Database Table Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbObjectReferenceKey(IDbObjectReferenceKey source) : base(source)
        {
            if (source.ReferenceSchemaName is string) { ReferenceSchemaName = source.ReferenceSchemaName; }
            else { ReferenceSchemaName = string.Empty; }

            if (source.ReferenceObjectName is string) { ReferenceObjectName = source.ReferenceObjectName; }
            else { ReferenceObjectName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbObjectReferenceKey? other)
        {
            return
                other is IDbObjectReferenceKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceSchemaName) &&
                !string.IsNullOrEmpty(other.ReferenceSchemaName) &&
                !string.IsNullOrEmpty(ReferenceObjectName) &&
                !string.IsNullOrEmpty(other.ReferenceObjectName) &&
                ReferenceSchemaName.Equals(other.ReferenceSchemaName, KeyExtension.CompareString) &&
                ReferenceObjectName.Equals(other.ReferenceObjectName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public bool Equals(IDbTableKey? other)
        {
            return
                other is IDbTableKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceSchemaName) &&
                !string.IsNullOrEmpty(other.SchemaName) &&
                !string.IsNullOrEmpty(ReferenceObjectName) &&
                !string.IsNullOrEmpty(other.TableName) &&
                ReferenceSchemaName.Equals(other.SchemaName, KeyExtension.CompareString) &&
                ReferenceObjectName.Equals(other.TableName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is IDbObjectReferenceKey value && Equals(new DbObjectReferenceKey(value))
                || obj is IDbTableKey talbeValue && Equals(new DbTableKey(talbeValue));
        }

        /// <inheritdoc/>
        public int CompareTo(IDbObjectReferenceKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogKeyUnique(this).CompareTo(other) is int value && value != 0)
            { return value; }
            else
            {
                if (string.Compare(ReferenceSchemaName,
                                   other.ReferenceSchemaName,
                                   true) is int schemaValue && schemaValue != 0)
                { return schemaValue; }
                else { return string.Compare(ReferenceObjectName, other.ReferenceObjectName, true); }
            }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbObjectReferenceKey value) { return CompareTo(new DbObjectReferenceKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbObjectReferenceKey left, DbObjectReferenceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbObjectReferenceKey left, DbObjectReferenceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbObjectReferenceKey left, DbObjectReferenceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbObjectReferenceKey left, DbObjectReferenceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbObjectReferenceKey left, DbObjectReferenceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbObjectReferenceKey left, DbObjectReferenceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogName, ReferenceSchemaName, ReferenceObjectName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ReferenceSchemaName is string && ReferenceObjectName is string)
            { return string.Format("{0}.{1}.{2}", base.ToString(), ReferenceSchemaName, ReferenceObjectName); }
            else { return string.Empty; }
        }
    }
}
