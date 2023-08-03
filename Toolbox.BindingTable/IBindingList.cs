using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public interface IBindingList<TRow>: IBindingList, ICollection<TRow>, ICancelAddNew, IRaiseItemChangedEvents
        where TRow : class, INotifyPropertyChanged
    { }
}
