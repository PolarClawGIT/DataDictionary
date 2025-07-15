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
    public interface IScriptingNodeIndexName : IScriptingNodeKeyName
    { }

    /// <inheritdoc/>
    public class ScriptingNodeIndexName : ScriptingNodeKeyName, IScriptingNodeIndexName,
        IKeyEquality<IScriptingNodeIndexName>, IKeyEquality<ScriptingNodeIndexName>
    {
        /// <inheritdoc cref="ScriptingNodeKeyName()"/>
        public ScriptingNodeIndexName() : base() { }

        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public ScriptingNodeIndexName(IScriptingNodeIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IScriptingNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ScriptingNodeIndexName? other)
        { return other is IScriptingNodeKeyName key && Equals(new ScriptingNodeKeyName(key)); }
    }
}
