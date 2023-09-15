using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Interface common to all Keys.
    /// </summary>
    /// <remarks>
    /// Use this for the interface for Keys and Items.
    /// The implementation of the key should use one of the child interfaces.
    /// Keys use full classes, instead of record or structure.
    /// This allows the implementation of IEquatable and inheritance restricted by the other types.
    /// It is the intention that the Key is implemented with Immutability in mind.
    /// </remarks>
    public interface IKey { }

    /// <summary>
    /// Interface common to all Keys with Equality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Use this in the implementation of the Key.
    /// </remarks>
    public interface IKeyEquality<T> : IEquatable<T>
        where T: IKey
    { }

    /// <summary>
    /// Interface common to all Keys with Comparable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Use this in the implementation of the Key.
    /// </remarks>
    public interface IKeyComparable<T> : IKeyEquality<T>, IComparable<T>, IComparable
        where T : IKey
    { }
        
    /// <summary>
    /// Extensions for IKey.
    /// </summary>
    public static class KeyExtension
    {
        /// <summary>
        /// String Compare option. Used in Key Equality.
        /// </summary>
        public static StringComparison CompareString { get; } = StringComparison.CurrentCultureIgnoreCase;
    }
}
