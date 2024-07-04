using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

/// <summary>
/// Enumeration support class for Database Table type.
/// </summary>
public class DbTableEnumeration : IEnumeration<DbTableType, DbTableEnumeration>
{
    /// <inheritdoc />
    public required DbTableType Value { get; init; }

    /// <inheritdoc />
    public required String Name { get; init; }

    /// <inheritdoc />
    public required String DisplayName { get; init; }

    /// <summary>
    /// Internal Constructor for Database Table Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DbTableEnumeration() : base() { }

    /// <summary>
    /// Internal list that contains all the values.
    /// </summary>
    static readonly List<DbTableEnumeration> values = new List<DbTableEnumeration>()
    {
        new DbTableEnumeration() { Value = DbTableType.Null, Name = String.Empty, DisplayName = "not defined" },
        new DbTableEnumeration() { Value = DbTableType.Table, Name = "Table", DisplayName = "Table" },
        new DbTableEnumeration() { Value = DbTableType.TemporalTable, Name = "Temporal Table", DisplayName = "Temporal Table" },
        new DbTableEnumeration() { Value = DbTableType.HistoryTable, Name = "History Table", DisplayName = "History Table" },
        new DbTableEnumeration() { Value = DbTableType.View, Name = "View", DisplayName = "View" },
    };

    /// <inheritdoc />
    public static IReadOnlyDictionary<DbTableType, DbTableEnumeration> AsDictionary { get { return values.ToDictionary(d => d.Value); } }

    /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
    public static DbTableEnumeration Cast(DbTableType source)
    { return IEnumeration<DbTableType, DbTableEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static DbTableEnumeration Parse(String s, IFormatProvider? provider)
    { return IEnumeration<DbTableType, DbTableEnumeration>.Parse(s); }

    /// <inheritdoc />
    public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DbTableEnumeration result)
    { return IEnumeration<DbTableType, DbTableEnumeration>.TryParse(s, out result); }

    /// <inheritdoc />
    public Boolean Equals(DbTableEnumeration? other)
    { return other is DbTableEnumeration && Value.Equals(other.Value); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is DbTableEnumeration value && Equals(other); }

    /// <inheritdoc/>
    public static Boolean operator ==(DbTableEnumeration left, DbTableEnumeration right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbTableEnumeration left, DbTableEnumeration right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return HashCode.Combine(Value); }

    public override String ToString()
    { return Name; }
}
