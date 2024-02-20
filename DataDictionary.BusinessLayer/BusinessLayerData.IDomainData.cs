using DataDictionary.BusinessLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for the Domain Data (Entity, Attribute, Process ...)
        /// </summary>
        public IDomainData DomainData { get { return domain; } }
        private readonly DomainData.DomainData domain = new DomainData.DomainData();
    }
}
