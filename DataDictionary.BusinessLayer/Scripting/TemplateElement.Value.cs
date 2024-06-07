using DataDictionary.DataLayer.ScriptingData.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateElementValue: IScriptingElementItem, ITemplateIndex
    { }

    /// <inheritdoc/>
    public class TemplateElementValue : ScriptingElementItem, ITemplateElementValue
    {
        /// <inheritdoc/>
        public TemplateElementValue() : base() { }

        /// <inheritdoc cref="ScriptingElementItem(IScriptingTemplateKey)"/>
        public TemplateElementValue(ITemplateIndex source) : base(source)
        { }
    }
}
