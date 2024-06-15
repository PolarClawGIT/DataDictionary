using DataDictionary.DataLayer.ScriptingData.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeIndex : IScriptingNodeKeyComposite
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndex : ScriptingNodeKeyComposite
    {
        /// <inheritdoc cref="ScriptingNodeKeyComposite(IScriptingNodeKeyComposite)"/>
        public TemplateNodeIndex(ITemplateNodeIndex source) : base(source)
        { }
    }
}
