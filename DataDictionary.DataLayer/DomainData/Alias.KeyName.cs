using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    /// <summary>
    /// Interface for the Name part of the Key used by Domain Aliases
    /// </summary>
    public interface IAliasKeyName : IKey
    {
        /// <summary>
        /// Name of the Alias.
        /// </summary>
        String? AliasName { get; }
    }

    /// <summary>
    /// Implement Name part of the Key used by Domain Aliases
    /// </summary>
    public class AliasKeyName : IAliasKeyName,
        IKeyEquality<IAliasKeyName>, IKeyEquality<AliasKeyName>
    {
        /// <inheritdoc/>
        public String AliasName { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Alias Name Key
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(IAliasKeyName source) : base()
        {
            if (source.AliasName is string) { AliasName = source.AliasName; }
            else { AliasName = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from TableColumn
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(IDbTableColumnKeyName source) : base()
        { AliasName = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName, source.ColumnName); }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Table
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(IDbTableKeyName source) : base()
        { AliasName = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName); }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(AliasKeyName? other)
        {
            return
                other is AliasKeyName &&
                !string.IsNullOrEmpty(AliasName) &&
                !string.IsNullOrEmpty(other.AliasName) &&
                AliasName.Equals(other.AliasName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IAliasKeyName? other)
        { return other is IAliasKeyName value && Equals(new AliasKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IAliasKeyName value && Equals(new AliasKeyName(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(AliasKeyName left, AliasKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(AliasKeyName left, AliasKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(AliasName); }
        #endregion
    }
}
