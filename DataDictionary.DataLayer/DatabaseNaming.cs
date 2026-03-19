namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Used to build fully qualified object SQL Names based on naming practices used by this application.
    /// </summary>
    /// <remarks>
    /// The intent of this class is to reduce copy/paste errors when building classes.
    /// It does this by building names based on the NameSpace and Object Name of
    /// the class that is passed to the constructor. The names built follow
    /// specific naming conventions and how they relate between the Database
    /// and the Database Layer of the application.<br/>
    /// <br/>
    /// Using the Init on the properties overrides the default naming behavior and uses what is passed.<br/>
    /// This can then be used as a base class to override behaviors for specific namespaces.<br/>
    /// </remarks>
    class DatabaseNaming
    {
        protected readonly String? schemaName;
        protected readonly String objectName;

        /// <summary>
        /// Uses the Naming Conventions to return the name of the Get Stored Procedure.<br/>
        /// [{schemaName}].[procGet{objectName}]
        /// </summary>
        public virtual String GetProcedure
        {
            get
            {
                if (String.IsNullOrWhiteSpace(field))
                {
                    if (String.IsNullOrWhiteSpace(schemaName))
                    { return String.Format("[procGet{0}]", objectName); }
                    else
                    { return String.Format("[{0}].[procGet{1}]", schemaName, objectName); }
                }
                else { return field; }
            }
            init;
        }

        /// <summary>
        /// Uses the Naming Conventions to return the name of the Set Stored Procedure.<br/>
        /// [{schemaName}].[procSet{objectName}]
        /// </summary>
        public virtual String SetProcedure
        {
            get
            {
                if (String.IsNullOrWhiteSpace(field))
                {
                    if (String.IsNullOrWhiteSpace(schemaName))
                    { return String.Format("[procSet{0}]", objectName); }
                    else
                    { return String.Format("[{0}].[procSet{1}]", schemaName, objectName); }
                }
                else { return field; }
            }
            init;
        }

        /// <summary>
        /// Uses the Naming Conventions to return the name of the User Defined Table Type.<br/>
        /// [{schemaName}].[udtt{objectName}]
        /// </summary>
        public virtual String TableType
        {
            get
            {
                if (String.IsNullOrWhiteSpace(field))
                {
                    if (String.IsNullOrWhiteSpace(schemaName))
                    { return String.Format("[udtt{0}]", objectName); }
                    else
                    { return String.Format("[{0}].[udtt{1}]", schemaName, objectName); }
                }
                else { return field; }
            }
            init;
        }

        /// <summary>
        /// Uses the Naming Conventions to return the name of the parameter used as the Identifier in stored procedure calls.<br/>
        /// @{objectName}Id 
        /// </summary>
        public virtual String Identifier
        {
            get
            {
                if (String.IsNullOrWhiteSpace(field))
                { return String.Format("@{0}Id", objectName); }
                else { return field; }
            }
            init;
        }

        /// <summary>
        /// Creates a SchemaName instance using the NameSpace of the type passed.
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <example>
        /// static readonly SchemaName schema = new SchemaName(typeof(Catalog));
        /// public readonly static String SetProcedure = schema.SetProcedure;
        /// </example>
        public DatabaseNaming(Type nameSpace) : base()
        {
            schemaName = (nameSpace.Namespace ?? String.Empty).Split('.').LastOrDefault();
            objectName = nameSpace.Name;
        }

        /// <summary>
        /// Creates a SchemaName instance using the hard coded schema and object names.
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="objectName"></param>
        /// <remarks>This is intended to handle override situations
        /// when the Schema Name does not match the Namespace and/or
        /// when the Object Name does not match the Class.
        /// </remarks>
        public DatabaseNaming(String schemaName, String objectName): base()
        {
            this.schemaName = schemaName;
            this.objectName = objectName;
        }
    }
}

