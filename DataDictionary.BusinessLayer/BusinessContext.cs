using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Used to define/set gobal values used by the business layer.
    /// This uses a simple Singleton Pattern.
    /// </summary>
    public class BusinessContext
    {
        /// <summary>
        /// Accessor to the Business Context. 
        /// </summary>
        internal static BusinessContext Instance { get; private set; } = new BusinessContext()
        {
            DbContext = new Toolbox.DbContext.Context(),
            AddWork = (item) => { }
        };

        /// <summary>
        /// Constructor. Singleton Pattern.
        /// Calling this method will reset the Context for the Business layer.
        /// The default Context the properties are usally not configured or Null condition.
        /// </summary>
        public BusinessContext()
        { Instance = this; }

        /// <summary>
        /// Database Context used by the Business Layer.
        /// </summary>
        public required Context DbContext { get; init; }

        public required Action<WorkItem> AddWork { get; init; }

    }
}
