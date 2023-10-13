using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// A data object capable of Removing Data by Key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRemoveData<TKey>
        where TKey : IKey
    {
        /// <summary>
        /// A data object capable of Removing Data by Key
        /// </summary>
        /// <param name="key"></param>
        void Remove(TKey key);
    }
}
