using DataDictionary.BusinessLayer.ToolSet;
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
    public interface ITemplateIndexName : IScriptingTemplateKeyName
    { }

    /// <inheritdoc/>
    public class TemplateIndexName : ScriptingTemplateKeyName, ITemplateIndexName,
        IKeyEquality<ITemplateIndexName>, IKeyEquality<TemplateIndexName>
    {
        /// <inheritdoc cref="ScriptingTemplateKeyName(IScriptingTemplateKeyName)"/>
        public TemplateIndexName(ITemplateIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateIndexName? other)
        { return other is IScriptingTemplateKeyName key && Equals(new ScriptingTemplateKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateIndexName? other)
        { return other is IScriptingTemplateKeyName key && Equals(new ScriptingTemplateKeyName(key)); }

        /// <summary>
        /// Convert TemplateIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TemplateIndexName source)
        { return new DataIndexName() { Title = source.TemplateTitle ?? String.Empty }; }
    }
}
