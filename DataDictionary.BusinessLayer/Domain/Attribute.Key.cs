using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeKey : IDomainAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeKey : DomainAttributeKey, IAttributeKey
    {
        /// <inheritdoc/>
        public AttributeKey(IDomainAttributeKey source) : base(source) { }
    }
}
