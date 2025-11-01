using DataDictionary.DataLayer.AppScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateElementIndexParent : ITemplateElementKeyParent
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class TemplateElementIndexParent : TemplateElementKeyParent,
        ITemplateElementIndexParent, ITemplateElementIndex
    {
        /// <inheritdoc cref="TemplateElementKeyParent(ITemplateElementKeyParent)"/>
        public TemplateElementIndexParent(ITemplateElementIndexParent source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateElementIndexParent? other)
        { return other is ITemplateElementIndexParent key && Equals(new TemplateElementKeyParent(key)); }
    }
}
