using DataDictionary.DataLayer.ScriptingData.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateAttributeValue : IScriptingAttributeItem, ITemplateIndex
    { }

    /// <inheritdoc/>
    public class TemplateAttributeValue : ScriptingAttributeItem, ITemplateAttributeValue
    {
        /// <inheritdoc/>
        public TemplateAttributeValue() : base() { }

        /// <inheritdoc cref="ScriptingAttributeItem(IScriptingNodeKeyComposite)"/>
        public TemplateAttributeValue(ITemplateNodeIndex source) : base(source)
        { }
    }
}
