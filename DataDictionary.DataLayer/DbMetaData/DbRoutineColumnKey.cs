using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbRoutineColumnKey : IDbRoutineKey, IDbTableReferenceKey
    {
        String? ReferenceColumnName { get; }
    }

    public class DbRoutineColumnKey : DbRoutineKey, IDbRoutineColumnKey, IEquatable<DbRoutineColumnKey>, IComparable<DbRoutineColumnKey>, IComparable
    {
        public String ReferenceSchemaName { get; init; }
        public String ReferenceObjectName { get; init; }
        public String ReferenceColumnName { get; init; }

        public DbRoutineColumnKey(IDbRoutineColumnKey source) : base(source)
        {
            if (source.ReferenceSchemaName is String) { ReferenceSchemaName = source.ReferenceSchemaName; }
            else { ReferenceSchemaName = String.Empty; }

            if (source.ReferenceObjectName is String) { ReferenceObjectName = source.ReferenceObjectName; }
            else { ReferenceObjectName = String.Empty; }

            if (source.ReferenceColumnName is String) { ReferenceColumnName = source.ReferenceColumnName; }
            else { ReferenceColumnName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbRoutineColumnKey? other)
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

        public Int32 CompareTo(DbRoutineColumnKey? other)
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
        { if (obj is IDbRoutineColumnKey value) { return this.CompareTo(new DbRoutineColumnKey(value)); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbRoutineColumnKey value) { return this.Equals(new DbRoutineColumnKey(value)); } else { return false; } }

        public static bool operator ==(DbRoutineColumnKey left, DbRoutineColumnKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbRoutineColumnKey left, DbRoutineColumnKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbRoutineColumnKey left, DbRoutineColumnKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbRoutineColumnKey left, DbRoutineColumnKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbRoutineColumnKey left, DbRoutineColumnKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbRoutineColumnKey left, DbRoutineColumnKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String && RoutineName is String && ReferenceSchemaName is String && ReferenceObjectName is String && ReferenceColumnName is String)
            { return (CatalogName, SchemaName, RoutineName, ReferenceSchemaName, ReferenceObjectName, ReferenceColumnName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (ReferenceSchemaName is String && ReferenceObjectName is String && ReferenceColumnName is String)
            { return String.Format("{0}.{1}.{2}.{3}", base.ToString(), ReferenceSchemaName, ReferenceObjectName, ReferenceColumnName); }
            else { return String.Empty; }
        }
    }
}
