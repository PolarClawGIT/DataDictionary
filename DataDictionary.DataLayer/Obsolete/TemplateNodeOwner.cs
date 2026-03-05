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
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(TemplateNodeOwner));

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template NodeOwner.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetTemplateNodeOwner]");

        /// <summary>  
        /// The stored procedure used to set scripting Template NodeOwner.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetTemplateNodeOwner]");

        /// <summary>  
        /// The user-defined table type for scripting Template NodeOwner.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttTemplateNodeOwner]");
    }
}
