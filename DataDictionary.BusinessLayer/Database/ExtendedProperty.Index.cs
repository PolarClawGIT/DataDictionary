using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IExtendedPropertyIndexName : IDbExtendedPropertyKeyName
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyIndexName : DbExtendedPropertyKeyName, IDbExtendedPropertyKeyName
    {
        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbExtendedPropertyKeyName)"/>
        public ExtendedPropertyIndexName(IExtendedPropertyIndexName source): base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbTableKeyName)"/>
        public ExtendedPropertyIndexName(ITableKeyName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbTableColumnKeyName)"/>
        public ExtendedPropertyIndexName(ITableColumnKeyName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbRoutineKeyName)"/>
        public ExtendedPropertyIndexName(IRoutineIndexName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbRoutineParameterKeyName)"/>
        public ExtendedPropertyIndexName(IRoutineParameterIndexName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbConstraintKeyName)"/>
        public ExtendedPropertyIndexName(IConstraintIndexName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbSchemaKeyName)"/>
        public ExtendedPropertyIndexName(ISchemaIndexName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbDomainKeyName)"/>
        public ExtendedPropertyIndexName(IDomainIndexName source) : base(source)
        { }
    }
}
