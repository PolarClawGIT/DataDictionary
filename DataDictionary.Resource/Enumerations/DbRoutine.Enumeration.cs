namespace DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Enumeration support class for Database Routine type.
/// </summary>
public class DbRoutineEnumeration : IEnumeration<DbRoutineType, DbRoutineEnumeration>
{
    /// <inheritdoc />
    public required DbRoutineType Value { get; init; }

    /// <inheritdoc />
    public required String Name { get; init; }

    /// <inheritdoc />
    public required String DisplayName { get; init; }

    /// <summary>
    /// Internal Constructor for Database Routine Enumeration
    /// </summary>
    /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
    DbRoutineEnumeration() : base() { }

    /// <summary>
    /// Internal Dictionary that contains all the values.
    /// </summary>
    static readonly Dictionary<DbRoutineType, DbRoutineEnumeration> values = new Dictionary<DbRoutineType, DbRoutineEnumeration>()
        {
            { DbRoutineType.Null, new DbRoutineEnumeration() { Value = DbRoutineType.Null, Name = String.Empty, DisplayName = "not defined" } },
            { DbRoutineType.Function, new DbRoutineEnumeration() { Value = DbRoutineType.Function, Name = "Function", DisplayName = "Function" } },
            { DbRoutineType.Procedure, new DbRoutineEnumeration() { Value = DbRoutineType.Procedure, Name = "Procedure", DisplayName = "Procedure" } },
        };

    /// <inheritdoc />
    public static IReadOnlyDictionary<DbRoutineType, DbRoutineEnumeration> AsDictionary { get { return values; } }

    /// <inheritdoc />
    public static implicit operator DbRoutineEnumeration(DbRoutineType source)
    { return IEnumeration<DbRoutineType, DbRoutineEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static implicit operator DbRoutineType(DbRoutineEnumeration source)
    { return IEnumeration<DbRoutineType, DbRoutineEnumeration>.Cast(source); }

    /// <inheritdoc />
    public static DbRoutineEnumeration Parse(String s, IFormatProvider? provider)
    { return IEnumeration<DbRoutineType, DbRoutineEnumeration>.Parse(s); }

    /// <inheritdoc />
    public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DbRoutineEnumeration result)
    { return IEnumeration<DbRoutineType, DbRoutineEnumeration>.TryParse(s, out result); }

    /// <inheritdoc />
    public Boolean Equals(DbRoutineEnumeration? other)
    { return other is DbRoutineEnumeration && Value.Equals(other.Value); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is DbRoutineEnumeration value && Equals(other); }

    /// <inheritdoc/>
    public static Boolean operator ==(DbRoutineEnumeration left, DbRoutineEnumeration right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbRoutineEnumeration left, DbRoutineEnumeration right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return HashCode.Combine(Value); }

    public override String ToString()
    { return Name; }
}
