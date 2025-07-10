using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
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

        public TemplateNodeIndexName(ScopeType scope, PropertyInfo property) : base()
        {
            PropertyScope = scope;
            PropertyName = property.Name;
        }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }
    }
}
