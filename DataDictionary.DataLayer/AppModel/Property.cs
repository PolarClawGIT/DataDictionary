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

    static class Property
    {
        public const String PropertyId = "@PropertyId";
        public const String GetProcedure = "[AppModel].[procGetProperty]";
        public const String SetProcedure = "[AppModel].[procSetProperty]";
        public const String TableType = "[AppModel].[udttProperty]";
    }
}