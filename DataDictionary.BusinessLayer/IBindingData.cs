using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{

    /// <summary>
    /// Additional interface that BindingList implements but is not in IBindingList.
    /// </summary>
    public interface IBindData
    {
        /// <inheritdoc cref="IBindingList{T}.RaiseListChangedEvents"/>
        Boolean RaiseListChangedEvents { get; set; }

        /// <inheritdoc cref="IBindingList{T}.ResetBindings"/>
        void ResetBindings();
    }

    /// <summary>
    /// Interface for Data BindingList objects
    /// </summary>
    public interface IBindingData: IBindingList, IBindingName, IBindingDataReader, IBindData
    { }

    /// <summary>
    /// Interface for Data BindingList objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBindingData<T> : IBindingData, IBindingList<T>
        where T : IBindingRowState, IBindingPropertyChanged
    { }
}
