using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    /// <inheritdoc/>
    public interface IEntityIndexName : IDomainEntityKeyName
    { }

    /// <inheritdoc/>
    public class EntityIndexName : DomainEntityKeyName
    {
        /// <inheritdoc cref="DomainEntityKeyName(IDomainEntityKeyName)"/>
        public EntityIndexName(IEntityIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainEntityKeyName(DataLayer.DatabaseData.Table.IDbTableKeyName)"/>
        internal EntityIndexName(ITableIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainEntityKeyName(DataLayer.DatabaseData.Routine.IDbRoutineKeyName)"/>
        internal EntityIndexName(IRoutineIndexName source) : base(source) { }
    }
}
