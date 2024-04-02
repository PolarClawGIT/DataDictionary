namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// List of supported Routine Types.
    /// </summary>
    public enum DbRoutineType
    {
        /// <summary>
        /// Unknown Routine Type
        /// </summary>
        Null,

        /// <summary>
        /// SQL Function
        /// </summary>
        Function,

        /// <summary>
        /// SQL Procedure
        /// </summary>
        Procedure,
    }

    /// <summary>
    /// Interface for Database RoutineType Key.
    /// </summary>
    public interface IDbRoutineType : IKey
    {
        /// <summary>
        /// Type of Routine (such as procedure or function)
        /// </summary>
        DbRoutineType RoutineType { get; }
    }
}

