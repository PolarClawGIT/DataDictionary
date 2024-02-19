using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NameScope
{
    interface INameScopeData
    {
        /// <summary>
        /// Generates a list of NameScope to be added to the NameScope Dictionary.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        IReadOnlyList<NameScopeItem> GetContextNames(Action<Int32, Int32> progress);
    }
}
