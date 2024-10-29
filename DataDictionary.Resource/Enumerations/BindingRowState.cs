namespace DataDictionary.Resource.Enumerations;

/// <inheritdoc cref="System.Data.DataRowState" />
/// <remarks>Extended base enumeration to cover special conditions</remarks>
[Flags]
public enum BindingRowState
{
    /// <summary>
    /// Null, Unknown or unspecified RowState
    /// </summary>
    Null = 0,

    /// <inheritdoc cref="System.Data.DataRowState.Detached" />
    Detached = 1,

    /// <inheritdoc cref="System.Data.DataRowState.Unchanged" />
    Unchanged = 2,

    /// <inheritdoc cref="System.Data.DataRowState.Added" />
    Added = 4,

    /// <inheritdoc cref="System.Data.DataRowState.Deleted" />
    Deleted = 8,

    /// <inheritdoc cref="System.Data.DataRowState.Modified" />
    Modified = 16,

    /// <summary>
    /// Data is Historic (from Temporal Table structure)
    /// </summary>
    Historic = 32,
}
