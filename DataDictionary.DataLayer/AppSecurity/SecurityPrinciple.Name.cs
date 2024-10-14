using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISecurityPrincipleName
    {
        /// <summary>
        /// Display Name of the Principle. Typically the name of the individual.
        /// </summary>
        String? PrincipleName { get; }
    }
}
