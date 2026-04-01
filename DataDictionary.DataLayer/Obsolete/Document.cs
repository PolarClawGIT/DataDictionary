using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.Obsolete
{
    [Obsolete]
    static class Document
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Document));

        /// <summary>  
        /// The parameter name for the Scripting DocumentId ID.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// The stored procedure used to retrieve scripting Document.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Document.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Document.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
