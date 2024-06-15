using DataDictionary.DataLayer.ScriptingData.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeValue : IScriptingNodeItem, ITemplateIndex, ITemplateNodeIndex
    { }

    /// <inheritdoc/>
    public class TemplateNodeValue : ScriptingNodeItem, ITemplateNodeValue
    {
        /// <inheritdoc/>
        public TemplateNodeValue() : base() { }

        /// <inheritdoc cref="ScriptingNodeItem(IScriptingTemplateKey)"/>
        public TemplateNodeValue(ITemplateIndex source) : base(source)
        { }
    }
}
