using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public interface IBindingName
    {
        /// <summary>
        /// Name given to the Binding Table.
        /// </summary>
        String BindingName { get; }
    }

    public interface IBindingDataReader
    {
        /// <inheritdoc cref="DataTable.CreateDataReader"/>
        IDataReader CreateDataReader();
    }

    public interface IBindingTable : IBindingName, IBindingDataReader, IBindingList, IDisposable, ICloneable
    {
        /// <inheritdoc cref="DataTable.Load(IDataReader)"/>
        void Load(IDataReader reader);

        /// <inheritdoc cref="DataTable.Load(IDataReader, LoadOption)"/>
        void Load(IDataReader reader, LoadOption loadOption);

        /// <inheritdoc cref="DataTable.Load(IDataReader, LoadOption, FillErrorEventHandler?)"/>
        void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler? errorHandler);

        /// <summary>
        /// Loads from a DataSet.
        /// </summary>
        /// <param name="source"></param>
        void Load(DataSet source);
    }

    public interface IBindingTable<T> : IBindingTable, IBindingList<T>
        where T : class, IBindingTableRow
    { }
}
