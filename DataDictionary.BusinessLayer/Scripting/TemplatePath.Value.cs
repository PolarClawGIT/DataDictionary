using DataDictionary.DataLayer.ScriptingData.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplatePathValue : IScriptingPathItem, ITemplateIndex
    { }

    /// <inheritdoc/>
    public class TemplatePathValue : ScriptingPathItem, ITemplatePathValue
    {
        /// <inheritdoc/>
        public TemplatePathValue() : base() { }

        /// <inheritdoc cref="ScriptingPathItem(IScriptingTemplateKey)"/>
        public TemplatePathValue(ITemplateIndex source) : base(source)
        { }
    }
}
