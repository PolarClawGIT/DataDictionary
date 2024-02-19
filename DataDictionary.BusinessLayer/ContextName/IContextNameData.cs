using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.ContextName
{
    interface IContextNameData
    {
        /// <summary>
        /// Generates a list of Context Names to be added to the Context Name Dictionary.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        IReadOnlyList<ContextNameItem> GetContextNames(Action<Int32, Int32> progress);
    }
}
