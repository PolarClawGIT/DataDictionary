using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainEntityAliasKey: IDbSchemaKey
    {
        String? ObjectName { get; }
    }

    public class DomainEntityAliasKey: DbSchemaKey, IDomainEntityAliasKey, IEquatable<DomainEntityAliasKey>, IComparable<DomainEntityAliasKey>, IComparable
    {
        public String ObjectName { get; init; } = String.Empty;

        public DomainEntityAliasKey(IDomainEntityAliasKey source) : base(source)
        { if (source.ObjectName is String) { ObjectName = source.ObjectName; } }

        public DomainEntityAliasKey(IDbTableKey source) : base (source)
        { if (source.TableName is String) { ObjectName = source.TableName; } }

        public DomainEntityAliasKey(IDbRoutineKey source) : base(source)
        { if (source.RoutineName is String) { ObjectName = source.RoutineName; } }

        #region IEquatable, IComparable
        public Boolean Equals(DomainEntityAliasKey? other)
        {
            return (
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !String.IsNullOrEmpty(ObjectName) &&
                !String.IsNullOrEmpty(other.ObjectName) &&
                ObjectName.Equals(other.ObjectName, ModelFactory.CompareString));
        }

        public Int32 CompareTo(DomainEntityAliasKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ObjectName, other.ObjectName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDomainEntityAliasKey value) { return this.CompareTo(new DomainEntityAliasKey(value)); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDomainEntityAliasKey value) { return this.Equals(new DomainEntityAliasKey(value)); } else { return false; } }

        public static bool operator ==(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return left.Equals(right); }

        public static bool operator !=(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return !left.Equals(right); }

        public static bool operator <(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, ObjectName); }
        #endregion

        public override string ToString()
        {
            if (ObjectName is String)
            { return String.Format("{0}.{1}", base.ToString(), ObjectName); }
            else { return String.Empty; }
        }
    }
}
