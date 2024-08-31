using DataDictionary.BusinessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Contains a DomainModel
    /// </summary>
    public interface IDomainData
    {
        /// <summary>
        /// Wrapper for the Domain Data (Entity, Attribute, Process ...)
        /// </summary>
        IDomainModel DomainModel { get; }
    }

    partial interface IBusinessLayerData : IDomainData
    { }

    partial class BusinessLayerData: IDomainData
    {
        /// <inheritdoc/>
        public IDomainModel DomainModel { get { return domainValues; } }
        private readonly DomainModel domainValues;
    }
}
