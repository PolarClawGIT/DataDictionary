using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.Obsolete
{
    [Obsolete]
    static class Document
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Document));

        /// <summary>  
        /// The parameter name for the Scripting DocumentId ID.  
        /// </summary>  
        public readonly static String DocumentId = "@DocumentId";

        /// <summary>  
        /// The stored procedure used to retrieve scripting Document.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetDocument]");

        /// <summary>  
        /// The stored procedure used to set scripting Document.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetDocument]");

        /// <summary>  
        /// The user-defined table type for scripting Document.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttDocument]");
    }
}
