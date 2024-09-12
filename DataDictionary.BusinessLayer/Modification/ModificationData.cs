using DataDictionary.BusinessLayer.DbWorkItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Modification
{
    /// <summary>
    /// Interface describing Modification Data
    /// </summary>
    public interface IModificationData
    {
        /// <summary>
        /// Create WorkItems that loads data from the Database with History
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="includeHistory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, Boolean includeHistory);

    }
}
