using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer;

/// <summary>
/// Interface describes a Temporal Table
/// </summary>
public interface ITemporalItem
{
    /// <summary>
    /// Account Name that Modified this record
    /// </summary>
    String? ModifiedBy { get; }

    /// <summary>
    /// Date (Local) that the record was Modified
    /// </summary>
    DateTime? ModifiedOn { get; }

    /// <summary>
    /// Type of Modification made.
    /// </summary>
    DbModificationType Modification { get; }
}
