using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>  
    /// Static class containing constants related to the Scripting Template Node Owner database operations.  
    /// </summary>  
    /// <remarks>
    /// XML Attributes are owned by XML Elements.
    /// Database allows M:N relationship where the same Atteribute can be owned by muliple Elements.
    /// </remarks>
    [Obsolete]
    static class TemplateNodeOwner
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(TemplateNodeOwner));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template NodeOwner.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Template NodeOwner.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Template NodeOwner.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
