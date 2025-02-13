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
    /// Helper Class for Model
    /// </summary>
    static class ModelHelper
    {
        /// <summary>
        /// Used to load the NamedScopes of the Model.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNamedScope(
            this IModelData data,
            ModelValue model,
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            String workName = "Adding NamedScopes (Model)";

            if (data.Count == 0)
            {
                NamedScopeValue newItem = new NamedScopeValue(model)
                { GetPath = () => new PathIndex(((IPathValue)model).Path) };

                work.Add(new WorkItem()
                { WorkName = workName, DoWork = () => addNamedScope(null, newItem) });
            }
            else
            {
                foreach (ModelValue item in data)
                {
                    NamedScopeValue newItem = new NamedScopeValue(item)
                    { GetPath = () => new PathIndex(((IPathValue)item).Path) };

                    work.Add(new WorkItem()
                    { WorkName = workName, DoWork = () => addNamedScope(null, newItem) });
                }
            }

            return work;
        }

        public static IReadOnlyList<WorkItem> LoadNamedScope(
            this ModelValue data,
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            String workName = "Adding NamedScopes (Model)";

            NamedScopeValue newItem = new NamedScopeValue(data)
            { GetPath = () => new PathIndex(((IPathValue)data).Path) };

            work.Add(new WorkItem()
            { WorkName = workName, DoWork = () => addNamedScope(null, newItem) });

            return work;
        }
    }
}
