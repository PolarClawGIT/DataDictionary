using System.Xml.Linq;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Template
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Title of the Scripting Template
        /// </summary>
        String? TemplateTitle { get; }

        /// <summary>
        /// Description of the Scripting Template
        /// </summary>
        String? TemplateDescription { get; }

        String? BreakOnScope { get; }

        XElement TransformScript { get; }

        String? RootDirectory { get; }
        String? DocumentDirectory { get; }
        String? DocumentPrefix { get; }
        String? DocumentSuffix { get; }
        String? DocumentExtension { get; }
        String? ScriptAs { get; }
        String? ScriptDirectory { get; }
        String? ScriptPrefix { get; }
        String? ScriptSuffix { get; }
        String? ScriptExtension { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Scripting Template database operations.  
    /// </summary>  
    static class Template
    {
        /// <summary>  
        /// The stored procedure used to retrieve scripting Template.  
        /// </summary>  
        public const String GetProcedure = "[AppScript].[procGetTemplate]";

        /// <summary>  
        /// The stored procedure used to set scripting Template.  
        /// </summary>  
        public const String SetProcedure = "[AppScript].[procSetTemplate]";

        /// <summary>  
        /// The user-defined table type for scripting Template.  
        /// </summary>  
        public const String TableType = "[AppScript].[udttTemplate]";
    }
}
