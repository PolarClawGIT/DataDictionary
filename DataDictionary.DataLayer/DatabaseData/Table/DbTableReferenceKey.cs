using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDictionary.DataLayer.DatabaseData.Catalog;

namespace DataDictionary.DataLayer.DatabaseData.Table
{
    /// <summary>
    /// Interface for the Database Table Reference Key
    /// </summary>
    public interface IDbTableReferenceKey : IKey, IDbCatalogKeyUnique
    {
        /// <summary>
        /// Name of the Database Schema being Referenced
        /// </summary>
        String? ReferenceSchemaName { get; }

        /// <summary>
        /// Name of the Database Object being Referenced
        /// </summary>
        String? ReferenceObjectName { get; }
    }

    /// <summary>
    /// Implementation of the Database Table Reference Key
    /// </summary>
    public class DbTableReferenceKey : DbCatalogKeyUnique, IDbTableReferenceKey, IKeyComparable<IDbTableReferenceKey>, IKeyEquality<IDbTableKey>
    {
        /// <inheritdoc/>
        public String ReferenceSchemaName { get; init; }

        /// <inheritdoc/>
        public String ReferenceObjectName { get; init; }

        /// <summary>
        /// Constructor for the Database Table Reference Key
        /// </summary>
        /// <param name="source"></param>
        public DbTableReferenceKey(IDbTableReferenceKey source) : base(source)
        {
            if (source.ReferenceSchemaName is string) { ReferenceSchemaName = source.ReferenceSchemaName; }
            else { ReferenceSchemaName = string.Empty; }

            if (source.ReferenceObjectName is string) { ReferenceObjectName = source.ReferenceObjectName; }
            else { ReferenceObjectName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbTableReferenceKey? other)
        {
            return
                other is IDbTableReferenceKey &&
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
            return obj is IDbTableReferenceKey value && Equals(new DbTableReferenceKey(value))
                || obj is IDbTableKey talbeValue && Equals(new DbTableKey(talbeValue));
        }

        /// <inheritdoc/>
        public int CompareTo(IDbTableReferenceKey? other)
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
        { if (obj is IDbTableReferenceKey value) { return CompareTo(new DbTableReferenceKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbTableReferenceKey left, DbTableReferenceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbTableReferenceKey left, DbTableReferenceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbTableReferenceKey left, DbTableReferenceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbTableReferenceKey left, DbTableReferenceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbTableReferenceKey left, DbTableReferenceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbTableReferenceKey left, DbTableReferenceKey right)
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
