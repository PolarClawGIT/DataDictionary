using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Static class containing constants related to the Document Object database operations.
    /// </summary>
    static class DocumentObject
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(DocumentObject));

        /// <summary>
        /// Stored procedure to retrieve an DocumentObject.
        /// </summary>
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>
        /// Stored procedure to set an DocumentObject.
        /// </summary>
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>
        /// User-defined table type for DocumentObject operations.
        /// </summary>
        public readonly static String TableType = dataObject.TableType;
    }
}
