using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.SubjectArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData: IModelDomain
    {
        /// <inheritdoc/>
        public DomainAttributeCollection DomainAttributes { get; } = new DomainAttributeCollection();

        /// <inheritdoc/>
        public DomainAttributeAliasCollection DomainAttributeAliases { get; } = new DomainAttributeAliasCollection();

        /// <inheritdoc/>
        public DomainAttributePropertyCollection DomainAttributeProperties { get; } = new DomainAttributePropertyCollection();

        /// <inheritdoc/>
        public DomainEntityCollection DomainEntities { get; } = new DomainEntityCollection();

        /// <inheritdoc/>
        public DomainEntityAliasCollection DomainEntityAliases { get; } = new DomainEntityAliasCollection();

        /// <inheritdoc/>
        public DomainEntityPropertyCollection DomainEntityProperties { get; } = new DomainEntityPropertyCollection();

        /// <inheritdoc/>
        public DomainSubjectAreaCollection DomainSubjectAreas { get; } = new DomainSubjectAreaCollection();
    }
}
