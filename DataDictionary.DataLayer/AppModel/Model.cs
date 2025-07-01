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
        /// <summary>  
        /// The stored procedure name for retrieving a model.  
        /// </summary>  
        public const String GetProcedure = "[AppModel].[procGetModel]";

        /// <summary>  
        /// The parameter name for the model ID.  
        /// </summary>  
        public const String ModelId = "@ModelId";

        /// <summary>  
        /// The stored procedure name for setting a model.  
        /// </summary>  
        public const String SetProcedure = "[AppModel].[procSetModel]";

        /// <summary>  
        /// The table type name for the model.  
        /// </summary>  
        public const String TableType = "[AppModel].[udttModel]";
    }
}