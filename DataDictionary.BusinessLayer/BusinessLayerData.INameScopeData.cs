using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            work.AddRange(subjectAreaValues.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(domainValues.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(databaseValues.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(libraryValues.LoadNamedScope(namedScopeValues.Add));
            work.AddRange(scriptingValues.LoadNamedScope(namedScopeValues.Add));

            return work;
        }
    }
}
