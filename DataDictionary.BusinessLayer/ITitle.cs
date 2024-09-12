using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Objects that have a Name or Title
    /// </summary>
    public interface ITitle
    {
        /// <summary>
        /// Title of the Value.
        /// </summary>
        String Title { get { return GetTitle(); } }

        /// <summary>
        /// Gets the generic Title from the Value
        /// </summary>
        /// <returns></returns>
        String GetTitle();
    }
}
