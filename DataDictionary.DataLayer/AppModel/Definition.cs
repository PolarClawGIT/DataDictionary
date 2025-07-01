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
        /// <summary>  
        /// Identifier for the Definition entity.  
        /// </summary>  
        public const String DefinitionId = "@DefinitionId";

        /// <summary>  
        /// Stored procedure to retrieve a Definition.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetDefinition]";

        /// <summary>  
        /// Stored procedure to set a Definition.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetDefinition]";

        /// <summary>  
        /// User-defined table type for Definition.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttDefinition]";
    }
}