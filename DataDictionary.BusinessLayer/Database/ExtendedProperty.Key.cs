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
    public interface IExtendedPropertyKeyName : IDbExtendedPropertyKeyName
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyKeyName : DbExtendedPropertyKeyName, IDbExtendedPropertyKeyName
    {
        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbExtendedPropertyKeyName)"/>
        public ExtendedPropertyKeyName(IExtendedPropertyKeyName source): base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbTableKeyName)"/>
        public ExtendedPropertyKeyName(ITableKeyName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbTableColumnKeyName)"/>
        public ExtendedPropertyKeyName(ITableColumnKeyName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbRoutineKeyName)"/>
        public ExtendedPropertyKeyName(IRoutineKeyName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbRoutineParameterKeyName)"/>
        public ExtendedPropertyKeyName(IRoutineParameterKeyName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbConstraintKeyName)"/>
        public ExtendedPropertyKeyName(IConstraintIndexName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbSchemaKeyName)"/>
        public ExtendedPropertyKeyName(ISchemaKeyName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbDomainKeyName)"/>
        public ExtendedPropertyKeyName(IDomainKeyName source) : base(source)
        { }
    }
}
