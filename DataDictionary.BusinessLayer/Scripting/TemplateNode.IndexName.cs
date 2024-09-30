using DataDictionary.DataLayer.ScriptingData;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeIndexName : IScriptingNodeKeyName
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndexName : ScriptingNodeKeyName, ITemplateNodeIndexName,
        IKeyEquality<ITemplateNodeIndexName>, IKeyEquality<TemplateNodeIndexName>
    {
        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public TemplateNodeIndexName(ITemplateNodeIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }
    }
}
