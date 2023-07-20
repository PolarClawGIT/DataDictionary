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
        /// <inheritdoc cref="DataTable.Load(IDataReader)"/>
        void Load(IDataReader reader);

        /// <inheritdoc cref="DataTable.Load(IDataReader, LoadOption)"/>
        void Load(IDataReader reader, LoadOption loadOption);

        /// <inheritdoc cref="DataTable.Load(IDataReader, LoadOption, FillErrorEventHandler?)"/>
        void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler? errorHandler);

        /// <inheritdoc cref="DataTable.CreateDataReader"/>
        IDataReader CreateDataReader();
    }

    public interface IBindingTable<T> : IBindingTable, ICollection<T>
        where T : class, INotifyPropertyChanged
    { }
}
