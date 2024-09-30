using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityIndex : IDomainEntityKey
    { }

    /// <inheritdoc/>
    public class EntityIndex : DomainEntityKey, IEntityIndex,
        IKeyEquality<IEntityIndex>, IKeyEquality<EntityIndex>
    {
        /// <inheritdoc cref="DomainEntityKey(IDomainEntityKey)"/>
        public EntityIndex(IEntityIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IEntityIndex? other)
        { return other is IDomainEntityKey key && Equals(new DomainEntityKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(EntityIndex? other)
        { return other is IDomainEntityKey key && Equals(new DomainEntityKey(key)); }

        /// <summary>
        /// Convert EntityIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(EntityIndex source)
        { return new DataIndex() { SystemId = source.EntityId ?? Guid.Empty }; }
    }


}
