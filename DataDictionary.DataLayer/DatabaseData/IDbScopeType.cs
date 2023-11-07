using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData
{
    /// <summary>
    /// Database Scope
    /// </summary>
    public interface IDbScopeType
    {
        /// <summary>
        /// The Database Scope Name. This describes the Scope/Level of an Object within database.
        /// </summary>
        string? ScopeName { get; }
    }
}
