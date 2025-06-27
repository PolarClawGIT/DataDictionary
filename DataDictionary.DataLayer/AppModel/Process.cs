namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Process
    /// </summary>
    public interface IProcess : IProcessKeyName, IProcessSubjectAreaName
    {
        /// <summary>
        /// Description of the Domain Process
        /// </summary>
        String? ProcessDescription { get; set; }
    }

    static class Process
    {
        public const String GetProcedure = "[AppModel].[procGetProcess]";
        public const String ProcessId = "@ProcessId";
        public const String SetProcedure = "[AppModel].[procSetProcess]";
        public const String TableType = "[AppModel].[udttProcess]";
    }
}