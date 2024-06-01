using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeDefinitionValue : IDomainAttributeDefinitionItem, IDefinitionIndex, IAttributeIndex
    { }

    /// <inheritdoc/>
    public class AttributeDefinitionValue : DomainAttributeDefinitionItem, IAttributeDefinitionValue
    {
        /// <inheritdoc/>
        public AttributeDefinitionValue() : base() { }

        /// <inheritdoc/>
        public AttributeDefinitionValue(IAttributeIndex attributeKey) : base(attributeKey) { }
    }
}
