using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DomainData.Entity;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityAliasValue : IDomainEntityAliasItem, 
        IEntityIndex, IAliasIndex
    { }

    /// <inheritdoc/>
    public class EntityAliasValue : DomainEntityAliasItem, IEntityAliasValue
    {
        /// <inheritdoc/>
        public EntityAliasValue() : base() { }

        /// <inheritdoc cref="DomainEntityAliasItem(IDomainEntityKey)"/>
        public EntityAliasValue(IEntityIndex key) : base(key) { }

        /// <summary>
        /// Create an Entity Alias form Entity and Alias.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alias"></param>
        public EntityAliasValue(IEntityIndex key, AliasIndex alias) : base(key)
        {
            AliasName = alias.AliasName;
            AliasScope = alias.AliasScope;
        }

        /// <inheritdoc/>
        internal EntityAliasValue(IDomainEntityKey key) : base(key) { }

        /// <summary>
        /// The Alias Path derived from AliasName
        /// </summary>
        public NamedScopePath AliasPath
        {
            get { return new NamedScopePath(NamedScopePath.Parse(AliasName).ToArray()); }
            set { AliasName = value.MemberFullPath; }
        }
    }
}
