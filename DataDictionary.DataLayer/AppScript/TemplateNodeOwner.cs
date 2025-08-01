using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Node Owner database operations.  
    /// </summary>  
    /// <remarks>
    /// XML Attributes are owned by XML Elements.
    /// Database allows M:N relationship where the same Atteribute can be owned by muliple Elements.
    /// </remarks>
    static class TemplateNodeOwner
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting Template NodeOwner.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetTemplateNodeOwner]";

        /// <summary>  
        /// The stored procedure used to set scripting Template NodeOwner.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetTemplateNodeOwner]";

        /// <summary>  
        /// The user-defined table type for scripting Template NodeOwner.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttTemplateNodeOwner]";
    }
}
