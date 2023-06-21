using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public interface IBindingTable : IBindingList, IDisposable, ICloneable
    {
        void Load(IDataReader reader);
        void Load(IDataReader reader, LoadOption loadOption);
        void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler? errorHandler);
    }

    public interface IBindingTable<T> : IBindingTable, ICollection<T>
        where T : class, INotifyPropertyChanged
    { }
}
