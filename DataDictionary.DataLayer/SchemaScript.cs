using System.Reflection;

namespace DataDictionary.DataLayer
{

    /// <summary>  
    /// Provides methods to retrieve embedded SQL schema scripts from resources.  
    /// </summary>  
    public static class SchemaScript
    {

        /// <summary>  
        /// The file name of the InformationSchema SQL script.  
        /// </summary>  
        const string InfoSchemaFile = "InformationSchema.sql";

        /// <summary>  
        /// Represents the type of script supported by the schema script retrieval methods.  
        /// </summary>  
        public enum ScriptType
        {
            /// <summary>  
            /// Microsoft T-SQL script type.  
            /// </summary>  
            TSql

            // TODO: other types are not supported.  
            // Needs a lot of research to resolve this.  
        }

        /// <summary>  
        /// Gets the embedded resource file that contains the InformationSchema script.  
        /// </summary>  
        /// <param name="type">The type whose namespace and class name are used to locate the resource.</param>  
        /// <returns>The content of the InformationSchema script as a string.</returns>  
        /// <remarks>  
        /// This overload uses the default script type, which is TSql.
        /// Ensure the file is set to "Embedded Resource" in the file's property settings. 
        /// </remarks>  
        public static string GetInformationSchema(Type type)
        { return GetInformationSchema(type, ScriptType.TSql); }

        /// <summary>  
        /// Gets the embedded resource file that contains the InformationSchema script.  
        /// </summary>  
        /// <param name="type">The type whose namespace and class name are used to locate the resource.</param>  
        /// <param name="script">The type of script to retrieve, such as TSql.</param>  
        /// <returns>The content of the InformationSchema script as a string.</returns>  
        /// <remarks>  
        /// This method retrieves the embedded resource file based on the provided script type.  
        /// Ensure the file is set to "Embedded Resource" in the file's property settings.  
        /// </remarks>  
        public static string GetInformationSchema(Type type, ScriptType script)
        {

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resource = string.Format("{0}.{1}.{2}", type.FullName, Enum.GetName(script), InfoSchemaFile);

            if (assembly.GetManifestResourceNames().FirstOrDefault(w => resource.Equals(w, StringComparison.InvariantCultureIgnoreCase)) is string resourceName)
            {

                using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream is Stream)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        { return reader.ReadToEnd(); }
                    }
                    else
                    {
                        Exception ex = new ArgumentException("Resource could not be loaded.");
                        ex.Data.Add(nameof(resourceName), resourceName);

                        throw ex;
                    }
                }
            }
            else
            {
                var debug = assembly.GetManifestResourceNames();

                Exception ex = new ArgumentException("Resource not found.");
                ex.Data.Add(nameof(type), type.Namespace);
                ex.Data.Add(nameof(resource), resource);

                throw ex;
            }
        }    }
}
