using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public interface IBindingTable<T> : IBindingList, ICollection<T>, IDisposable, ICloneable
        where T : class, INotifyPropertyChanged
    {
        void Load(IDataReader reader);
        void Load(IDataReader reader, LoadOption loadOption);
        void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler? errorHandler);
    }
}
