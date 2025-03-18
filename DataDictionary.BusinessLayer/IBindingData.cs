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
    /// Interface for Data BindingList objects
    /// </summary>
    public interface IBindingData: IBindingList, IBindingName, IBindingDataReader
    {
        /// <inheritdoc cref="BindingList{T}.ResetBindings"/>
        void ResetBindings();
    }

    /// <summary>
    /// Interface for Data BindingList objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBindingData<T> : IBindingData, IBindingList<T>
        where T : IBindingRowState, IBindingPropertyChanged
    { }
}
