using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Entity
    /// </summary>
    public interface IModelEntity
    {
        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        DomainEntityCollection DomainEntities { get; }

        /// <summary>
        /// List of Domain Aliases for the Entities within the Model.
        /// </summary>
        DomainEntityAliasCollection DomainEntityAliases { get; }

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        DomainEntityPropertyCollection DomainEntityProperties { get; }
    }
}
