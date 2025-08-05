using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateAttributeIndex : ITemplateAttributeKey
    { }

    /// <inheritdoc/>
    public class TemplateAttributeIndex : TemplateAttributeKey, ITemplateAttributeIndex,
        IKeyEquality<ITemplateAttributeIndex>, IKeyEquality<TemplateAttributeIndex>
    {
        /// <inheritdoc cref="TemplateAttributeKey(ITemplateAttributeKey)"/>
        public TemplateAttributeIndex(ITemplateAttributeIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateAttributeIndex? other)
        { return other is ITemplateAttributeIndex key && Equals(new TemplateAttributeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateAttributeIndex? other)
        { return other is ITemplateAttributeIndex key && Equals(new TemplateAttributeKey(key)); }

        /// <summary>
        /// Convert TemplateIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TemplateAttributeIndex source)
        { return new DataIndex() { SystemId = source.AttributeId ?? Guid.Empty }; }
    }
}
