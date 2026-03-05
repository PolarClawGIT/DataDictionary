using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Node database operations.  
    /// </summary>  
    [Obsolete]
    static class TemplateNode
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(TemplateNode));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Node.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetTemplateNode]");

        /// <summary>  
        /// The stored procedure used to set scripting Template Node.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetTemplateNode]");

        /// <summary>  
        /// The user-defined table type for scripting Template Node.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttTemplateNode]");
    }
}
