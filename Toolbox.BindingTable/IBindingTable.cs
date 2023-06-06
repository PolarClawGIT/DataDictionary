using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public interface IBindingTable<T> : IBindingList, ICollection<T>, IDisposable, ICloneable
        where T : class, INotifyPropertyChanged
    { 
    }
}
