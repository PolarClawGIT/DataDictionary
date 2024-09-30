using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer;

/// <summary>
/// Interface describes a Temporal Table
/// </summary>
public interface ITemporalItem: ITemporalKey
{
    /// <summary>
    /// Account Name that Modified this record
    /// </summary>
    String? ModifiedBy { get; }

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
