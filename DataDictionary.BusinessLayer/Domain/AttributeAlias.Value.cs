using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeAliasValue: IDomainAttributeAliasItem
    { }

    /// <inheritdoc/>
    public class AttributeAliasValue : DomainAttributeAliasItem, IAttributeAliasValue
    {
        /// <inheritdoc/>
        public AttributeAliasValue() : base() { }

        /// <inheritdoc/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }
    }
}
