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
        public INamedScopeData NamedScope { get { return namedScopeValue; } }
        private readonly NamedScopeData namedScopeValue;

        /// <summary>
        /// Work Items to Clears then Load the NamedScope
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> LoadNamedScope()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { DoWork= () => namedScopeValue.Clear() });

            work.AddRange(modelValues.AddNamedScopes(namedScopeValue.Add));
            work.AddRange(subjectAreaValues.AddNamedScopes(namedScopeValue.Add));

            work.Add(new WorkItem()
            {
                WorkName = "Load NamedScope",
                DoWork = () =>
                {
                    //namedScopeValue.AddRange(modelValue.GetNamedScopes());
                    //namedScopeValue.AddRange(subjectAreaValues.GetNamedScopes());
                    namedScopeValue.AddRange(domainValue.GetNamedScopes());
                    namedScopeValue.AddRange(databaseValue.GetNamedScopes());
                    namedScopeValue.AddRange(libraryValue.GetNamedScopes());
                    namedScopeValue.AddRange(scriptingValue.GetNamedScopes());
                }
            });

            return work;
        }
    }
}
