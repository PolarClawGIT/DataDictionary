using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System.Xml.Linq;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <summary>
    /// Helper Class for Attribute
    /// </summary>
    static class AttributeHelper
    {
        /// <summary>
        /// Used to load the NamedScopes of the Attributes.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="subjects"></param>
        /// <param name="addNamedScope"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadNamedScope(
            this IAttribute data,
            ModelValue model,
            ISubjectAreaData subjects,
            Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();
            String workName = "Adding NamedScopes (Attributes)";

            foreach (AttributeValue item in data.Values)
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

            IEnumerable<SubjectAreaValue> ParentSubjects(IAttributeIndex index)
            {

                AttributeIndex key = new AttributeIndex(index);

                return data.Values.
                    Where(w => key.Equals(w)).
                    Join(data.SubjectArea,
                        attribute => new AttributeIndex(attribute),
                        subject => new AttributeIndex(subject),
                        (attribute, subject) => new SubjectAreaIndex(subject)).
                    Join(subjects,
                        subjectKey => subjectKey,
                        subject => new SubjectAreaIndex(subject),
                        (key, subject) => subject).
                    ToList();
            }
        }
    }
}
