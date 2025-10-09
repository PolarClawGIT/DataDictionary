using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Node database operations.  
    /// </summary>  
    static class TemplateNode
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Node.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetTemplateNode]";

        /// <summary>  
        /// The stored procedure used to set scripting Template Node.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetTemplateNode]";

        /// <summary>  
        /// The user-defined table type for scripting Template Node.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttTemplateNode]";
    }
}
