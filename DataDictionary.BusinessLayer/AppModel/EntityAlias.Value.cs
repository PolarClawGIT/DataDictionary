using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAliasValue : IEntityAliasItem,
        IEntityIndex, IAliasIndex, IAliasSubType,
        IScopeType
    { }

    /// <inheritdoc/>
    public class EntityAliasValue : EntityAliasItem, IEntityAliasValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityAlias; } }

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

        /// <inheritdoc/>
        public EntityAliasValue() : base() { }

        /// <inheritdoc cref="EntityAliasItem(IEntityKey)"/>
        public EntityAliasValue(IEntityIndex key) : base(key) { }

        /// <summary>
        /// Create an Entity Alias form Entity and Alias.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alias"></param>
        public EntityAliasValue(IEntityIndex key, AliasIndex alias) : base(key)
        {
            base.AliasPath = alias.AliasPath;
            AliasScope = alias.AliasScope;
        }

        /// <inheritdoc/>
        internal EntityAliasValue(IEntityKey key) : base(key) { }

        /// <inheritdoc cref="EntityAliasItem.AliasPath"/>
        public PathIndex AliasName
        {
            get { return new PathIndex(PathIndex.Parse(base.AliasPath).ToArray()); }
            set { base.AliasPath = value.MemberFullPath; }
        }


    }
}
