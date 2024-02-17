using DataDictionary.BusinessLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        public IDomainData DomainData { get; } = new DomainData.DomainData();
    }
}
