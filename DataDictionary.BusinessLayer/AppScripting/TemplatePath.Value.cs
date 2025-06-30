using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplatePathValue : IScriptingPathItem, ITemplateIndex
    { }

    /// <inheritdoc/>
    public class TemplatePathValue : ScriptingPathItem, ITemplatePathValue
    {
        /// <inheritdoc/>
        public TemplatePathValue() : base() { }

        /// <summary>
        /// The Template Path derived from PathName
        /// </summary>
        public PathIndex Path
        {
            get { return new PathIndex(PathIndex.Parse(NameSpace).ToArray()); }
            set { NameSpace = value.MemberFullPath; }
        }

        /// <inheritdoc cref="ScriptingPathItem(IScriptingTemplateKey)"/>
        public TemplatePathValue(ITemplateIndex source) : base(source)
        { }
    }
}
