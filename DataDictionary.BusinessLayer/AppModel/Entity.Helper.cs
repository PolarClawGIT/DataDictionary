using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Helper Class for Entity
    /// </summary>
    static class EntityHelper
    {
        /// <summary>
        /// Used to load the NamedScopes of the entities.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="subjects"></param>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNamedScope(
            this IEntity data,
            ModelValue model,
            ISubjectAreaData subjects,
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            String workName = "Adding NamedScopes (Entities)";

            foreach (EntityValue item in data.Entities)
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

            IEnumerable<SubjectAreaValue> ParentSubjects(IEntityIndex index)
            {
                EntityIndex key = new EntityIndex(index);

                return data.Entities.
                    Where(w => key.Equals(w)).
                    Join(data.SubjectArea,
                        entity => new EntityIndex(entity),
                        subject => new EntityIndex(subject),
                        (entity, subject) => new SubjectAreaIndex(subject)).
                    Join(subjects,
                        subjectKey => subjectKey,
                        subject => new SubjectAreaIndex(subject),
                        (key, subject) => subject).
                    ToList();
            }
        }
    }
}
