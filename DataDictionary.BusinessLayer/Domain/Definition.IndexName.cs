using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDefinitionIndexName : IDefinitionKeyName
    { }

    /// <inheritdoc/>
    public class DefinitionIndexName : DefinitionKeyName, IDefinitionIndexName,
        IKeyEquality<IDefinitionIndexName>, IKeyEquality<DefinitionIndexName>
    {
        /// <inheritdoc cref="DefinitionKeyName(IDefinitionKeyName)"/>
        public DefinitionIndexName(IDefinitionIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDefinitionIndexName? other)
        { return other is IDefinitionKeyName key && Equals(new DefinitionKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DefinitionIndexName? other)
        { return other is IDefinitionKeyName key && Equals(new DefinitionKeyName(key)); }

        /// <summary>
        /// Convert DefinitionIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(DefinitionIndexName source)
        { return new DataIndexName() { Title = source.DefinitionTitle ?? String.Empty }; }
    }
}
