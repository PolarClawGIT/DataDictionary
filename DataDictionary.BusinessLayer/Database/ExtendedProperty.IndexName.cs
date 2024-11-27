using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IExtendedPropertyIndexName : IPropertyKeyObject
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyIndexName : PropertyKeyObject, IPropertyKeyObject,
        IKeyEquality<IExtendedPropertyIndexName>, IKeyEquality<ExtendedPropertyIndexName>
    {
        /// <inheritdoc cref="PropertyKeyObject(IPropertyKeyObject)"/>
        public ExtendedPropertyIndexName(IExtendedPropertyIndexName source): base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDbTableKeyName)"/>
        public ExtendedPropertyIndexName(ITableIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDbTableColumnKeyName)"/>
        public ExtendedPropertyIndexName(ITableColumnIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDbRoutineKeyName)"/>
        public ExtendedPropertyIndexName(IRoutineIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDbRoutineParameterKeyName)"/>
        public ExtendedPropertyIndexName(IRoutineParameterIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDbConstraintKeyName)"/>
        public ExtendedPropertyIndexName(IConstraintIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(ISchemaKeyName)"/>
        public ExtendedPropertyIndexName(ISchemaIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDomainKeyName)"/>
        public ExtendedPropertyIndexName(IDomainIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IExtendedPropertyIndexName? other)
        { return other is IPropertyKeyObject key && Equals(new PropertyKeyObject(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ExtendedPropertyIndexName? other)
        { return other is IPropertyKeyObject key && Equals(new PropertyKeyObject(key)); }
    }
}
