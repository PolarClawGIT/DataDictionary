using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

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
    /// Internal list that contains all the values.
    /// </summary>
    static readonly List<DbRoutineEnumeration> values = new List<DbRoutineEnumeration>() 
    {
        new DbRoutineEnumeration() { Value = DbRoutineType.Null, Name = String.Empty, DisplayName = "not defined" },
        new DbRoutineEnumeration() { Value = DbRoutineType.Function, Name = "Function", DisplayName = "Function" },
        new DbRoutineEnumeration() { Value = DbRoutineType.Procedure, Name = "Procedure", DisplayName = "Procedure" },
    };

    /// <inheritdoc />
    public static IReadOnlyDictionary<DbRoutineType, DbRoutineEnumeration> Values { get { return values.ToDictionary(d => d.Value); } }

    /// <inheritdoc cref="IEnumeration{TEnum, TSelf}.Cast(TEnum)" />
    public static DbRoutineEnumeration Cast(DbRoutineType source)
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
