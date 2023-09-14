using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Interface common to all Keys
    /// </summary>
    public interface IKey { }

    /// <summary>
    /// Interface common to all Keys with Equality.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeyEquality<T> : IKey, IEquatable<T>
        where T : IKeyEquality<T>
    { }

    /// <summary>
    /// Interface common to all Keys with Comparable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeyComparable<T> : IKey, IKeyEquality<T>, IComparable<T>, IComparable
        where T : IKeyEquality<T>
    { }
}
