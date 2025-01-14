using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Key used by Model Aliases
    /// </summary>
    public interface IAliasKey : IAliasKeyName
    {
        /// <summary>
        /// Application Scope of the Alias.
        /// </summary>
        ScopeType AliasScope { get; }
    }

    /// <summary>
    /// Implementation of the Key used by Model Aliases
    /// </summary>
    public class AliasKey : AliasKeyName, IAliasKey,
        IKeyEquality<IAliasKey>, IKeyEquality<AliasKey>
    {
        /// <inheritdoc/>
        public ScopeType AliasScope { get; init; } = ScopeType.Null;

        /// <summary>
        /// Constructor for the Key used by Aliases
        /// </summary>
        /// <param name="source"></param>
        public AliasKey(IAliasKey source) : base(source)
        { AliasScope = source.AliasScope; }

        /// <summary>
        /// Constructor for the Key used by Aliases given a TableColumn
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scope"></param>
        protected AliasKey(ITableColumnKeyName source, ScopeType scope) : base(source)
        { AliasScope = scope; }

        /// <summary>
        /// Constructor for the Key used by Aliases given a Table
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scope"></param>
        protected AliasKey(ITableKeyName source, ScopeType scope) : base(source)
        { AliasScope = scope; }

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
        { return HashCode.Combine(AliasScope, AliasPath); }
        #endregion
    }
}
