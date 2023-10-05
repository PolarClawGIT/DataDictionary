using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms
{
    interface IApplicationDataForm
    {
        Boolean IsOpenItem(Object? Item);
    }

    /// <summary>
    /// Application Base Class for Forms that have a DataKey.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// This is still a prototype currently implemented using IApplicationDataForm.
    /// </remarks>
    class ApplicationBase<TKey>: ApplicationBase
        where TKey: class, IKey
    {
        public virtual required TKey DataKey { get; init; }

        public virtual Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }
    }

}
