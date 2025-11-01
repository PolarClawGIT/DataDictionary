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
    public interface ITemplateNodeIndexName : ITemplateNodeKeyName
    { }

    /// <inheritdoc/>
    public class TemplateNodeIndexName : TemplateNodeKeyName, ITemplateNodeIndexName,
        IKeyEquality<ITemplateNodeIndexName>, IKeyEquality<TemplateNodeIndexName>
    {
        /// <inheritdoc cref="TemplateNodeKeyName(ITemplateNodeKeyName)"/>
        public TemplateNodeIndexName(ITemplateNodeIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateNodeIndexName? other)
        { return other is ITemplateNodeKeyName value && Equals(new TemplateNodeKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateNodeIndexName? other)
        { return other is ITemplateNodeKeyName value && Equals(new TemplateNodeKeyName(value)); }

        /// <summary>
        /// Convert TemplateNodeIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TemplateNodeIndexName source)
        { return new DataIndexName() { Title = source.NodeName ?? String.Empty }; }
    }
}
