using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityAliasValue : IEntityAliasItem,
        IEntityIndex, IAliasIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    public class EntityAliasValue : EntityAliasItem, IEntityAliasValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntityAlias; } }

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
        public new PathIndex AliasPath
        {
            get { return new PathIndex(PathIndex.Parse(base.AliasPath).ToArray()); }
            set { base.AliasPath = value.MemberFullPath; }
        }


    }
}
