using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface that supports returning the Temporal data.
    /// </summary>
    public interface ITemporalData
    {
        /// <summary>
        /// Returns the Temporal Data
        /// </summary>
        /// <returns></returns>
        ITemporalView GetTemporal();
    }
}
