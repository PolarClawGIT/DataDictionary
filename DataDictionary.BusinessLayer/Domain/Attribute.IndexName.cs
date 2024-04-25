using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeIndexName : IDomainAttributeKeyName
    { }

    /// <inheritdoc/>
    public class AttributeIndexName : DomainAttributeKeyName
    {
        /// <inheritdoc cref="DomainAttributeKeyName(IDomainAttributeKeyName)"/>
        public AttributeIndexName(IAttributeIndexName source) : base(source) { }
    }
}
