using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for objects that contain NameScope Data
    /// </summary>
    public interface INamedScopeData
    {
        /// <summary>
        /// Create WorkItems to build the NamedScope Dictionary.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Build(NamedScopeDictionary target);
    }
}
