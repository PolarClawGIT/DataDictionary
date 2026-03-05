namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Used to build a fully qualified object name used by SQL. 
    /// </summary>
    /// <remarks>
    /// The intent of this class is to allow classes to be moved from one NameSpace to another
    /// and have the SchemaName match the Namespace. This will reduce the number of places that
    /// code must be changed in order to change NameSpaces. Temporary overrides can be added
    /// to handle exceptions in one location, reducing copy/paste errors.
    /// </remarks>
    class SchemaName
    {
        readonly String? schemaName;

        /// <summary>
        /// Creates a SchemaName instance using the NameSpace of the type passed.
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <example>
        /// static readonly SchemaName schema = new SchemaName(typeof(Catalog));
        /// public readonly static String SetProcedure = schema.FullName("[procSetCatalog]");
        /// </example>
        public SchemaName(Type nameSpace)
        { 
            schemaName = (nameSpace.Namespace ?? String.Empty).Split('.').LastOrDefault();

            //TODO: Temporary Override for the Obsolete NameSpace.
            //if (schemaName is "Obsolete") { schemaName = "AppScript"; }
        }

        /// <summary>
        /// Builds a SQL Qualified Name using the Schema and the ObjectName passed.
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public String FullName (String objectName)
        {
            if (String.IsNullOrWhiteSpace(schemaName))
            {
                if (objectName.StartsWith('[') && objectName.EndsWith(']'))
                { return String.Format("{0}", objectName); }
                else { return String.Format("[{0}]", objectName); }
            }
            else
            {
                if (objectName.StartsWith('[') && objectName.EndsWith(']'))
                { return String.Format("[{0}].{1}", schemaName, objectName); }
                else { return String.Format("[{0}].[{1}]", schemaName, objectName); }
            }
        }
    }
}
