using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Resource.Enumerations;
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
                    XElement? rootElement = null;

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
                                rootElement = BuildElement(scripting, data);

                                doc = new TemplateDocumentValue(scripting.Template, rootElement) { ElementName = elementName };
                                scripting.Documents.Add(doc);
                            }
                            else if (doc is null)
                            {
                                rootElement = new XElement(ScopeEnumeration.Cast(Model.Scope).Name);
                                doc = new TemplateDocumentValue(scripting.Template, rootElement) { ElementName = Model.ModelTitle };

                                try
                                { rootElement.Add(BuildElement(scripting, data)); }
                                catch (Exception ex)
                                {
                                    ex.Data.Add(nameof(namedScope.Title), namedScope.Title);
                                    ex.Data.Add(nameof(namedScope.Scope), namedScope.Scope);
                                    ex.Data.Add(nameof(namedScope.Path), namedScope.Path.MemberFullPath);
                                    doc.Exception = ex;
                                }

                                scripting.Documents.Add(doc);
                            }
                            else if(rootElement is XElement)
                            {
                                try
                                { rootElement.Add(BuildElement(scripting, data)); }
                                catch (Exception ex)
                                {
                                    ex.Data.Add(nameof(namedScope.Title), namedScope.Title);
                                    ex.Data.Add(nameof(namedScope.Scope), namedScope.Scope);
                                    ex.Data.Add(nameof(namedScope.Path), namedScope.Path.MemberFullPath);
                                    doc.Exception = ex;
                                }
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

        /// <summary>
        /// Build XML Element for Domain Attributes
        /// </summary>
        /// <param name="scripting"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        XElement? BuildElement(ScriptingWork scripting, IAttributeIndex data)
        { return domainValue.Attributes.GetXElement(scripting, data); }

    }
}
