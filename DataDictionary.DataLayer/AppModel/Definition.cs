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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Definition));

        /// <summary>  
        /// Identifier for the Definition entity.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// Stored procedure to retrieve a Definition.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// Stored procedure to set a Definition.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// User-defined table type for Definition.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}