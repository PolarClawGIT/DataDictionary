using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    /// <summary>
    /// Interface for the Database Scope Name
    /// </summary>
    public interface IDbScopeName
    {
        /// <summary>
        /// The Database Scope Name. This describes the Scope/Level of an Object within a NameSpace.
        /// </summary>
        String? ScopeName { get; }
    }
}
