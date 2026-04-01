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
    public interface ITemplateNodeOwnerIndex : ITemplateNodeOwnerKey
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class TemplateNodeOwnerIndex : TemplateNodeOwnerKey, ITemplateNodeOwnerIndex,
        IKeyEquality<ITemplateNodeOwnerIndex>, IKeyEquality<TemplateNodeOwnerIndex>
    {
        /// <inheritdoc cref="TemplateNodeOwnerKey()"/>
        public TemplateNodeOwnerIndex() : base()
        { }

        /// <inheritdoc cref="TemplateNodeOwnerKey(ITemplateNodeOwnerKey)"/>
        public TemplateNodeOwnerIndex(ITemplateNodeOwnerIndex source) : base(source)
        { }

        /// <inheritdoc cref="TemplateNodeOwnerKey(ITemplateNodeKey)"/>
        public TemplateNodeOwnerIndex(ITemplateNodeIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeOwnerIndex? other)
        { return other is ITemplateNodeOwnerKey key && Equals(new TemplateNodeOwnerKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeOwnerIndex? other)
        { return other is ITemplateNodeOwnerKey key && Equals(new TemplateNodeOwnerKey(key)); }
    }
}
