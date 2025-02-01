using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeIndex : IAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeIndex : AttributeKey, IAttributeIndex,
        IKeyEquality<IAttributeIndex>, IKeyEquality<AttributeIndex>
    {
        /// <inheritdoc cref="AttributeKey()"/>
        public AttributeIndex() : base() { }

        /// <inheritdoc cref="AttributeKey(IAttributeKey)"/>
        public AttributeIndex(IAttributeIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(AttributeIndex? other)
        { return other is IAttributeKey key && Equals(new AttributeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAttributeIndex? other)
        { return other is IAttributeKey key && Equals(new AttributeKey(key)); }

        /// <summary>
        /// Convert AttributeIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(AttributeIndex source)
        { return new DataIndex() { SystemId = source.AttributeId ?? Guid.Empty }; }
    }
}
