using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateNodeIndex: ITemplateNodeKey
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class TemplateNodeIndex : TemplateNodeKey, ITemplateNodeIndex,
        IKeyEquality<ITemplateNodeIndex>, IKeyEquality<TemplateNodeIndex>
    {
        /// <inheritdoc cref="TemplateNodeKey()"/>
        public TemplateNodeIndex() : base()
        { }

        /// <inheritdoc cref="TemplateNodeKey(ITemplateNodeKey)"/>
        public TemplateNodeIndex(ITemplateNodeIndex source) : base(source)
        { }

        /// <inheritdoc cref="TemplateNodeKey(ITemplateNodeOwnerKey)"/>
        public TemplateNodeIndex(ITemplateNodeOwnerIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeIndex? other)
        { return other is ITemplateNodeKey key && Equals(new TemplateNodeKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeIndex? other)
        { return other is ITemplateNodeKey key && Equals(new TemplateNodeKey(key)); }

        /// <summary>
        /// Convert TemplateIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(TemplateNodeIndex source)
        { return new DataIndex() { SystemId = source.NodeId ?? Guid.Empty }; }
    }
}
