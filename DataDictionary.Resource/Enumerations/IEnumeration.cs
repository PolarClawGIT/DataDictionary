using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Common components of Enumeration support class
    /// </summary>
    /// <typeparam name="TEnum">The Enum that this class is based on</typeparam>
    /// <typeparam name="TSelf">This Enumeration</typeparam>
    public interface IEnumeration<TEnum, TSelf> : IEquatable<TSelf>//, IParsable<TSelf>
        where TEnum : System.Enum
        where TSelf : class, IEnumeration<TEnum, TSelf>
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
        /// Dictionary of the Enumeration (the List)
        /// </summary>
        /// <remarks>For a Single value, use Cast method instead.</remarks>
        static abstract IReadOnlyDictionary<TEnum, TSelf> Values { get; }

        /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
        static abstract TSelf Parse(String s, IFormatProvider? provider);

        /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
        static abstract Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result);


        #region IParsable<TSelf> Helpers
        /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
        /// <remarks>Used by IParsable</remarks>
        static TSelf Parse(String source)
        {
            if (TryParse(source, out TSelf? result) && result is TSelf)
            { return result; }
            else
            {
                Exception ex = new ArgumentException("Could not parse value", nameof(source));
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }

        /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
        /// <remarks>Used by IParsable</remarks>
        static Boolean TryParse([NotNullWhen(true)] String? source, [MaybeNullWhen(false)] out TSelf? result)
        {
            if (TSelf.Values.Values.FirstOrDefault(w => String.Equals(source, w.Name, StringComparison.OrdinalIgnoreCase)) is TSelf item)
            { result = item; return true; }
            else { result = null; return false; }
        }
        #endregion

        #region Cast Helpers
        // TODO: What is desired is a: static virtual.
        // That always results in a CS8926 with no way to call the interface method.
        // Ideally, the method would be available to all classes that implement this
        // interface without coding the exact same code in every class.
        //
        // This case a static indexer would have worked but do not exist in C#.
        // This is the best option I could find.
        // These methods can be used with implicit operators, as written.

        /// <summary>
        /// Convert the Enum to the Enumeration
        /// </summary>
        /// <param name="source"></param>
        static TSelf Cast(TEnum source)
        {
            if (TSelf.Values.ContainsKey(source)) { return TSelf.Values[source]; }
            else
            {
                Exception ex = new ArgumentException("Not Defined", nameof(source));
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }
        #endregion
    }
}
