using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    public interface IDbRoutineDependencyKey : IDbRoutineKey, IDbTableReferenceKey
    {
        String? ReferenceColumnName { get; }
    }

    public class DbRoutineDependencyKey : DbRoutineKey, IDbRoutineDependencyKey, IEquatable<DbRoutineDependencyKey>, IComparable<DbRoutineDependencyKey>, IComparable
    {
        public String ReferenceSchemaName { get; init; }
        public String ReferenceObjectName { get; init; }
        public String ReferenceColumnName { get; init; }

        public DbRoutineDependencyKey(IDbRoutineDependencyKey source) : base(source)
        {
            if (source.ReferenceSchemaName is String) { ReferenceSchemaName = source.ReferenceSchemaName; }
            else { ReferenceSchemaName = String.Empty; }

            if (source.ReferenceObjectName is String) { ReferenceObjectName = source.ReferenceObjectName; }
            else { ReferenceObjectName = String.Empty; }

            if (source.ReferenceColumnName is String) { ReferenceColumnName = source.ReferenceColumnName; }
            else { ReferenceColumnName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbRoutineDependencyKey? other)
        {
            return (
                other is IDbTableReferenceKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !String.IsNullOrEmpty(ReferenceSchemaName) &&
                !String.IsNullOrEmpty(other.ReferenceSchemaName) &&
                !String.IsNullOrEmpty(ReferenceObjectName) &&
                !String.IsNullOrEmpty(other.ReferenceObjectName) &&
                !String.IsNullOrEmpty(ReferenceColumnName) &&
                !String.IsNullOrEmpty(other.ReferenceColumnName) &&
                ReferenceSchemaName.Equals(other.ReferenceSchemaName, ModelFactory.CompareString) &&
                ReferenceObjectName.Equals(other.ReferenceObjectName, ModelFactory.CompareString) &&
                ReferenceColumnName.Equals(other.ReferenceColumnName, ModelFactory.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IDbRoutineDependencyKey value && this.Equals(new DbRoutineDependencyKey(value)); }

        public Boolean Equals(DbTableColumnKey? other)
        {
            return (
                other is IDbTableKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !String.IsNullOrEmpty(ReferenceSchemaName) &&
                !String.IsNullOrEmpty(other.SchemaName) &&
                !String.IsNullOrEmpty(ReferenceObjectName) &&
                !String.IsNullOrEmpty(other.TableName) &&
                !String.IsNullOrEmpty(ReferenceColumnName) &&
                !String.IsNullOrEmpty(other.ColumnName) &&
                ReferenceSchemaName.Equals(other.SchemaName, ModelFactory.CompareString) &&
                ReferenceObjectName.Equals(other.TableName, ModelFactory.CompareString) &&
                ReferenceColumnName.Equals(other.ColumnName, ModelFactory.CompareString)
                );
        }

        public Int32 CompareTo(DbRoutineDependencyKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbRoutineKey(this).CompareTo(other) is Int32 value && value != 0)
            { return value; }
            else
            {
                if (String.Compare(ReferenceSchemaName,
                                   other.ReferenceSchemaName,
                                   true) is Int32 schemaValue &&
                                   schemaValue != 0)
                { return schemaValue; }
                else if (String.Compare(ReferenceObjectName,
                    other.ReferenceObjectName,
                    true) is Int32 tableValue &&
                    tableValue != 0)
                { return tableValue; }
                else
                { return String.Compare(ReferenceColumnName, other.ReferenceColumnName, true); }
            }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineDependencyKey value) { return this.CompareTo(new DbRoutineDependencyKey(value)); } else { return 1; } }

        public static bool operator ==(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbRoutineDependencyKey left, DbRoutineDependencyKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, RoutineName, ReferenceSchemaName, ReferenceObjectName, ReferenceColumnName); }
        #endregion

        public override string ToString()
        {
            if (ReferenceSchemaName is String && ReferenceObjectName is String && ReferenceColumnName is String)
            { return String.Format("{0}.{1}.{2}.{3}", base.ToString(), ReferenceSchemaName, ReferenceObjectName, ReferenceColumnName); }
            else { return String.Empty; }
        }
    }
}
