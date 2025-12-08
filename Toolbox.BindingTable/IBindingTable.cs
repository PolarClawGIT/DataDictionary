using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    [Obsolete("Try to get rid of this", true)]
    public interface IBindingName
    {
        /// <summary>
        /// Name given to the Binding Table.
        /// </summary>
        //[Obsolete("Try to get rid of this", true)]
        //String BindingName { get; }
    }

    public interface IBindingDataReader
    {
        /// <inheritdoc cref="DataTable.CreateDataReader"/>
        IDataReader CreateDataReader();
    }

    public interface IBindingTable : IBindingDataReader, IBindingList, IDisposable, ICloneable
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
        /// <param name="source">Datset to load the data from</param>
        /// <param name="tableName">Tablename to Look for. If null/empty the object name is used.</param>
        /// <param name="isSkipable">If true, do not throw and exception when the table is not found.</param>
        void Load(DataSet source, String tableName, Boolean isSkipable);

        /// <inheritdoc cref="BindingList.RaiseListChangedEvents"/>
        Boolean RaiseListChangedEvents { get; set; }
    }

    public interface IBindingTable<T> : IBindingTable, IBindingList<T>
        where T : class, IBindingTableRow
    { }
}
