using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IPropertyIndexObject : IPropertyKeyObject
    { }

    /// <inheritdoc/>
    public class PropertyIndexObject : PropertyKeyObject, IPropertyKeyObject,
        IKeyEquality<IPropertyIndexObject>, IKeyEquality<PropertyIndexObject>
    {
        /// <inheritdoc cref="PropertyKeyObject(IPropertyKeyObject)"/>
        public PropertyIndexObject(IPropertyIndexObject source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(ITableKeyName)"/>
        public PropertyIndexObject(ITableIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(ITableColumnKeyName)"/>
        public PropertyIndexObject(ITableColumnIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IRoutineKeyName)"/>
        public PropertyIndexObject(IRoutineIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IRoutineParameterKeyName)"/>
        public PropertyIndexObject(IRoutineParameterIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDbConstraintKeyName)"/>
        public PropertyIndexObject(IConstraintIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(ISchemaKeyName)"/>
        public PropertyIndexObject(ISchemaIndexName source) : base(source)
        { }

        /// <inheritdoc cref="PropertyKeyObject(IDomainKeyName)"/>
        public PropertyIndexObject(IDomainIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyIndexObject? other)
        { return other is IPropertyKeyObject key && Equals(new PropertyKeyObject(key)); }

        /// <inheritdoc/>
        public Boolean Equals(PropertyIndexObject? other)
        { return other is IPropertyKeyObject key && Equals(new PropertyKeyObject(key)); }
    }
}
