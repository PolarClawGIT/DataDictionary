using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Interface for Domain Entity Alias Key
    /// </summary>
    public interface IDomainEntityAliasKey : IDbSchemaKey
    {
        /// <summary>
        /// Object Name for the Domain Entity Alias
        /// </summary>
        String? ObjectName { get; }
    }

    /// <summary>
    /// Implementation of Domain Entity Alias Key
    /// </summary>
    public class DomainEntityAliasKey : DbSchemaKey, IDomainEntityAliasKey, IKeyComparable<IDomainEntityAliasKey>
    {
        /// <inheritdoc/>
        public string ObjectName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for Domain Entity Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityAliasKey(IDomainEntityAliasKey source) : base(source)
        { if (source.ObjectName is string) { ObjectName = source.ObjectName; } }

        /// <summary>
        /// Constructor for Domain Entity Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityAliasKey(IDbTableKey source) : base(source)
        { if (source.TableName is string) { ObjectName = source.TableName; } }

        /// <summary>
        /// Constructor for Domain Entity Alias Key
        /// </summary>
        /// <param name="source"></param>
        public DomainEntityAliasKey(IDbRoutineKey source) : base(source)
        { if (source.RoutineName is string) { ObjectName = source.RoutineName; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainEntityAliasKey? other)
        {
            return 
                other is IDbSchemaKey &&
                new DbSchemaKey(this).Equals(other) &&
                !string.IsNullOrEmpty(ObjectName) &&
                !string.IsNullOrEmpty(other.ObjectName) &&
                ObjectName.Equals(other.ObjectName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainEntityAliasKey value && Equals(new DomainEntityAliasKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDomainEntityAliasKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ObjectName, other.ObjectName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDomainEntityAliasKey value) { return CompareTo(new DomainEntityAliasKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainEntityAliasKey left, DomainEntityAliasKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(DatabaseName, SchemaName, ObjectName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ObjectName is string)
            { return string.Format("{0}.{1}", base.ToString(), ObjectName); }
            else { return string.Empty; }
        }
    }
}
