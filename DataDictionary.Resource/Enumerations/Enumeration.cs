using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;

//TODO: This is a prototype that appears to work. Try to implement using Scope.

/// <summary>
/// Base Interface used by Enumerations.
/// </summary>
public interface IEnumeration<TEnum , TSelf> //: IParsable<TSelf>
    where TSelf : class, IEnumeration<TEnum, TSelf> // The self referencing appears to need to be first.
    where TEnum : System.Enum
{
    /// <summary>
    /// Name of the Enumeration as it appears in the Database
    /// </summary>
    String Name { get; init; }

    /// <summary>
    /// Name of the Enumeration as it appears in the User Interface
    /// </summary>
    String DisplayName { get; init; }

    /// <summary>
    /// Enum Value of the Enumeration
    /// </summary>
    TEnum Value { get; init; }

    /// <summary>
    /// List of all values for the Enumeration
    /// </summary>
    static abstract IReadOnlyDictionary<TEnum, TSelf> Values { get; }

    /// <summary>
    /// Given the Enum, return the Enumeration. xx
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    static abstract TSelf Cast(TEnum source);
}

/// <summary>
/// Base class for Enumeration.
/// </summary>
/// <typeparam name="TEnum"></typeparam>
/// <typeparam name="TSelf"></typeparam>
public abstract class Enumeration<TEnum , TSelf> : IEnumeration<TEnum , TSelf>, IEquatable<TSelf>
        where TSelf : class, IEnumeration<TEnum , TSelf>
        where TEnum : System.Enum
{
    /// <inheritdoc />
    public String Name
    {
        get
        {
            if (String.IsNullOrWhiteSpace(nameValue))
            { return Value.ToString(); }
            else { return nameValue; }
        }
        init { nameValue = value; }
    }
    private String nameValue = String.Empty; // Backing field for Name

    /// <inheritdoc />
    public String DisplayName
    {
        get
        {
            if (String.IsNullOrWhiteSpace(displayValue))
            { return Value.ToString(); }
            else { return displayValue; }
        }
        init { displayValue = value; }
    }
    private String displayValue = String.Empty; // Backing field for DisplayName

    /// <inheritdoc />
    public TEnum Value { get; init; } = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().First();

    /// <summary>
    /// Base constructor for Enumeration
    /// </summary>
    /// <remarks></remarks>
    protected Enumeration() : base() { }

    /// <summary>
    /// Base constructor for Enumeration
    /// </summary>
    /// <param name="value"></param>
    protected Enumeration(TEnum source) : this()
    {
        Value = source;
        Name = source.ToString();
        DisplayName = source.ToString();
    }

    /// <summary>
    /// This is the container of the data for the Enumeration.
    /// </summary>
    /// <remarks>This MUST be overridden (using New) in child classes to load the data.</remarks>
    /// <example>
    /// class Example : Enumeration<ScopeType, Example>
    /// {
    ///    protected static new List<Example> Data = new List<Example>()
    ///    { new Example(), };
    /// }
    /// </example>
    protected static List<TSelf> Data = new List<TSelf>();

    /// <summary>
    /// Used to combine the Data with the Enum and produce a static dictionary.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">The Data field is missing an entry found in the Enum</exception>
    protected static Dictionary<TEnum, TSelf> BuildDictionary()
    {
        Dictionary<TEnum, TSelf> result = new Dictionary<TEnum, TSelf>();

        //TODO: Issue, Data is not the child types data. It is this data.
        var x = Data; // Not the object I expected.

        foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
        {
            if (Data.FirstOrDefault(w => w.Value.Equals(item)) is TSelf value)
            { result.Add(item, value); }
            else
            {
                Exception ex = new ArgumentOutOfRangeException("Entry for Enum is missing");
                ex.Data.Add(typeof(TEnum).Name, item.ToString());
                throw ex;
            }
        }

        return result;
    }

    /// <summary>
    /// List of all values for the Enumeration
    /// </summary>
    public static IReadOnlyDictionary<TEnum, TSelf> Values { get; } = BuildDictionary();

    /// <inheritdoc />
    public static TSelf Cast(TEnum source)
    { return Values[source]; }

    #region IParsable
    // This implements a IParsable<TSelf>.
    // CA2260 prevent this from being declared.

    /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)" />
    public static TSelf Parse(String source, IFormatProvider? format)
    {
        if (TSelf.Values.Values.FirstOrDefault(w => String.Equals(source, w.Name, StringComparison.OrdinalIgnoreCase)) is TSelf item)
        { return item; }
        else
        {
            Exception ex = new ArgumentException("Could not parse value", nameof(source));
            ex.Data.Add(nameof(source), source);
            throw ex;
        }
    }

    /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)" />
    public static Boolean TryParse([NotNullWhen(true)] String? source, IFormatProvider? format, [MaybeNullWhen(false)] out TSelf result)
    {
        if (TSelf.Values.Values.FirstOrDefault(w => String.Equals(source, w.Name, StringComparison.OrdinalIgnoreCase)) is TSelf item)
        { result = item; return true; }
        else { result = null; return false; }
    }
    #endregion
    #region IEquatable
    /// <inheritdoc />
    public virtual Boolean Equals(TSelf? other)
    { return other is TSelf && this.Value.Equals(other.Value); }

    /// <inheritdoc />
    public virtual Boolean Equals(TEnum? other)
    { return other is TEnum && this.Value.Equals(other); }

    /// <inheritdoc />
    public override Boolean Equals(Object? obj)
    { return obj is TSelf other && this.Value.Equals(other.Value); }

    /// <inheritdoc/>
    public static Boolean operator ==(Enumeration<TEnum , TSelf> left, Enumeration<TEnum , TSelf> right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(Enumeration<TEnum , TSelf> left, Enumeration<TEnum , TSelf> right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return Value.GetHashCode(); }
    #endregion

    /// <inheritdoc />
    public override String ToString()
    { return Name; }


}
