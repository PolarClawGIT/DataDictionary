using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ScriptingData;
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

        /// <summary>
        /// The Template Path derived from PathName
        /// </summary>
        public PathIndex Path
        {
            get { return new PathIndex(PathIndex.Parse(PathName).ToArray()); }
            set { PathName = value.MemberFullPath; }
        }

        /// <inheritdoc cref="ScriptingPathItem(IScriptingTemplateKey)"/>
        public TemplatePathValue(ITemplateIndex source) : base(source)
        { }
    }
}
