using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityIndexName : IEntityKeyName
    { }

    /// <inheritdoc/>
    public class EntityIndexName : EntityKeyName, IEntityIndexName,
        IKeyEquality<IEntityIndexName>, IKeyEquality<EntityIndexName>
    {
        /// <inheritdoc cref="EntityKeyName(IEntityKeyName)"/>
        public EntityIndexName(IEntityIndexName source) : base(source) { }

        /// <inheritdoc cref="EntityKeyName(ITableKeyName)"/>
        internal EntityIndexName(ITableIndexName source) : base(source) { }

        /// <inheritdoc cref="EntityKeyName(IRoutineKeyName)"/>
        internal EntityIndexName(IRoutineIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IEntityIndexName? other)
        { return other is IEntityKeyName key && Equals(new EntityKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(EntityIndexName? other)
        { return other is IEntityKeyName key && Equals(new EntityKeyName(key)); }

        /// <summary>
        /// Convert EntityIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(EntityIndexName source)
        { return new DataIndexName() { Title = source.EntityTitle ?? String.Empty }; }
    }
}
