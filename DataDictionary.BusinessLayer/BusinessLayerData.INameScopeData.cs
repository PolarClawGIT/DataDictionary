using DataDictionary.BusinessLayer.NameScope;
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
        /// Wrapper for NameScope Data (NameSpace)
        /// </summary>
        public NameScopeDictionary NameScope { get; } = new NameScopeDictionary();

        public void LoadNameScope()
        {
            NameScope.Clear();
            NameScope.AddRange(database.GetNameScopes());
            NameScope.AddRange(library.GetNameScopes());
            NameScope.AddRange(domain.GetNameScopes());
        }
    }

}
