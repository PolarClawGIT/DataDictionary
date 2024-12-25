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
        String? AliasNameSpace { get; }
    }

    /// <summary>
    /// Implement Name part of the Key used by Domain Aliases
    /// </summary>
    public class AliasKeyName : IAliasKeyName,
        IKeyEquality<IAliasKeyName>, IKeyEquality<AliasKeyName>
    {
        /// <inheritdoc/>
        public String AliasNameSpace { get; init; } = string.Empty;

        /// <summary>
        /// Constructor for the Domain Alias Name Key
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(IAliasKeyName source) : base()
        {
            if (source.AliasNameSpace is string) { AliasNameSpace = source.AliasNameSpace; }
            else { AliasNameSpace = string.Empty; }
        }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from TableColumn
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(ITableColumnKeyName source) : base()
        { AliasNameSpace = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName, source.ColumnName); }

        /// <summary>
        /// Constructor for the Domain Alias Name Key from Table
        /// </summary>
        /// <param name="source"></param>
        public AliasKeyName(ITableKeyName source) : base()
        { AliasNameSpace = DbObjectName.Format(source.DatabaseName, source.SchemaName, source.TableName); }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(AliasKeyName? other)
        {
            return
                other is AliasKeyName &&
                !string.IsNullOrEmpty(AliasNameSpace) &&
                !string.IsNullOrEmpty(other.AliasNameSpace) &&
                AliasNameSpace.Equals(other.AliasNameSpace, KeyExtension.CompareString);
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
        { return HashCode.Combine(AliasNameSpace); }
        #endregion
    }
}
