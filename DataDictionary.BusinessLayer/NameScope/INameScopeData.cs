using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.NameScope
{
    /// <summary>
    /// Interface for objects that contain NameScope Data
    /// </summary>
    public interface INameScopeData
    {
        /// <summary>
        /// Create Work Items that are used to load the NameScopeDictionary
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Export(IList<NameScopeItem> target);
    }
}
