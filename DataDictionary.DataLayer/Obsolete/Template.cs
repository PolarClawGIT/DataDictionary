using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Scripting Template
    /// </summary>
    [Obsolete]
    public interface ITemplate: ITemplateKeyName
    {
        /// <summary>
        /// Description of the Scripting Template
        /// </summary>
        String? TemplateDescription { get; }

        /// <summary>
        /// The Scope to have a document break on. Null = no break.
        /// </summary>
        String? BreakOnScope { get; }

        /// <summary>
        /// XSLT Transform Script.
        /// </summary>
        /// <remarks>
        /// Root Node should be- xsl:stylesheet
        /// XML NameSpace- xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        /// </remarks>
        String? TransformScript { get; }

        /// <summary>
        /// Name of the Special Directory used as the Root Directory.
        /// </summary>
        /// <remarks>
        /// This uses an Enum that repensts locations in: Environment.SpecialFolder.UserProfile
        /// </remarks>
        DirectoryType RootFolder { get; }

        /// <summary>
        /// From the Root Directory, the directory for the XML Document files.
        /// </summary>
        String? DocumentDirectory { get; }

        /// <summary>
        /// Prefix to add to the front of the XML Document file name.
        /// </summary>
        String? DocumentPrefix { get; }

        /// <summary>
        /// Suffix to add to the end of the XML Document file name.
        /// </summary>
        String? DocumentSuffix { get; }

        /// <summary>
        /// Extenstion to use for the XML Document file.
        /// </summary>
        String? DocumentExtension { get; }

        /// <summary>
        /// Type of Script to generate.
        /// </summary>
        /// <remarks>Defined by an ENum and allows handling of specfic script types.</remarks>
        String? ScriptAs { get; }

        /// <summary>
        /// From the Root Directory, the directory for the Script files.
        /// </summary>
        String? ScriptDirectory { get; }

        /// <summary>
        /// Prefix  to add to the front of the Script file name.
        /// </summary>
        String? ScriptPrefix { get; }

        /// <summary>
        /// Suffix to add to the end of the Script file name.
        /// </summary>
        String? ScriptSuffix { get; }

        /// <summary>
        /// Extension to use for the Script file.
        /// </summary>
        String? ScriptExtension { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Scripting Template database operations.  
    /// </summary>  
    static class Template
    {
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Template));

        /// <summary>  
        /// The parameter name for the Scripting Template ID.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// The stored procedure used to retrieve scripting Template.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The stored procedure used to set scripting Template.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The user-defined table type for scripting Template.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}
