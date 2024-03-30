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
        /// Create Work Items that are used to load the NameScopeDictionary
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Export(IList<NamedScopeItem> target);
    }

    /// <summary>
    /// Interface for objects that contain NameScope Data
    /// </summary>
    public interface INamedScopeData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Create Work Items that are used to load the NameScopeDictionary
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Export(IList<NamedScopeItem> target, Func<TKey?> parent);
    }
}
