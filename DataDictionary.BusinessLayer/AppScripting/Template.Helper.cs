using DataDictionary.BusinessLayer.NamedScope;
using System;
using System.Collections.Generic;
using System.Text;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Helper class for Scripting Templates
    /// </summary>
    static class TemplateHelper
    {

        public static IReadOnlyList<WorkItem> LoadNamedScope(this TemplateData data, Action<INamedScopeSourceValue?, NamedScopeValue> addNamedScope)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(NameSpaceSource.Load<ITemplateData, TemplateValue>(data, addNamedScope));

            work.AddRange(NameSpaceSource.Load<ISchemaDefinitionData, SchemaDefinitionValue>(data.Schemata, addNamedScope,
                (parent) => data.FirstOrDefault(w => new TemplateIndex(parent).Equals(w))));

            work.AddRange(NameSpaceSource.Load<ITransformData, TransformValue>(data.Transforms, addNamedScope,
                (parent) => data.FirstOrDefault(w => new TemplateIndex(parent).Equals(w))));

            return work;
        }
    }
}
