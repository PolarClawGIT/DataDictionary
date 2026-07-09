namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Interface for DbConstraintType
    /// </summary>
    public interface IDbConstraintType
    {
        /// <summary>
        /// Type of Constraint (Primary Key, Unique Key, Foreign Key, ...)
        /// </summary>
        DbConstraintType ConstraintType { get; }
    }

    /// <summary>
    /// List of supported Constraint Types.
    /// </summary>
    public enum DbConstraintType
    {
        /// <summary>
        /// Unknown Constraint Type
        /// </summary>
        Null,

        /// <summary>
        /// Check Constraint
        /// </summary>
        Check,

        /// <summary>
        /// Unique Key Constraint
        /// </summary>
        Unique,

        /// <summary>
        /// Primary Key Constraint
        /// </summary>
        PrimaryKey,

        /// <summary>
        /// Foreign Key Constraint
        /// </summary>
        ForeignKey,
    }
}