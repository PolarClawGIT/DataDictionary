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
    public interface ITemplateNodeOwnerIndex : ITemplateNodeOwnerKey
    { }

    /// <inheritdoc/>
    public class TemplateNodeOwnerIndex : TemplateNodeOwnerKey, ITemplateNodeOwnerIndex,
        IKeyEquality<ITemplateNodeOwnerIndex>, IKeyEquality<TemplateNodeOwnerIndex>
    {
        /// <inheritdoc cref="TemplateNodeOwnerKey()"/>
        public TemplateNodeOwnerIndex() : base()
        { }

        /// <inheritdoc cref="TemplateNodeOwnerKey(ITemplateNodeOwnerKey)"/>
        public TemplateNodeOwnerIndex(ITemplateNodeOwnerIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeOwnerIndex? other)
        { return other is ITemplateNodeOwnerKey key && Equals(new TemplateNodeOwnerKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeOwnerIndex? other)
        { return other is ITemplateNodeOwnerKey key && Equals(new TemplateNodeOwnerKey(key)); }
    }
}
