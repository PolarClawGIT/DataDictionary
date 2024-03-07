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
        public NamedScopeDictionary NameScope { get; }
    }

    /// <summary>
    /// Extension class for NameScopeDictionary
    /// </summary>
    public static class BusinessLayerData_NameScope
    {
        /// <summary>
        /// Create WorkItems to Import a List of NameScopeItems to the NameScopeDictionary
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> Import(this NamedScopeDictionary target, IList<NamedScopeItem> source)
        {
            return new WorkItem()
            {
                WorkName = "Load NameScope",
                DoWork = () => target.AddRange(source)
            }.ToList();
        }

        /// <summary>
        /// Create WorkItems to Remove all items from a NameScopeDictionary.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> Remove(this NamedScopeDictionary target)
        { return new WorkItem() { WorkName = "Remove NameScope", DoWork = () => target.Clear() }.ToList(); }
    }

}
