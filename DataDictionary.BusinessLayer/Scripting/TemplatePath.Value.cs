using DataDictionary.BusinessLayer.NamedScope;
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
        public NamedScopePath Path
        {
            get { return new NamedScopePath(NamedScopePath.Parse(PathName).ToArray()); }
            set { PathName = value.MemberFullPath; }
        }

        /// <inheritdoc cref="ScriptingPathItem(IScriptingTemplateKey)"/>
        public TemplatePathValue(ITemplateIndex source) : base(source)
        { }
    }
}
