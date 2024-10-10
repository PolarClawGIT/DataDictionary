using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for common Security Object Properties
    /// </summary>
    public interface IObjectSecurity
    {
        /// <summary>
        /// Current User can alter the Value of the Object. Usually Insert/Update/Delete 
        /// </summary>
        Boolean AlterValue { get; }

        /// <summary>
        /// Current User can alter the Security of the Object.
        /// </summary>
        Boolean AlterSecurity { get; }
    }
}
