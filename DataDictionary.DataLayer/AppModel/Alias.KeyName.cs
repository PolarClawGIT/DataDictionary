using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Name part of the Key used by Model Aliases
    /// </summary>
    public interface IAliasKeyName : IKey
    {
        /// <summary>
        /// Name of the Alias.
        /// </summary>
        String? AliasPath { get; }
    }

    /// <summary>
    /// Implement Name part of the Key used by Domain Aliases
    /// </summary>
    public class AliasKeyName : IAliasKeyName,
        IKeyEquality<IAliasKeyName>, IKeyEquality<AliasKeyName>
    {
        /// <inheritdoc/>
        public String AliasPath { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Alias Name Key
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(IAliasKeyName source) : base()
        {
            if (source.AliasPath is string) { AliasPath = source.AliasPath; }
            else { AliasPath = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from TableColumn
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(ITableColumnKeyName source) : base()
        { AliasPath = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName, source.ColumnName); }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Table
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(ITableKeyName source) : base()
        { AliasPath = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName); }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Attribute Alias
        /// </summary>
        /// <param name="alias"></param>
        public AliasKeyName(AppModel.IAttributeAliasItem alias) : base()
        { AliasPath = alias.AliasPath ?? String.Empty; }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Entity Alias
        /// </summary>
        /// <param name="alias"></param>
        public AliasKeyName(AppModel.IEntityAliasItem alias):base()
        { AliasPath = alias.AliasPath ?? String.Empty; }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Entity Attribute
        /// </summary>
        /// <param name="alias"></param>
        public AliasKeyName(AppModel.IEntityAttributeItem alias) : base()
        { AliasPath = alias.AttributePath ?? String.Empty; }


        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(AliasKeyName? other)
        {
            return
                other is AliasKeyName &&
                !string.IsNullOrEmpty(AliasPath) &&
                !string.IsNullOrEmpty(other.AliasPath) &&
                AliasPath.Equals(other.AliasPath, KeyExtension.CompareString);
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
        { return HashCode.Combine(AliasPath); }
        #endregion
    }
}
