using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        static abstract IReadOnlyDictionary<TEnum, TSelf> AsDictionary { get; }

        /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
        static abstract TSelf Parse(String s, IFormatProvider? provider);

        /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
        static abstract Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TSelf result);

        /// <summary>
        /// Convert the Enum to the Enumeration
        /// </summary>
        /// <param name="source"></param>
        static abstract implicit operator TSelf(TEnum source);

        /// <summary>
        /// Convert the Enumeration to the Enum
        /// </summary>
        /// <param name="source"></param>
        static abstract implicit operator TEnum(TSelf source);

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
            if (TSelf.AsDictionary.Values.FirstOrDefault(w => String.Equals(source, w.Name, StringComparison.OrdinalIgnoreCase)) is TSelf item)
            { result = item; return true; }
            else { result = null; return false; }
        }
        #endregion

        #region Cast Helpers
        /// <summary>
        /// Convert the Enum to the Enumeration
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>Used for implicit operator</remarks>
        static TSelf Cast(TEnum source)
        {
            if (TSelf.AsDictionary.ContainsKey(source)) { return TSelf.AsDictionary[source]; }
            else
            {
                Exception ex = new ArgumentException("Not Defined", nameof(source));
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }

        /// <summary>
        /// Convert the Enumeration to the Enum
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>Used for implicit operator</remarks>
        static TEnum Cast(TSelf source)
        { return source.Value; }
        #endregion

    }
}
