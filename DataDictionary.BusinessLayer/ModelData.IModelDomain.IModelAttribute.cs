using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Attribute
    /// </summary>
    public interface IModelAttribute
    {
        /// <summary>
        /// List of Domain Attributes within the Model.
        /// </summary>
        DomainAttributeCollection DomainAttributes { get; }

        /// <summary>
        /// List of Domain Aliases for the Attributes within the Model.
        /// </summary>
        DomainAttributeAliasCollection DomainAttributeAliases { get; }

        /// <summary>
        /// List of Domain Properties for the Attributes within the Model.
        /// </summary>
        DomainAttributePropertyCollection DomainAttributeProperties { get; }
    }
}
