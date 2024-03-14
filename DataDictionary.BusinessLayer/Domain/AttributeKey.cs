using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public class AttributeKey : DomainAttributeKey
    {
        /// <inheritdoc/>
        public AttributeKey(IDomainAttributeKey source): base(source) { }
    }
}
