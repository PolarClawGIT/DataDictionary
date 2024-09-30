using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
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

        /// <summary>
        /// Convert EntityIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(EntityIndexName source)
        { return new DataIndexName() { Title = source.EntityTitle ?? String.Empty }; }
    }
}
