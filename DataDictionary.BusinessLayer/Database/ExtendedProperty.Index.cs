using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IExtendedPropertyIndexName : IDbExtendedPropertyKeyName
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyIndexName : DbExtendedPropertyKeyName, IDbExtendedPropertyKeyName,
        IKeyEquality<IExtendedPropertyIndexName>, IKeyEquality<ExtendedPropertyIndexName>
    {
        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbExtendedPropertyKeyName)"/>
        public ExtendedPropertyIndexName(IExtendedPropertyIndexName source): base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbTableKeyName)"/>
        public ExtendedPropertyIndexName(ITableIndexName source) : base(source)
        { }

        /// <inheritdoc cref="DbExtendedPropertyKeyName(IDbTableColumnKeyName)"/>
        public ExtendedPropertyIndexName(ITableColumnIndexName source) : base(source)
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

        /// <inheritdoc/>
        public Boolean Equals(IExtendedPropertyIndexName? other)
        { return other is IDbExtendedPropertyKeyName key && Equals(new DbExtendedPropertyKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ExtendedPropertyIndexName? other)
        { return other is IDbExtendedPropertyKeyName key && Equals(new DbExtendedPropertyKeyName(key)); }
    }
}
