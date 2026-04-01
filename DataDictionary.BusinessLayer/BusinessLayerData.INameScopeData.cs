using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{

    partial class BusinessLayerData
    {
        /// <summary>
        /// Wrapper for NameScope Data (NameSpace)
        /// </summary>
        public INamedScopeData NamedScope { get { return namedScopeValues; } }
        private readonly NamedScopeData namedScopeValues;

        /// <summary>
        /// Work Items to Clears then Load the NamedScope
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> LoadNamedScope()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork= () => namedScopeValues.Clear() });

            work.AddRange(modelValues.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(catalogValue.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(libraryValues.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(scriptingValue.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(templateValues.LoadNamedScope(namedScopeValues.Add));
            return work;
        }
    }
}
