using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbTableReferenceKey : IDbCatalogKeyUnique
    {
        String? ReferenceSchemaName { get; }
        String? ReferenceObjectName { get; }
    }

    public class DbTableReferenceKey : DbCatalogKeyUnique, IEquatable<DbTableReferenceKey>, IEquatable<DbTableKey>, IComparable<DbTableReferenceKey>, IComparable
    {
        String ReferenceSchemaName { get; init; }
        String ReferenceTableName { get; init; }

        public DbTableReferenceKey(IDbTableReferenceKey source) : base(source)
        {
            if (source.ReferenceSchemaName is String) { ReferenceSchemaName = source.ReferenceSchemaName; }
            else { ReferenceSchemaName = String.Empty; }

            if (source.ReferenceObjectName is String) { ReferenceTableName = source.ReferenceObjectName; }
            else { ReferenceTableName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(DbTableReferenceKey? other)
        {
            return (
                other is IDbTableReferenceKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !String.IsNullOrEmpty(ReferenceSchemaName) &&
                !String.IsNullOrEmpty(other.ReferenceSchemaName) &&
                !String.IsNullOrEmpty(ReferenceTableName) &&
                !String.IsNullOrEmpty(other.ReferenceTableName) &&
                ReferenceSchemaName.Equals(other.ReferenceSchemaName, ModelFactory.CompareString) &&
                ReferenceTableName.Equals(other.ReferenceTableName, ModelFactory.CompareString));
        }

        public Boolean Equals(DbTableKey? other)
        {
            return (
                other is IDbTableKey &&
                new DbCatalogKeyUnique(this).Equals(other) &&
                !String.IsNullOrEmpty(ReferenceSchemaName) &&
                !String.IsNullOrEmpty(other.SchemaName) &&
                !String.IsNullOrEmpty(ReferenceTableName) &&
                !String.IsNullOrEmpty(other.TableName) &&
                ReferenceSchemaName.Equals(other.SchemaName, ModelFactory.CompareString) &&
                ReferenceTableName.Equals(other.TableName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(DbTableReferenceKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogKeyUnique(this).CompareTo(other) is Int32 value && value != 0)
            { return value; }
            else {
                if (String.Compare(ReferenceSchemaName,
                                   other.ReferenceSchemaName,
                                   true) is Int32 schemaValue && schemaValue != 0)
                { return schemaValue; }
                else { return String.Compare(ReferenceTableName, other.ReferenceTableName, true); }
            }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbTableReferenceKey value) { return this.CompareTo(new DbTableReferenceKey(value)); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbTableReferenceKey value) { return this.Equals(new DbTableReferenceKey(value)); } else { return false; } }

        public static bool operator ==(DbTableReferenceKey left, DbTableReferenceKey right)
        { return left.Equals(right); }

        public static bool operator !=(DbTableReferenceKey left, DbTableReferenceKey right)
        { return !left.Equals(right); }

        public static bool operator <(DbTableReferenceKey left, DbTableReferenceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbTableReferenceKey left, DbTableReferenceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbTableReferenceKey left, DbTableReferenceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbTableReferenceKey left, DbTableReferenceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && ReferenceSchemaName is String && ReferenceTableName is String)
            { return (CatalogName, ReferenceSchemaName, ReferenceTableName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (ReferenceSchemaName is String && ReferenceTableName is String)
            { return String.Format("{0}.{1}.{2}", base.ToString(), ReferenceSchemaName, ReferenceTableName); }
            else { return String.Empty; }
        }
    }
}
