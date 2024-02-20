using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NameScope
{
    /// <summary>
    /// Interface for objects that contain NameScope Data
    /// </summary>
    interface INameScopeData
    {
        /// <summary>
        /// Generates a list of NameScope to be added to the NameScope Dictionary.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<NameScopeItem> GetNameScopes();
    }
}
