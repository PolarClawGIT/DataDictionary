using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateElementIndex : ITemplateElementKey
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class TemplateElementIndex : TemplateElementKey, ITemplateElementIndex,
        IKeyEquality<ITemplateElementIndex>//, IKeyEquality<TemplateElementIndex>
    {
        /// <inheritdoc cref="TemplateElementKey(ITemplateElementKey)"/>
        public TemplateElementIndex(ITemplateElementIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateElementIndex? other)
        { return other is ITemplateElementIndex key && Equals(new TemplateElementKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateElementIndex? other)
        { return other is ITemplateElementIndex key && Equals(new TemplateElementKey(key)); }

        /// <summary>
        /// Convert TemplateIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TemplateElementIndex source)
        { return new DataIndex() { SystemId = source.ElementId ?? Guid.Empty }; }
    }
}
