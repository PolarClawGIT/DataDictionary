using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations;
/// <summary>
/// Base Interface used by Enumerations.
/// </summary>
public interface IEnumeration<TEnum, TSelf> //: IParsable<TSelf>
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
    static abstract IReadOnlyDictionary<TEnum, TSelf> Members { get; }

    /// <summary>
    /// Given the Enum, return the Enumeration.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"/>
    static abstract TSelf Cast(TEnum source);
}

/// <summary>
/// Base class for Enumeration.
/// </summary>
/// <typeparam name="TEnum"></typeparam>
/// <typeparam name="TSelf"></typeparam>
public abstract class Enumeration<TEnum, TSelf> : IEnumeration<TEnum, TSelf>, IEquatable<TSelf>
        where TSelf : class, IEnumeration<TEnum, TSelf>
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
    /// Static base constructor of the Enumeration classes.
    /// </summary>
    /// <remarks>
    /// C# does not support "Curiously recurring template pattern".
    /// The code forces derived classes static constructor to be executed.
    /// Each of the derived classes static constructor load the Enumeration specific data into the Values list.
    /// This is done using the BuildDictionary method.
    /// * Search term: Derived Class Static Constructor Not Invoked
    /// </remarks>
    /// <see href="https://en.wikipedia.org/wiki/Curiously_recurring_template_pattern"/>
    /// <see href="https://chrisoldwood.blogspot.com/2014/11/derived-class-static-constructor-not.html"/>
    static Enumeration()
    {

        //This forces the derived class execute the static constructor at run time.
        System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(TSelf).TypeHandle);
    }


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
    /// Base constructor for Enumeration
    /// </summary>
    /// <param name="source"></param>
    /// <param name="name"></param>
    protected Enumeration(TEnum source, String name) : this(source)
    {
        Name = name;
        DisplayName = name;
    }

    /// <summary>
    /// Helper Method to build the Enumeration Dictionary. 
    /// </summary>
    /// <param name="data"></param>
    /// <remarks>
    /// This does not detect missing entires in the incoming data.
    /// Use Missing Values method to get a list of any values that are missing.
    /// Only distinct Values (by the enum) are loaded.
    /// </remarks>
    protected static void BuildDictionary(IEnumerable<TSelf> data)
    {
        Dictionary<TEnum, TSelf> result = new Dictionary<TEnum, TSelf>();

        foreach (TSelf item in data.DistinctBy(d => d.Value))
        { enumerationValues.Add(item.Value, item); }
    }

    /// <summary>
    /// The list in the Values is cross referenced against the Enum. Any missing Enum's are returned.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// This is mostly for debugging.
    /// </remarks>
    public static IEnumerable<TEnum> MissingValues()
    { return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Except(Members.Values.Select(s => s.Value)); }

    /// <summary>
    /// List of all values for the Enumeration
    /// </summary>
    /// <remarks>Use BuildDictionary to add values to the list.</remarks>
    public static IReadOnlyDictionary<TEnum, TSelf> Members
    { get { return enumerationValues; } }
    private static Dictionary<TEnum, TSelf> enumerationValues = new Dictionary<TEnum, TSelf>();

    /// <inheritdoc />
    public static TSelf Cast(TEnum source)
    { return Members[source]; }

    #region IParsable
    // This implements a IParsable<TSelf>.
    // CA2260 prevent IParsable from being declared.

    /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)" />
    public static TSelf Parse(String source, IFormatProvider? format)
    {
        if (TSelf.Members.Values.FirstOrDefault(w => String.Equals(source, w.Name, StringComparison.OrdinalIgnoreCase)) is TSelf item)
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
        if (Members is null) { result = null; return false; }

        if (TSelf.Members.Values.FirstOrDefault(w => String.Equals(source, w.Name, StringComparison.OrdinalIgnoreCase)) is TSelf item)
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
    public static Boolean operator ==(Enumeration<TEnum, TSelf> left, Enumeration<TEnum, TSelf> right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(Enumeration<TEnum, TSelf> left, Enumeration<TEnum, TSelf> right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override int GetHashCode()
    { return Value.GetHashCode(); }
    #endregion

    /// <inheritdoc />
    public override String ToString()
    { return Name; }


}
