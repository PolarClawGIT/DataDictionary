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
    [Obsolete("replace", true)]
    public interface IScriptingPathValue : IScriptingPathItem, IScriptingTemplateIndex
    { }

    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public class ScriptingPathValue : ScriptingPathItem, IScriptingPathValue
    {
        /// <inheritdoc/>
        public ScriptingPathValue() : base() { }

        /// <summary>
        /// The Template Path derived from PathName
        /// </summary>
        public PathIndex Path
        {
            get { return new PathIndex(PathIndex.Parse(NameSpace).ToArray()); }
            set { NameSpace = value.MemberFullPath; }
        }

        /// <inheritdoc cref="ScriptingPathItem(IScriptingTemplateKey)"/>
        public ScriptingPathValue(IScriptingTemplateIndex source) : base(source)
        { }
    }
}
