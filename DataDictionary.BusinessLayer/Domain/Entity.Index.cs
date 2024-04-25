using DataDictionary.DataLayer.DomainData.Entity;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityIndex : IDomainEntityKey
    { }

    /// <inheritdoc/>
    public class EntityIndex : DomainEntityKey, IEntityIndex
    {
        /// <inheritdoc cref="DomainEntityKey(IDomainEntityKey)"/>
        public EntityIndex(IEntityIndex source) : base(source) { }

        /// <inheritdoc/>
        public EntityIndex(IDomainEntityKey source) : base(source) { }
    }
}
