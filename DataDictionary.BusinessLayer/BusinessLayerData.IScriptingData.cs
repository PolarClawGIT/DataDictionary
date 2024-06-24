using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for the Catalog (database) Data
        /// </summary>
        public IScriptingEngine ScriptingEngine { get { return scriptingValue; } }
        private readonly ScriptingEngine scriptingValue;

        /// <summary>
        /// Builds the XML and Script documents for the Template.
        /// </summary>
        /// <param name="templateKey"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> BuildDocuments(ITemplateIndex templateKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            TemplateBinding target = new TemplateBinding(templateKey, scriptingValue);

            if (target.Template.BreakOnScope is ScopeType.Null or ScopeType.Model)
            {
                TemplateDocumentValue doc = new TemplateDocumentValue(target.Template) { ElementName = Model.ModelTitle };
                doc.Source.Add(new XElement(Model.Scope.ToName()));
                target.Documents.Add(doc);
            }

            foreach (TemplatePathValue item in target.Paths)
            {
                NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(item.PathName).ToArray());
                ScopeKey scope = new ScopeKey(item.PathScope);

                IEnumerable<INamedScopeSourceValue> values = namedScopeValue.PathKeys(path).Select(s => namedScopeValue.GetData(s)).Where(w => scope.Equals(w));

                work.AddRange(domainValue.BuildDocuments(target, values));
            }


            return work;
        }

    }
}
