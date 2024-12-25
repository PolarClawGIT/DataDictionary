using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeIndexName : IAttributeKeyName
    { }

    /// <inheritdoc/>
    public class AttributeIndexName : AttributeKeyName, IAttributeIndexName,
        IKeyEquality<IAttributeIndexName>, IKeyEquality<AttributeIndexName>
    {
        /// <inheritdoc cref="AttributeKeyName(IAttributeKeyName)"/>
        public AttributeIndexName(IAttributeIndexName source) : base(source) { }

        /// <inheritdoc cref="AttributeKeyName(ITableColumnKeyName)"/>
        internal AttributeIndexName(ITableColumnIndexName source) : base(source) { }

        /// <inheritdoc cref="AttributeKeyName(IRoutineParameterKeyName)"/>
        internal AttributeIndexName(IRoutineParameterIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IAttributeIndexName? other)
        { return other is IAttributeKeyName value && Equals(new AttributeKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(AttributeIndexName? other)
        { return other is IAttributeKeyName value && Equals(new AttributeKeyName(value)); }

        /// <summary>
        /// Convert AttributeIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(AttributeIndexName source)
        { return new DataIndexName() { Title = source.AttributeTitle ?? String.Empty }; }
    }
}
