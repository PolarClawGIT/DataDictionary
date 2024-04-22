using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for NameScope Data (NameSpace)
        /// </summary>
        public INamedScopeData NamedScope { get { return namedScopeValue; } }
        private readonly NamedScopeData namedScopeValue;
    }
}
