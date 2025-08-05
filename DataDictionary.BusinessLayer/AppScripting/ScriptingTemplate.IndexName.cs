using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{

    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public interface IScriptingTemplateName : IScriptingTemplateKeyName
    { }

    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public class ScriptingTemplateName : ScriptingTemplateKeyName, IScriptingTemplateName,
        IKeyEquality<IScriptingTemplateName>, IKeyEquality<ScriptingTemplateName>
    {
        /// <inheritdoc cref="ScriptingTemplateKeyName(IScriptingTemplateKeyName)"/>
        public ScriptingTemplateName(IScriptingTemplateName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IScriptingTemplateName? other)
        { return other is IScriptingTemplateKeyName key && Equals(new ScriptingTemplateKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ScriptingTemplateName? other)
        { return other is IScriptingTemplateKeyName key && Equals(new ScriptingTemplateKeyName(key)); }

        /// <summary>
        /// Convert TemplateIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(ScriptingTemplateName source)
        { return new DataIndexName() { Title = source.TemplateTitle ?? String.Empty }; }
    }
}
