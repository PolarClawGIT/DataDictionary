using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeAliasValue : IAttributeAliasItem,
        IAttributeIndex, IAliasIndex, IAliasSubType,
        IScopeType
    {
        /// <summary>
        /// Attribute Alias Name returned as parts.
        /// </summary>
        public List<String> AliasParts { get; }
    }

    /// <inheritdoc/>
    public partial class AttributeAliasValue : AttributeAliasItem, IAttributeAliasValue
    {
        /// <inheritdoc/>
        public List<String> AliasParts { get { return PathIndex.Parse(base.AliasPath); } }

        /// <inheritdoc/>
        public AttributeAliasValue() : base() { }

        /// <inheritdoc cref="AttributeAliasItem(IAttributeKey)"/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeAlias; } }

        /// <summary>
        /// Create Attribute Alias from Attribute and Alias.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alias"></param>
        public AttributeAliasValue(IAttributeIndex key, IAliasIndex alias) : base(key)
        {
            base.AliasPath = alias.AliasPath;
            AliasScope = alias.AliasScope;
        }

        /// <inheritdoc/>
        internal AttributeAliasValue(IAttributeKey key) : base(key) { }

        /// <inheritdoc/>
        public new PathIndex AliasPath
        {
            get
            {
                // Changing the property in the base class is not always caught by the OnPropertyChanged.
                // Extra code is needed to check if the data has changed and update the backing field.  
                if (!aliasPathValue.MemberFullPath.Equals(base.AliasPath))
                { aliasPathValue = new PathIndex(PathIndex.Parse(base.AliasPath).ToArray()); }

                return aliasPathValue;
            }
            set
            {
                base.AliasPath = value.MemberFullPath;
                aliasPathValue.Set(value);
                OnPropertyChanged(nameof(base.AliasPath));
            }
        }
        PathIndex aliasPathValue = new PathIndex();
    }
}
