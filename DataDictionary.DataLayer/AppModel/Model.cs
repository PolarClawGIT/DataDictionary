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

    static class Model
    {
        public const String GetProcedure = "[AppModel].[procGetModel]";
        public const String ModelId = "@ModelId";
        public const String SetProcedure = "[AppModel].[procSetModel]";
        public const String TableType = "[AppModel].[udttModel]";
    }
}