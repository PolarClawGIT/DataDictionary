namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Entity
    /// </summary>
    public interface IEntity: IEntityKeyName, IEntitySubjectAreaName
    {
        /// <summary>
        /// Description of the Domain Entity
        /// </summary>
        String? EntityDescription { get; set; }
    }

    static class Entity
    {
        public const String EntityId = "@EntityId";
        public const String GetProcedure = "[AppModel].[procGetEntity]";
        public const String SetProcedure = "[AppModel].[procSetEntity]";
        public const String TableType = "[AppModel].[udttEntity]";
    }
}
