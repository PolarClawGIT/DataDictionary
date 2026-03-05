namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>  
    /// Interface for the Model Definition Sub Type  
    /// </summary>  
    public interface IDefinition : IDefinitionKey
    {
        /// <summary>  
        /// Definition Summary (Plain Text, limited length)  
        /// </summary>  
        String? DefinitionSummary { get; }

        /// <summary>  
        /// Definition Text (Rich Text)  
        /// </summary>  
        String? DefinitionText { get; }
    }

    /// <summary>  
    /// Static class containing constants related to the Definition database operations.  
    /// </summary>  
    static class Definition
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Definition));

        /// <summary>  
        /// Identifier for the Definition entity.  
        /// </summary>  
        public readonly static String DefinitionId = "@DefinitionId";

        /// <summary>  
        /// Stored procedure to retrieve a Definition.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetDefinition]");

        /// <summary>  
        /// Stored procedure to set a Definition.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetDefinition]");

        /// <summary>  
        /// User-defined table type for Definition.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttDefinition]");
    }
}