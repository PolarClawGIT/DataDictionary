using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    /// <summary>
    /// Missing Changed Event in BindingList{T} but not in IBindingList.
    /// </summary>
    public interface IBindListChanged
    {
        /// <inheritdoc cref="IBindingList.ListChanged"/>
        event ListChangedEventHandler ListChanged;

        //Example:
        //public SomeObject() : base()
        //{
        //    someBindingList.ListChanged += OnListChanged;

        //    void OnListChanged(Object? sender, ListChangedEventArgs e)
        //    {
        //        if (ListChanged is ListChangedEventHandler handler)
        //        { handler(sender, e); }
        //    }
        //}
        //
        //Note: I have had lots of issues with the ListChanged event.
        //It does not always bubble up as expected thru interfaces or inherited classes.
        //Interfaces do not have an instance so I cannot put event related code in one spot.
        //Instead the code has to be copied all over the place and is prone to errors.

        /// <inheritdoc cref="BindingList{T}.RaiseListChangedEvents"/>
        Boolean RaiseListChangedEvents { get; set; }

        /// <inheritdoc cref="BindingList{T}.ResetBindings"/>
        void ResetBindings();
    }

    /// <summary>
    /// Generic version of IBindingList incorporating generic ICollection and IList.
    /// </summary>
    /// <typeparam name="TRow"></typeparam>
    public interface IBindingList<TRow> : IBindingList,
        ICollection<TRow>, IList<TRow>,
        ICancelAddNew, IRaiseItemChangedEvents,
        IBindListChanged
        where TRow : IBindingPropertyChanged
    {
        // This works but attempts to do the same thing with my own interfaces fails. I don't know why.

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

        /// <inheritdoc cref="IBindingList.Count"/>
        /// <remakes>Resolves ambiguity between IList, ICollection and IBindingList</remakes>
        new Int32 Count { get { return ((IBindingList)this).Count; } }

        /// <inheritdoc cref="IBindingList.RemoveAt"/>
        /// <remarks>Resolves ambiguity between IList and generic list</remarks>
        new void RemoveAt(int index) { ((IBindingList)this).RemoveAt(index); }

        /// <inheritdoc cref="IBindingList.Clear"/>
        /// <remakes>Resolves ambiguity between IList, ICollection and IBindingList</remakes>
        new void Clear() { ((IBindingList)this).Clear(); }

        /// <inheritdoc cref="IBindingList.ListChanged"/>
        new event ListChangedEventHandler ListChanged;
    }
}
