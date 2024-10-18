using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Principal Name
    /// </summary>
    public interface IPrincipalName
    {
        /// <summary>
        /// Display Name of the Principal. Typically the name of the individual.
        /// </summary>
        String? PrincipalName { get; }
    }
}
