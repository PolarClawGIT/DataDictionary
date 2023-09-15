using DataDictionary.DataLayer.DatabaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeAliasKey : IDomainEntityAliasKey
    {
        String? ElementName { get; }
    }

    public class DomainAttributeAliasKey : DomainEntityAliasKey, IDomainAttributeAliasKey, IEquatable<DomainAttributeAliasKey>, IComparable<DomainAttributeAliasKey>, IComparable
    {
        public String ElementName { get; } = String.Empty;

        public DomainAttributeAliasKey(IDomainAttributeAliasKey source) : base(source)
        { if (source.ElementName is String) { ElementName = source.ElementName; } }

        public DomainAttributeAliasKey(IDbTableColumnKey source) : base(source)
        { if (source.ColumnName is String) { ElementName = source.ColumnName; } }

        public DomainAttributeAliasKey(IDbRoutineParameterKey source) : base(source)
        { if (source.ParameterName is String) { ElementName = source.ParameterName; } }

        #region IEquatable, IComparable
        public Boolean Equals(DomainAttributeAliasKey? other)
        {
            return (
                other is IDomainEntityAliasKey &&
                new DomainEntityAliasKey(this).Equals(other) &&
                !String.IsNullOrEmpty(ElementName) &&
                !String.IsNullOrEmpty(other.ElementName) &&
                ElementName.Equals(other.ElementName, KeyExtension.CompareString));
        }

        public override bool Equals(object? obj)
        { return obj is IDomainAttributeAliasKey value && this.Equals(new DomainAttributeAliasKey(value)); }

        public Int32 CompareTo(DomainAttributeAliasKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(ElementName, other.ElementName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDomainAttributeAliasKey value) { return this.CompareTo(new DomainAttributeAliasKey(value)); } else { return 1; } }

        public static bool operator ==(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return left.Equals(right); }

        public static bool operator !=(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return !left.Equals(right); }

        public static bool operator <(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, ObjectName, ElementName); }
        #endregion

        public override string ToString()
        {
            if (ElementName is String)
            { return String.Format("{0}.{1}", base.ToString(), ElementName); }
            else { return String.Empty; }
        }
    }
}
