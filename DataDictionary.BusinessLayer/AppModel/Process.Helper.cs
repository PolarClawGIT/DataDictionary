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
    static class ProcessHelper
    {
        /// <summary>
        /// Used to load the NamedScopes of the Processes.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="subjects"></param>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNamedScope(
            this IProcess data,
            ModelValue model,
            ISubjectAreaData subjects,
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            String workName = "Adding NamedScopes (Processes)";

            foreach (ProcessValue item in data.Processes)
            {
                Boolean hasParent = false;

                foreach (SubjectAreaValue subjectParent in ParentSubjects(item))
                {
                    NamedScopeValue newItem = new NamedScopeValue(item)
                    {
                        GetPath = () => new PathIndex(
                            ((IPathValue)subjectParent).Path,
                            ((IPathValue)item).Path)
                    };

                    work.Add(new WorkItem()
                    { WorkName = workName, DoWork = () => addNamedScope(subjectParent, newItem) });

                    hasParent = true;
                }

                if (!hasParent) // No Parents found
                {
                    NamedScopeValue newItem = new NamedScopeValue(item);

                    work.Add(new WorkItem()
                    { WorkName = workName, DoWork = () => addNamedScope(model, newItem) });
                }
            }

            return work;

            IEnumerable<SubjectAreaValue> ParentSubjects(IProcessIndex index)
            {
                ProcessIndex key = new ProcessIndex(index);

                return data.Processes.
                    Where(w => key.Equals(w)).
                    Join(data.SubjectArea,
                        Process => new ProcessIndex(Process),
                        subject => new ProcessIndex(subject),
                        (Process, subject) => new SubjectAreaIndex(subject)).
                    Join(subjects,
                        subjectKey => subjectKey,
                        subject => new SubjectAreaIndex(subject),
                        (key, subject) => subject).
                    ToList();
            }
        }

    }
}
