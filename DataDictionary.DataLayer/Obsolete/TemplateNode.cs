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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(TemplateNode));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template Node.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Template Node.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Template Node.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
