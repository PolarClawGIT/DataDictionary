using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public interface IBindingList<TRow> : IBindingList, ICollection<TRow>, IList<TRow>, ICancelAddNew, IRaiseItemChangedEvents
        where TRow : class, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remakes>Resolves ambiguity between IList, ICollection and IBindingList</remakes>
        new TRow this[int index]
        {
            get { return ((IList<TRow>)this)[index]; }
            set { ((IList<TRow>)this)[index] = value; }
        }

        /// <inheritdoc cref="ICollection{T}.Count"/>
        /// <remakes>Resolves ambiguity between IList, ICollection and IBindingList</remakes>
        new Int32 Count { get { return ((ICollection<TRow>)this).Count; } }

        /// <inheritdoc cref="IList{T}.RemoveAt"/>
        /// <remarks>Resolves ambiguity between IList and generic list</remarks>
        new void RemoveAt(int index) { ((IList<TRow>)this).RemoveAt(index); }
    }
}
