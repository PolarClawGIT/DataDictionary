using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Domain;
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
            ScriptingWork scripting = new ScriptingWork(templateKey, ScriptingEngine);
            Action<Int32, Int32> onBuildProgress = (x, y) => { };
            Action<Int32, Int32> onTransformProgress = (x, y) => { };
            Boolean cancelWork = false;

            WorkItem docWork = new WorkItem()
            {
                WorkName = "Build Template Documents",
                IsCanceling = () => cancelWork,
                DoWork = () =>
                {
                    Int32 totlaWork = scripting.Paths.Count();
                    Int32 completeWork = 0;
                    TemplateDocumentValue? doc = null;

                    foreach (TemplatePathValue item in scripting.Paths)
                    {
                        NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(item.PathName).ToArray());

                        foreach (NamedScopeIndex value in namedScopeValue.PathKeys(path))
                        {
                            INamedScopeSourceValue namedScope = namedScopeValue.GetData(value);
                            String elementName = namedScope.Title;
                            ScopeType scope = namedScope.Scope;
                            dynamic data = namedScopeValue.GetData(value);

                            if (scripting.Template.BreakOnScope == namedScope.Scope)
                            {
                                doc = new TemplateDocumentValue(scripting.Template) { ElementName = elementName };
                                scripting.Documents.Add(doc);
                            }
                            else if (doc is null)
                            {
                                doc = new TemplateDocumentValue(scripting.Template) { ElementName = Model.ModelTitle };
                                scripting.Documents.Add(doc);
                            }

                            try
                            {
                                if (BuildElement(scripting, data) is XElement docValue)
                                { doc.Source.Add(docValue); }
                            }
                            catch (Exception ex)
                            {
                                ex.Data.Add(nameof(namedScope.Title), namedScope.Title);
                                ex.Data.Add(nameof(namedScope.Scope), namedScope.Scope);
                                ex.Data.Add(nameof(namedScope.Path), namedScope.Path.MemberFullPath);
                                doc.Exception = ex;
                            }

                            completeWork = completeWork + 1;
                            onBuildProgress(completeWork, totlaWork);
                        }

                    }
                }
            };
            onBuildProgress = docWork.OnProgressChanged;

            WorkItem docTransform = new WorkItem()
            {
                WorkName = "Apply Template Transforms",
                IsCanceling = () => cancelWork,
                DoWork = () =>
                {
                    Int32 totlaWork = scripting.Documents.Count();
                    Int32 completeWork = 0;

                    foreach (TemplateDocumentValue doc in scripting.Documents)
                    {
                        doc.ApplyTransform();

                        completeWork = completeWork + 1;
                        onBuildProgress(completeWork, totlaWork);
                    }

                }
            };
            onTransformProgress = docTransform.OnProgressChanged;

            work.Add(docWork);
            work.Add(docTransform);
            return work;
        }

        /// <summary>
        /// Generic catch when runtime cannot determine type.
        /// </summary>
        /// <param name="scripting"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        XElement? BuildElement(ScriptingWork scripting, Object data)
        {
            Exception ex = new ArgumentException("Missing BuildWork method");
            if (data is INamedScopeSourceValue values)
            {
                ex.Data.Add(nameof(values.Title), values.Title);
                ex.Data.Add(nameof(values.Scope), values.Scope);
                ex.Data.Add(nameof(values.Path), values.Path.MemberFullPath);
            }
            else
            {
                Type dataType = data.GetType();
                ex.Data.Add(nameof(dataType.FullName), dataType.FullName);
            }

            throw ex;
        }

        XElement? BuildElement(ScriptingWork scripting, IAttributeIndex data)
        { return domainValue.Attributes.GetXElement(scripting, data); }

    }
}
