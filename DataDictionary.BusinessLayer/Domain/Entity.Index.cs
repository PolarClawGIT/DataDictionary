using DataDictionary.BusinessLayer.Database;
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
        /// Convert EntityIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(EntityIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.EntityId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IEntityIndexName : IDomainEntityKeyName
    { }

    /// <inheritdoc/>
    public class EntityIndexName : DomainEntityKeyName, IEntityIndexName,
        IKeyEquality<IEntityIndexName>, IKeyEquality<EntityIndexName>
    {
        /// <inheritdoc cref="DomainEntityKeyName(IDomainEntityKeyName)"/>
        public EntityIndexName(IEntityIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainEntityKeyName(DataLayer.DatabaseData.Table.IDbTableKeyName)"/>
        internal EntityIndexName(ITableIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainEntityKeyName(DataLayer.DatabaseData.Routine.IDbRoutineKeyName)"/>
        internal EntityIndexName(IRoutineIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IEntityIndexName? other)
        { return other is IDomainEntityKeyName key && Equals(new DomainEntityKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(EntityIndexName? other)
        { return other is IDomainEntityKeyName key && Equals(new DomainEntityKeyName(key)); }
    }
}
