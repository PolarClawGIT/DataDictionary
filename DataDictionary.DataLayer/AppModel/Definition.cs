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

    static class Definition
    {
        public const String DefinitionId = "@DefinitionId";
        public const String GetProcedure = "[AppModel].[procGetDefinition]";
        public const String SetProcedure = "[AppModel].[procSetDefinition]";
        public const String TableType = "[AppModel].[udttDefinition]";
    }
}