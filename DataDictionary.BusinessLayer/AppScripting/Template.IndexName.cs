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
    public interface ITemplateIndexName : ITemplateKeyName
    { }

    /// <inheritdoc/>
    public class TemplateIndexName : TemplateKeyName, ITemplateIndexName,
        IKeyEquality<ITemplateIndexName>, IKeyEquality<TemplateIndexName>
    {
        /// <inheritdoc cref="TemplateKeyName(ITemplateKeyName)"/>
        public TemplateIndexName(ITemplateIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateIndexName? other)
        { return other is ITemplateKeyName value && Equals(new TemplateKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TemplateIndexName? other)
        { return other is ITemplateKeyName value && Equals(new TemplateKeyName(value)); }

        /// <summary>
        /// Convert TemplateIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(TemplateIndexName source)
        { return new DataIndexName() { Title = source.TemplateTitle ?? String.Empty }; }
    }
}
