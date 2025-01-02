using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Base Temporal Interface (data elements only)
    /// </summary>
    public interface ITemporal
    {
        /// <summary>
        /// Date on which the record was removed (Update/Delete)
        /// </summary>
        DateTime? CreatedOn { get; }

        /// <summary>
        /// Account Name that Created (Insert/Update) this record
        /// </summary>
        String? CreatedBy { get; }

        /// <summary>
        /// Date on which the record was removed (Update/Delete)
        /// </summary>
        DateTime? RemovedOn { get; }

        /// <summary>
        /// Account Name that removed (Update/Delete) this record
        /// </summary>
        String? RemovedBy { get; }

        /// <summary>
        /// The value was modified by an Insert action
        /// </summary>
        Boolean? IsInserted { get; }

        /// <summary>
        /// The value was modified by an Update action
        /// </summary>
        Boolean? IsUpdated { get; }

        /// <summary>
        /// The value was Deleted
        /// </summary>
        Boolean? IsDeleted { get; }

        /// <summary>
        /// The value is the current/last state
        /// </summary>
        Boolean? IsCurrent { get; }

        /// <summary>
        /// Type of Modification made.
        /// </summary>
        DbModificationType Modification { get; }
    }
}