using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Helper Class for Subject Area
    /// </summary>
    static class SubjectAreaHelper
    {
        /// <summary>
        /// Used to load the NamedScopes of the Subject Areas.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNamedScope(
            this ISubjectAreaData data,
            ModelValue model,
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            String workName = "Adding NamedScopes (Subject Areas)";

            foreach (SubjectAreaValue item in data)
            {
                NamedScopeValue newItem = new NamedScopeValue(item)
                { GetPath = () => new PathIndex(((IPathValue)item).Path) };

                work.Add(new WorkItem()
                { WorkName = workName, DoWork = () => addNamedScope(model, newItem) });
            }

            return work;
        }
    }
}
