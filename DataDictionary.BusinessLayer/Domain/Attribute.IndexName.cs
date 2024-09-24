using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeIndexName : IDomainAttributeKeyName
    { }

    /// <inheritdoc/>
    public class AttributeIndexName : DomainAttributeKeyName, IAttributeIndexName,
        IKeyEquality<IAttributeIndexName>, IKeyEquality<AttributeIndexName>
    {
        /// <inheritdoc cref="DomainAttributeKeyName(IDomainAttributeKeyName)"/>
        public AttributeIndexName(IAttributeIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Table.IDbTableColumnKeyName)"/>
        internal AttributeIndexName(ITableColumnIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Routine.IDbRoutineParameterKeyName)"/>
        internal AttributeIndexName(IRoutineParameterIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IAttributeIndexName? other)
        { return other is IDomainAttributeKeyName value && Equals(new DomainAttributeKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(AttributeIndexName? other)
        { return other is IDomainAttributeKeyName value && Equals(new DomainAttributeKeyName(value)); }

        /// <summary>
        /// Convert AttributeIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(AttributeIndexName source)
        { return new DataIndexName() { Title = source.AttributeTitle ?? String.Empty }; }
    }
}
