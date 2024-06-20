using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.BusinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateAttributeValue : IScriptingAttributeItem, ITemplateIndex, ITemplateNodeIndex, IPropertyIndex
    { }

    /// <inheritdoc/>
    public class TemplateAttributeValue : ScriptingAttributeItem, ITemplateAttributeValue
    {
        /// <inheritdoc/>
        public TemplateAttributeValue() : base() { }

        /// <inheritdoc cref="ScriptingAttributeItem(IScriptingNodeKeyComposite)"/>
        public TemplateAttributeValue(IScriptingNodeKeyComposite source) : base(source)
        { }
    }
}
