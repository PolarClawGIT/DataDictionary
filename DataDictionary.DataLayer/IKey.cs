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
    /// Use this for the interface of the Key, not the implementation.
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
}
