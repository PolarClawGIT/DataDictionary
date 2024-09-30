using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Definition;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IDefinitionIndexName : IDomainDefinitionKeyName
    { }

    /// <inheritdoc/>
    public class DefinitionIndexName : DomainDefinitionKeyName, IDefinitionIndexName,
        IKeyEquality<IDefinitionIndexName>, IKeyEquality<DefinitionIndexName>
    {
        /// <inheritdoc cref="DomainDefinitionKeyName(IDomainDefinitionKeyName)"/>
        public DefinitionIndexName(IDefinitionIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IDefinitionIndexName? other)
        { return other is IDomainDefinitionKeyName key && Equals(new DomainDefinitionKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(DefinitionIndexName? other)
        { return other is IDomainDefinitionKeyName key && Equals(new DomainDefinitionKeyName(key)); }

        /// <summary>
        /// Convert DefinitionIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(DefinitionIndexName source)
        { return new DataIndexName() { Title = source.DefinitionTitle ?? String.Empty }; }
    }
}
