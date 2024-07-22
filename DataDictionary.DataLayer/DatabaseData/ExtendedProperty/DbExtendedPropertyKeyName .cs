using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Interface for the Database Extended Property Name Key
    /// </summary>
    public interface IDbExtendedPropertyKeyName : IKey, IDbCatalogKeyName
    {
        /// <summary>
        /// Level 0 (Catalog) Name parameter
        /// </summary>
        String? Level0Name { get; }

        /// <summary>
        /// Level 1 (Object) Name parameter
        /// </summary>
        String? Level1Name { get; }

        /// <summary>
        /// Level 2 (Element) Name parameter
        /// </summary>
        String? Level2Name { get; }

    }

    /// <summary>
    /// Implementation for the Database Extended Property Name Key
    /// </summary>
    public class DbExtendedPropertyKeyName : DbCatalogKeyName, IDbExtendedPropertyKeyName, IKeyComparable<IDbExtendedPropertyKeyName>
    {
        /// <inheritdoc/>
        public String Level0Name { get; init; } = string.Empty;

        /// <inheritdoc/>
        public String Level1Name { get; init; } = string.Empty;

        /// <inheritdoc/>
        public String Level2Name { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbExtendedPropertyKeyName source) : base(source)
        {
            if (!String.IsNullOrWhiteSpace(source.Level0Name)) { Level0Name = source.Level0Name; }
            if (!String.IsNullOrWhiteSpace(source.Level1Name)) { Level1Name = source.Level1Name; }
            if (!String.IsNullOrWhiteSpace(source.Level2Name)) { Level2Name = source.Level2Name; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbTableKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.TableName is String) { Level1Name = source.TableName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbTableColumnKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.TableName is String) { Level1Name = source.TableName; }
            if (source.ColumnName is String) { Level2Name = source.ColumnName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbRoutineKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.RoutineName is String) { Level1Name = source.RoutineName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbRoutineParameterKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.RoutineName is String) { Level1Name = source.RoutineName; }
            if (source.ParameterName is String) { Level2Name = source.ParameterName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbConstraintKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.ConstraintName is String) { Level1Name = source.ConstraintName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbSchemaKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
        }

        /// <summary>
        /// Constructor for the Database Extended Property Name Key
        /// </summary>
        /// <param name="source"></param>
        public DbExtendedPropertyKeyName(IDbDomainKeyName source) : base(source)
        {
            if (source.SchemaName is String) { Level0Name = source.SchemaName; }
            if (source.DomainName is String) { Level1Name = source.DomainName; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbExtendedPropertyKeyName? other)
        {
            return
                other is IDbExtendedPropertyKeyName &&
                new DbCatalogKeyName(this).Equals(other) &&
                ((String.IsNullOrWhiteSpace(Level0Name) &&
                  String.IsNullOrWhiteSpace(other.Level0Name)) ||
                  Level0Name.Equals(other.Level0Name, KeyExtension.CompareString)) &&
                ((String.IsNullOrWhiteSpace(Level1Name) &&
                  String.IsNullOrWhiteSpace(other.Level1Name)) ||
                  Level1Name.Equals(other.Level1Name, KeyExtension.CompareString)) &&
                ((String.IsNullOrWhiteSpace(Level2Name) &&
                  String.IsNullOrWhiteSpace(other.Level2Name)) ||
                  Level2Name.Equals(other.Level2Name, KeyExtension.CompareString));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbExtendedPropertyKeyName value && Equals(new DbExtendedPropertyKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbExtendedPropertyKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else if (string.Compare(Level0Name, other.Level0Name, true) is int level0 && level0 != 0) { return level0; }
            else if (string.Compare(Level1Name, other.Level1Name, true) is int level1 && level0 != 0) { return level1; }
            { return string.Compare(Level2Name, other.Level2Name, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbExtendedPropertyKeyName value) { return CompareTo(new DbExtendedPropertyKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbExtendedPropertyKeyName left, DbExtendedPropertyKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbExtendedPropertyKeyName left, DbExtendedPropertyKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbExtendedPropertyKeyName left, DbExtendedPropertyKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbExtendedPropertyKeyName left, DbExtendedPropertyKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbExtendedPropertyKeyName left, DbExtendedPropertyKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbExtendedPropertyKeyName left, DbExtendedPropertyKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(
            base.GetHashCode(),
            Level0Name.GetHashCode(KeyExtension.CompareString),
            Level1Name.GetHashCode(KeyExtension.CompareString),
            Level2Name.GetHashCode(KeyExtension.CompareString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            String result = base.ToString();
            if (!String.IsNullOrWhiteSpace(Level0Name)) { result = String.Format("{0}.{1}", result, Level0Name); }
            if (!String.IsNullOrWhiteSpace(Level1Name)) { result = String.Format("{0}.{1}", result, Level1Name); }
            if (!String.IsNullOrWhiteSpace(Level2Name)) { result = String.Format("{0}.{1}", result, Level2Name); }

            return result;
        }
    }
}
