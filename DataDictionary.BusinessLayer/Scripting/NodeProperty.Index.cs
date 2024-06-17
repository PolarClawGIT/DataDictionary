using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface for the Scripting Node Property key.
    /// </summary>
    public interface INodePropertyIndex : IScriptingNodeKeyName
    { }

    /// <summary>
    /// Implementation for the Scripting Schema Column key.
    /// </summary>
    public class NodePropertyIndex : ScriptingNodeKeyName
    {
        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public NodePropertyIndex(INodePropertyIndex source) : base(source)
        { }

        /// <inheritdoc cref="ScriptingNodeKeyName(IScriptingNodeKeyName)"/>
        public NodePropertyIndex(IScriptingNodeKeyName source) : base(source)
        { }
        
    }
}
