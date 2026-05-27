using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    [Obsolete("Merged to Schema Document")]
    public interface ITemplateObjectNameIndex : ITemplateObjectKeyName
    { }

    /// <inheritdoc/>
    [Obsolete("Merged to Schema Document")]
    public class TemplateObjectNameIndex : TemplateObjectKeyName, ITemplateObjectNameIndex,
        IKeyEquality<TemplateObjectNameIndex>,
        IKeyEquality<ITemplateObjectNameIndex>
    {
        /// <inheritdoc cref="TemplateObjectKeyName(ITemplateObjectKeyName)"/>
        public TemplateObjectNameIndex(ITemplateObjectNameIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateObjectNameIndex? other)
        { return other is ITemplateObjectKeyName value && Equals(new TemplateObjectKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateObjectNameIndex? other)
        { return other is ITemplateObjectKeyName value && Equals(new TemplateObjectKeyName(value)); }
    }
}
