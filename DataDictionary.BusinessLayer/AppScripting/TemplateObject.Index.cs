using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    [Obsolete("Merged to Schema Document")]
    public interface ITemplateObjectIndex : ITemplateObjectKey
    { }

    /// <inheritdoc/>
    [Obsolete("Merged to Schema Document")]
    public class TemplateObjectIndex : TemplateObjectKey, ITemplateObjectIndex,
        IKeyEquality<ITemplateObjectIndex>, IKeyEquality<TemplateObjectIndex>
    {
        /// <inheritdoc cref="TemplateObjectKey()"/>
        public TemplateObjectIndex() : base() { }

        /// <inheritdoc cref="TemplateObjectKey(ITemplateObjectKey)"/>
        public TemplateObjectIndex(ITemplateObjectIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateObjectIndex? other)
        { return other is ITemplateObjectKey key && Equals(new TemplateObjectKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateObjectIndex? other)
        { return other is ITemplateObjectKey key && Equals(new TemplateObjectKey(key)); }

        /// <summary>
        /// Convert TemplateObjectIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TemplateObjectIndex source)
        { return new DataIndex() { SystemId = source.ObjectId ?? Guid.Empty }; }
    }
}
