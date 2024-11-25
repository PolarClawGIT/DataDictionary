using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IExtendedPropertyIndexName : IPropertyKeyName
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyIndexName : PropertyKeyName, IPropertyKeyName,
        IKeyEquality<IExtendedPropertyIndexName>, IKeyEquality<ExtendedPropertyIndexName>
    {
        /// <inheritdoc cref="PropertyKeyName(IPropertyKeyName)"/>
        public ExtendedPropertyIndexName(IExtendedPropertyIndexName source): base(source)
        { }

        /// <inheritdoc cref="PropertyKeyName(IDbTableKeyName)"/>
        public ExtendedPropertyIndexName(ITableIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyName(IDbTableColumnKeyName)"/>
        public ExtendedPropertyIndexName(ITableColumnIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyName(IDbRoutineKeyName)"/>
        public ExtendedPropertyIndexName(IRoutineIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyName(IDbRoutineParameterKeyName)"/>
        public ExtendedPropertyIndexName(IRoutineParameterIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyName(IDbConstraintKeyName)"/>
        public ExtendedPropertyIndexName(IConstraintIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyName(ISchemaKeyName)"/>
        public ExtendedPropertyIndexName(ISchemaIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyName(IDomainKeyName)"/>
        public ExtendedPropertyIndexName(IDomainIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IExtendedPropertyIndexName? other)
        { return other is IPropertyKeyName key && Equals(new PropertyKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ExtendedPropertyIndexName? other)
        { return other is IPropertyKeyName key && Equals(new PropertyKeyName(key)); }
    }
}
