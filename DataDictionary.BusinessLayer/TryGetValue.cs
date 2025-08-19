using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{

    /// <summary>
    /// Delegate representing a TryGetValue function.
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate Boolean TryGetValue<TIndex, TValue>(TIndex index, [NotNullWhen(true)] out TValue? value);

    /// <summary>
    /// Interface representing the TryGetValue delegate.
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ITryGetValue<TIndex, TValue>
    {
        /// <summary>
        /// Implmentation of the TryGetValue delegate.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Boolean TryGetValue(TIndex index, [NotNullWhen(true)] out TValue? value);
    }
}
