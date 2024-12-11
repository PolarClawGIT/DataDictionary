using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer;

/// <summary>
/// Interface describes a Temporal Table
/// </summary>
public interface ITemporalItem : ITemporalKey
{
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

/// <summary>
/// Temporal sub-class for common functionality of Temporal data.
/// </summary>
class TemporalItem: ITemporalItem
{
    /// <inheritdoc cref="BindingTableRow.GetValue(string)"/>
    /// <remarks>Pass: BindingTableRow.GetValue</remarks>
    public required Func<String,String?> GetString { get; init; }

    /// <inheritdoc cref="BindingTableRow.GetValue{T}(string)"/>
    /// <remarks><![CDATA[Pass: BindingTableRow.GetValue<DateTime>]]></remarks>
    public required Func<String, DateTime?> GetDate { get; init; }

    /// <inheritdoc cref="BindingTableRow.GetValue{T}(string, BindingTableRow.tryParseDelegate{T})"/>
    /// <remarks><![CDATA[Pass: BindingTableRow.GetValue<Boolean>(name, BindingItemParsers.BooleanTryParse)]]></remarks>
    public required Func<String, Boolean?> GetBoolean { get; init; }

    /// <inheritdoc/>
    public String? CreatedBy { get { return GetString(nameof(CreatedBy)); } }

    /// <inheritdoc/>
    public DateTime? CreatedOn
    {
        get
        {
            DateTime? value = GetDate(nameof(CreatedOn));
            if (value is DateTime baseDate)
            { return TimeZoneInfo.ConvertTimeFromUtc(baseDate, TimeZoneInfo.Local); }
            else { return null; }
        }
    }

    /// <inheritdoc/>
    public String? RemovedBy { get { return GetString(nameof(RemovedBy)); } }

    /// <inheritdoc/>
    public DateTime? RemovedOn
    {
        get
        {
            DateTime? value = GetDate(nameof(RemovedOn));
            if (value is DateTime baseDate)
            { return TimeZoneInfo.ConvertTimeFromUtc(baseDate, TimeZoneInfo.Local); }
            else { return null; }
        }
    }

    /// <inheritdoc/>
    public Boolean? IsInserted
    { get { return GetBoolean(nameof(IsInserted)); } }

    /// <inheritdoc/>
    public Boolean? IsUpdated
    { get { return GetBoolean(nameof(IsUpdated)); } }

    /// <inheritdoc/>
    public Boolean? IsDeleted
    { get { return GetBoolean(nameof(IsDeleted)); } }

    /// <inheritdoc/>
    public Boolean? IsCurrent
    { get { return GetBoolean(nameof(IsCurrent)); } }

    /// <inheritdoc/>
    public DbModificationType Modification
    {
        get
        {
            if (IsDeleted == true) { return DbModificationType.Deleted; }
            else if (IsInserted == true) { return DbModificationType.Inserted; }
            else if (IsUpdated == true) { return DbModificationType.Updated; }
            else { return DbModificationType.Null; }
        }
    }

    /// <summary>
    /// List of Temporal Columns
    /// </summary>
    public static readonly IReadOnlyList<DataColumn> columnDefinitions =
        [
            new DataColumn(nameof(CreatedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(CreatedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedOn), typeof(DateTime)){ AllowDBNull = true},
            new DataColumn(nameof(RemovedBy), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsInserted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsUpdated), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsDeleted), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsCurrent), typeof(Boolean)){ AllowDBNull = true},
        ];
}