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
        static readonly DatabaseNaming dataObject = new DatabaseNaming(typeof(Model));

        /// <summary>  
        /// The stored procedure name for retrieving a model.  
        /// </summary>  
        public readonly static String GetProcedure = dataObject.GetProcedure;

        /// <summary>  
        /// The parameter name for the model ID.  
        /// </summary>  
        public readonly static String Identifier = dataObject.Identifier;

        /// <summary>  
        /// The stored procedure name for setting a model.  
        /// </summary>  
        public readonly static String SetProcedure = dataObject.SetProcedure;

        /// <summary>  
        /// The table type name for the model.  
        /// </summary>  
        public readonly static String TableType = dataObject.TableType;
    }
}