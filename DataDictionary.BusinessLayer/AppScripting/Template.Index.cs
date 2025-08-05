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
    public interface ITemplateIndex : ITemplateKey
    { }

    /// <inheritdoc/>
    public class TemplateIndex : TemplateKey, ITemplateIndex,
        IKeyEquality<ITemplateIndex>, IKeyEquality<TemplateIndex>
    {
        /// <inheritdoc cref="TemplateKey(ITemplateKey)"/>
        public TemplateIndex(ITemplateIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateIndex? other)
        { return other is ITemplateKey key && Equals(new TemplateKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateIndex? other)
        { return other is ITemplateKey key && Equals(new TemplateKey(key)); }

        /// <summary>
        /// Convert TemplateIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TemplateIndex source)
        { return new DataIndex() { SystemId = source.TemplateId ?? Guid.Empty }; }
    }
}
