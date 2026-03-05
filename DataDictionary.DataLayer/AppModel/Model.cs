namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Base Model Interface (data elements only)
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Title for the Model.
        /// </summary>
       String? ModelTitle { get; set; }

        /// <summary>
        /// Description for the Model.
        /// </summary>
        String? ModelDescription { get; set; }
    }

    /// <summary>  
    /// Static class containing constants related to the Model database operations.
    /// </summary>  
    static class Model
    {
        /// <inheritdoc cref="Object.GetType"/>
        static readonly SchemaName schema = new SchemaName(typeof(Model));

        /// <summary>  
        /// The stored procedure name for retrieving a model.  
        /// </summary>  
        public readonly static String GetProcedure = schema.FullName("[procGetModel]");

        /// <summary>  
        /// The parameter name for the model ID.  
        /// </summary>  
        public readonly static String ModelId = "@ModelId";

        /// <summary>  
        /// The stored procedure name for setting a model.  
        /// </summary>  
        public readonly static String SetProcedure = schema.FullName("[procSetModel]");

        /// <summary>  
        /// The table type name for the model.  
        /// </summary>  
        public readonly static String TableType = schema.FullName("[udttModel]");
    }
}