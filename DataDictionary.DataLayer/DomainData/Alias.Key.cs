using DataDictionary.DataLayer.DatabaseData;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    /// <summary>
    /// Interface for the Key used by Domain Aliases
    /// </summary>
    public interface IAliasKey : IAliasKeyName
    {
        /// <summary>
        /// Application Scope of the Alias.
        /// </summary>
        ScopeType AliasScope { get; }
    }

    /// <summary>
    /// Implementation of the Key used by Domain Aliases
    /// </summary>
    public class AliasKey : AliasKeyName, IAliasKey,
        IKeyEquality<IAliasKey>, IKeyEquality<AliasKey>
    {
        /// <inheritdoc/>
        public ScopeType AliasScope { get; init; } = ScopeType.Null;

        /// <summary>
        /// Constructor for the Key used by Domain Aliases
        /// </summary>
        /// <param name="source"></param>
        public AliasKey(IAliasKey source) : base(source)
        { AliasScope = source.AliasScope; }

        /// <summary>
        /// Constructor for the Key used by Domain Aliases from TableColumn
        /// </summary>
        /// <param name="source"></param>
        public AliasKey(IDbTableColumnItem source) : base(source)
        { AliasScope = source.Scope; }

        /// <summary>
        /// Constructor for the Key used by Domain Aliases from Table
        /// </summary>
        /// <param name="source"></param>
        public AliasKey(IDbTableItem source) : base(source)
        { AliasScope = source.Scope; }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(AliasKey? other)
        {
            return
                other is AliasKey &&
                new AliasKeyName(this).Equals(other) &&
                AliasScope is not ScopeType.Null &&
                other.AliasScope is not ScopeType.Null &&
                AliasScope == other.AliasScope;
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IAliasKey? other)
        { return other is IAliasKey value && Equals(new AliasKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? other)
        { return other is IAliasKey value && Equals(new AliasKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(AliasKey left, AliasKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(AliasKey left, AliasKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(AliasScope, AliasName); }
        #endregion
    }
}
