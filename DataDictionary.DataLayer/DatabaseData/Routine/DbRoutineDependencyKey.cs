using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Dependencies Key
    /// </summary>
    public interface IDbRoutineDependencyKey : IKey, IDbRoutineKey, IDbColumnReferenceKey
    { }

    /// <summary>
    /// Implementation of the Database Routine Dependencies Key
    /// </summary>
    public class DbRoutineDependencyKey : DbRoutineKey, IDbRoutineDependencyKey, IKeyComparable<IDbRoutineDependencyKey>, IKeyEquality<IDbTableColumnKey>
    {
        /// <inheritdoc/>
        public String ReferenceSchemaName { get; init; }

        /// <inheritdoc/>
        public String ReferenceObjectName { get; init; }

        /// <inheritdoc/>
        public String ReferenceColumnName { get; init; }

        /// <summary>
        /// Constructor for the Database Routine Dependencies Key
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineDependencyKey(IDbRoutineDependencyKey source) : base(source)
        {
            if (source.ReferenceSchemaName is string) { ReferenceSchemaName = source.ReferenceSchemaName; }
            else { ReferenceSchemaName = string.Empty; }

            if (source.ReferenceObjectName is string) { ReferenceObjectName = source.ReferenceObjectName; }
            else { ReferenceObjectName = string.Empty; }

            if (source.ReferenceColumnName is string) { ReferenceColumnName = source.ReferenceColumnName; }
            else { ReferenceColumnName = string.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbRoutineDependencyKey? other)
        {
            return
                other is IDbObjectReferenceKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceSchemaName) &&
                !string.IsNullOrEmpty(other.ReferenceSchemaName) &&
                !string.IsNullOrEmpty(ReferenceObjectName) &&
                !string.IsNullOrEmpty(other.ReferenceObjectName) &&
                !string.IsNullOrEmpty(ReferenceColumnName) &&
                !string.IsNullOrEmpty(other.ReferenceColumnName) &&
                ReferenceSchemaName.Equals(other.ReferenceSchemaName, KeyExtension.CompareString) &&
                ReferenceObjectName.Equals(other.ReferenceObjectName, KeyExtension.CompareString) &&
                ReferenceColumnName.Equals(other.ReferenceColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineDependencyKey value && Equals(new DbRoutineDependencyKey(value)); }

        /// <inheritdoc/>
        public bool Equals(IDbTableColumnKey? other)
        {
            return
                other is IDbTableKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !string.IsNullOrEmpty(ReferenceSchemaName) &&
                !string.IsNullOrEmpty(other.SchemaName) &&
                !string.IsNullOrEmpty(ReferenceObjectName) &&
                !string.IsNullOrEmpty(other.TableName) &&
                !string.IsNullOrEmpty(ReferenceColumnName) &&
                !string.IsNullOrEmpty(other.ColumnName) &&
                ReferenceSchemaName.Equals(other.SchemaName, KeyExtension.CompareString) &&
                ReferenceObjectName.Equals(other.TableName, KeyExtension.CompareString) &&
                ReferenceColumnName.Equals(other.ColumnName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public int CompareTo(IDbRoutineDependencyKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbRoutineKey(this).CompareTo(other) is int value && value != 0)
            { return value; }
            else
            {
                if (string.Compare(ReferenceSchemaName,
                                   other.ReferenceSchemaName,
                                   true) is int schemaValue &&
                                   schemaValue != 0)
                { return schemaValue; }
                else if (string.Compare(ReferenceObjectName,
                    other.ReferenceObjectName,
                    true) is int tableValue &&
                    tableValue != 0)
                { return tableValue; }
                else
                { return string.Compare(ReferenceColumnName, other.ReferenceColumnName, true); }
            }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineDependencyKey value) { return CompareTo(new DbRoutineDependencyKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
                base.GetHashCode(),
                ReferenceSchemaName.GetHashCode(KeyExtension.CompareString),
                ReferenceObjectName.GetHashCode(KeyExtension.CompareString),
                ReferenceColumnName.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ReferenceSchemaName is string && ReferenceObjectName is string && ReferenceColumnName is string)
            { return string.Format("{0}.{1}.{2}.{3}", base.ToString(), ReferenceSchemaName, ReferenceObjectName, ReferenceColumnName); }
            else { return string.Empty; }
        }
    }
}
