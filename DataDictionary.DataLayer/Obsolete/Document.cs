using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.Obsolete
{
    [Obsolete]
    static class Document
    {
        /// <summary>  
        /// The parameter name for the Scripting DocumentId ID.  
        /// </summary>  
        public const String DocumentId = "@DocumentId";

        /// <summary>  
        /// The stored procedure used to retrieve scripting Document.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetDocument]";

        /// <summary>  
        /// The stored procedure used to set scripting Document.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetDocument]";

        /// <summary>  
        /// The user-defined table type for scripting Document.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttDocument]";
    }
}
