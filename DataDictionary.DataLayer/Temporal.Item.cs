using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer;

/// <summary>
/// Interface describes a Temporal Table
/// </summary>
public interface ITemporalItem 
{
    /// <summary>
    /// Temporal Data Elements
    /// </summary>
    public ITemporal Temporal { get; }
}

/// <summary>
/// Temporal sub-class for common functionality of Temporal data.
/// </summary>
class TemporalItem: ITemporal
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

    public override String ToString()
    {
        String temporalValue;
        temporalValue = String.Format("{0}", DbModificationEnumeration.Cast(Modification).DisplayName);

        if (IsCurrent == false)
        { temporalValue = String.Format("{0}/Historic", temporalValue); }

        if (CreatedOn is DateTime createdOn && IsDeleted == false)
        { temporalValue = String.Format("{0} on {1}", temporalValue, createdOn); }

        if (CreatedOn is DateTime removedOn && IsDeleted == true)
        { temporalValue = String.Format("{0} on {1}", temporalValue, removedOn); }

        if (CreatedBy is String createdBy && IsDeleted == false)
        { temporalValue = String.Format("{0} by {1}", temporalValue, createdBy); }

        if (CreatedBy is String removedBy && IsDeleted == true)
        { temporalValue = String.Format("{0} by {1}", temporalValue, removedBy); }

        return temporalValue;
    }
}