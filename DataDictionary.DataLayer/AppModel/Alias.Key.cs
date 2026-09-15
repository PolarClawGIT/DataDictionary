using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Key used by Model Aliases
    /// </summary>
    public interface IAliasKey : IKey
    {
        /// <summary>
        /// Application Scope of the Alias.
        /// </summary>
        ScopeType AliasScope { get; }

        /// <summary>
        /// Name of the Alias.
        /// </summary>
        String? AliasPath { get; }
    }

    /// <summary>
    /// Implementation of the Key used by Model Aliases
    /// </summary>
    public class AliasKey : IAliasKey,
        IKeyEquality<IAliasKey>, IKeyEquality<AliasKey>
    {
        /// <inheritdoc/>
        public ScopeType AliasScope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public String? AliasPath { get; init; } = string.Empty;

        /// <inheritdoc/>
        public Boolean HasValue { get { return !String.IsNullOrEmpty(AliasPath) && AliasScope != ScopeType.Null; } }

        /// <summary>
        /// Constructor for the Key used by Aliases
        /// </summary>
        /// <param name="source"></param>
        public AliasKey(IAliasKey source) : base()
        {
            AliasScope = source.AliasScope;
            AliasPath = source.AliasPath;
        }

        /// <summary>
        /// Constructor for the Key used by Aliases given a TableColumn
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scope"></param>
        protected AliasKey(ITableColumnKeyName source, ScopeType scope) : base()
        {
            AliasScope = scope;
            AliasPath = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName, source.ColumnName);
        }

        /// <summary>
        /// Constructor for the Key used by Aliases given a Table
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scope"></param>
        protected AliasKey(ITableKeyName source, ScopeType scope) : base()
        {
            AliasScope = scope;
            AliasPath = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName);
        }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Attribute Alias
        /// </summary>
        /// <param name="alias"></param>
        public AliasKey(AppModel.IAttributeAliasItem alias) : base()
        {
            AliasScope = alias.AliasScope;
            AliasPath = alias.AliasPath ?? String.Empty;
        }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Entity Alias
        /// </summary>
        /// <param name="alias"></param>
        public AliasKey(AppModel.IEntityAliasItem alias) : base()
        {
            AliasScope = alias.AliasScope;
            AliasPath = alias.AliasPath ?? String.Empty;
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(AliasKey? other)
        {
            return
                other is AliasKey key
                && this.HasValue
                && key.HasValue
                && !String.IsNullOrEmpty(AliasPath)
                && !String.IsNullOrEmpty(other.AliasPath)
                && AliasPath.Equals(other.AliasPath, KeyExtension.CompareString);
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
