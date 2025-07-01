namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Property Sub Type.
    /// </summary>
    public interface IProperty : IPropertyKey
    {
        /// <summary>
        /// Property Value. Type and Format is dependent on PeropertyId.
        /// </summary>
        public String? PropertyValue { get; }
    }

    /// <summary>
    /// Static class containing constants related to the Property database operations.
    /// </summary>
    static class Property
    {
        /// <summary>
        /// Identifier for the property. Used as a parameter in database operations.
        /// </summary>
        public const String PropertyId = "@PropertyId";

        /// <summary>
        /// Name of the stored procedure to retrieve property data.
        /// </summary>
        public const String GetProcedure = "[AppModel].[procGetProperty]";

        /// <summary>
        /// Name of the stored procedure to set property data.
        /// </summary>
        public const String SetProcedure = "[AppModel].[procSetProperty]";

        /// <summary>
        /// Name of the user-defined table type for property data.
        /// </summary>
        public const String TableType = "[AppModel].[udttProperty]";
    }
}