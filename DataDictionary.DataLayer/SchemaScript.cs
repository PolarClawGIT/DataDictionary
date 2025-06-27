using System.Reflection;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Helper class for dealing with InformationSchema
    /// </summary>
    static class SchemaScript
    {
        /// <summary>
        /// Base name of the Resource file used for InformationSchema.
        /// </summary>
        const String InfoSchemaFile = "InformationSchema.sql";

        public enum ScriptType
        {
            /// <summary>
            /// MS TSQL Script
            /// </summary>
            TSql

            // TODO: other types are not supported.
            // Needs a lot of research to resolve this.
        }

        /// <summary>
        /// Gets the Embedded Resource file that contains the InformationSchema script
        /// using the default script type of TSQL.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>
        /// This can be sensitive to name changes.
        ///   Pattern: {namespace}.{className}.{scriptType}.InformationSchema.sql
        /// Example:
        ///   File name: Catalog.TSql.InformationSchema.sql
        ///   Resource name: DataDictionary.DataLayer.AppCatalog.Catalog.TSql.InformationSchema.sql
        /// Be sure the file is set to "Embedded Resource" in the files property settings.
        /// </remarks>
        public static String GetInformationSchema(Type type)
        { return GetInformationSchema(type, ScriptType.TSql); }

        /// <summary>
        /// Gets the Embedded Resource file that contains the InformationSchema script.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        /// <remarks>
        /// This can be sensitive to name changes.
        ///   Pattern: {namespace}.{className}.{scriptType}.{baseFile}
        /// Example:
        ///   File name: Catalog.TSql.InformationSchema.sql
        ///   Resource name: DataDictionary.DataLayer.AppCatalog.Catalog.TSql.InformationSchema.sql
        /// </remarks>
        public static String GetInformationSchema(Type type, ScriptType script)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            String resource = String.Format("{0}.{1}.{2}", type.FullName, Enum.GetName(script), InfoSchemaFile);

            if (assembly.GetManifestResourceNames().FirstOrDefault(w => resource.Equals(w, StringComparison.InvariantCultureIgnoreCase)) is String resourceName)
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
                        Exception ex = new ArgumentException("resource could not be loaded");
                        ex.Data.Add(nameof(resourceName), resourceName);
                        throw ex;
                    }
                }
            }
            else
            {
                var debug = assembly.GetManifestResourceNames();

                Exception ex = new ArgumentException("resource not found");
                ex.Data.Add(nameof(type), type.Namespace);
                ex.Data.Add(nameof(resource), resource);
                throw ex;
            }
        }
    }
}
