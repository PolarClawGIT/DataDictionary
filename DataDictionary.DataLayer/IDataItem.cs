using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Base interface used by all Data Item classes.
    /// </summary>
    public interface IDataItem: INotifyPropertyChanged, IBindingRowState
    { // This is really a way of getting generics to work with any item that has a similar function.
    }
}
