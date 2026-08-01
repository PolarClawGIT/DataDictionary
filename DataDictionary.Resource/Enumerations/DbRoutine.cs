namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for DbRoutineType
    /// </summary>
    public interface IDbRoutineType
    {
        /// <summary>
        /// Type of Routine (such as procedure or function)
        /// </summary>
        DbRoutineType RoutineType { get; }
    }

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
}