using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Alias Key
    /// </summary>
    public interface IDomainAttributeAliasKey : IDomainEntityAliasKey
    {
        /// <summary>
        /// Element Name for the Domain Attribute Alias
        /// </summary>
        String? ElementName { get; }
    }

    /// <summary>
    /// Implementation for Domain Attribute Alias Key
    /// </summary>
    public class DomainAttributeAliasKey : DomainEntityAliasKey, IDomainAttributeAliasKey, IKeyComparable<IDomainAttributeAliasKey>
    {
        /// <inheritdoc/>
        public string ElementName { get; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Attribute Alias
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeAliasKey(IDomainAttributeAliasKey source) : base(source)
        { if (source.ElementName is string) { ElementName = source.ElementName; } }

        /// <summary>
        /// Constructor for the Domain Attribute Alias
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeAliasKey(IDbTableColumnKey source) : base(source)
        { if (source.ColumnName is string) { ElementName = source.ColumnName; } }

        /// <summary>
        /// Constructor for the Domain Attribute Alias
        /// </summary>
        /// <param name="source"></param>
        public DomainAttributeAliasKey(IDbRoutineParameterKey source) : base(source)
        { if (source.ParameterName is string) { ElementName = source.ParameterName; } }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDomainAttributeAliasKey? other)
        {
            return 
                other is IDomainEntityAliasKey &&
                new DomainEntityAliasKey(this).Equals(other) &&
                !string.IsNullOrEmpty(ElementName) &&
                !string.IsNullOrEmpty(other.ElementName) &&
                ElementName.Equals(other.ElementName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDomainAttributeAliasKey value && Equals(new DomainAttributeAliasKey(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDomainAttributeAliasKey? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKey(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ElementName, other.ElementName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDomainAttributeAliasKey value) { return CompareTo(new DomainAttributeAliasKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DomainAttributeAliasKey left, DomainAttributeAliasKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(CatalogName, SchemaName, ObjectName, ElementName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            if (ElementName is string)
            { return string.Format("{0}.{1}", base.ToString(), ElementName); }
            else { return string.Empty; }
        }
    }
}
