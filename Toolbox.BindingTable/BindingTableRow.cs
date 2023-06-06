using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{

    /// <summary>
    /// The BindingTableRow is the wrappers around the DataRow used by the BindingTable class.
    /// This wrappers job is t expose the DataRow as a POCO like class while keeping the
    /// DataRow internal to the system.
    /// </summary>
    public abstract class BindingTableRow : INotifyPropertyChanged
    {
        /// <summary>
        /// Internal DataRow being wrappered.
        /// </summary>
        private DataRow? data;

        /// <summary>
        /// Base Constructor for the BindingTableRow.
        /// </summary>
        protected BindingTableRow() : base() { }

        /// <summary>
        /// Constructor that loadeds the DataRow.
        /// </summary>
        /// <param name="row"></param>
        protected BindingTableRow(DataRow row) : this()
        { ImportRow(row); }

        /// <summary>
        /// Used to assign a data row to the class.
        /// </summary>
        /// <param name="row"></param>
        /// <exception cref="InvalidOperationException"></exception>
        protected internal void ImportRow(DataRow row)
        {
            if (data is DataRow) { throw new InvalidOperationException("DataRow is already assigned."); }
            row.Table.RowChanging += Table_RowChanging;
            row.Table.RowChanged += Table_RowChanged;
            row.Table.RowDeleting += Table_RowDeleting;
            row.Table.RowDeleted += Table_RowDeleted;
            row.Table.Disposed += Table_Disposed;
            data = row;
        }

        /// <summary>
        /// Gives access to the Internal DataRow.
        /// </summary>
        /// <returns></returns>
        protected internal DataRow? GetRow()
        { return data; }

        /// <summary>
        /// Used to Get a Value from the DataRow and return it as a specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <remarks>Many data types have there onw TryParse functions. These can be handled genericly.</remarks>
        protected virtual Nullable<T> GetValue<T>(String columnName)
            where T : struct, IParsable<T>
        {
            if (data is not DataRow row) { throw new InvalidOperationException("Internal DataRow is not defined"); }
            if (!row.Table.Columns.Contains(columnName)) { throw new IndexOutOfRangeException(String.Format("{0} not in list of Columns", columnName)); }

            // Generic handling
            if (T.TryParse(row[columnName].ToString(), null, out T value))
            { return new Nullable<T>(value); }

            // Parsing failed, return null
            return new Nullable<T>();
        }

        /// <summary>
        /// Used to Get a Value from the DataRow and return it as a String type.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <remarks>Sting are a Microsoft special class and needs diffrent handling</remarks>
        protected virtual String? GetValue(String columnName)
        {
            if (data is not DataRow row) { throw new InvalidOperationException("Internal DataRow is not defined"); }
            if (!row.Table.Columns.Contains(columnName)) { throw new ArgumentOutOfRangeException(String.Format("{0} not in list of Columns", columnName)); }

            if (row[columnName].ToString() is String value)
            { return value; }
            else { return null; }
        }

        /// <summary>
        /// Delegate that matches a TryParse function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns>Used with GetValue function where the TryParse function is passed</returns>
        protected delegate Boolean tryParseDelegate<T>(String value, out T result) where T : struct;

        /// <summary>
        /// Used to Get a Value from the DataRow and return it as a specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="praseFunction"></param>
        /// <returns></returns>
        /// <remarks>
        /// Some data types do not implment IParsable (like Boolean) or a specialzied version of TryParse is needed.
        /// This allows a TryParse function to be passed in.
        /// </remarks>
        protected virtual Nullable<T> GetValue<T>(String columnName, tryParseDelegate<T> praseFunction)
            where T : struct
        {
            if (String.IsNullOrWhiteSpace(columnName)) { throw new ArgumentNullException(nameof(columnName)); }
            if (praseFunction is null) { throw new ArgumentNullException(nameof(praseFunction)); }
            if (data is not DataRow row) { throw new InvalidOperationException("Internal DataRow is not defined"); }
            if (!row.Table.Columns.Contains(columnName)) { throw new ArgumentOutOfRangeException(String.Format("{0} not in list of Columns", columnName)); }

            if (row[columnName].ToString() is String stringValue &&
                praseFunction(stringValue, out T result))
            { return new Nullable<T>(result); }
            else { return new Nullable<T>(); }
        }

        /// <summary>
        /// Used to Set a value in the DataRow with the specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        protected virtual void SetValue<T>(String columnName, Nullable<T> value)
            where T : struct
        {
            if (data is DataRow row && row.Table.Columns.Contains(columnName))
            { row[columnName] = value; }
        }

        /// <summary>
        /// Used to Set a value in the DataRow with the String type.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        protected virtual void SetValue(String columnName, String? value)
        {
            if (data is DataRow row && row.Table.Columns.Contains(columnName))
            { row[columnName] = value; }
        }

        #region DataRow
        /// <summary>
        /// Used store the list of columns that have pending changes. The "Changing" event.
        /// The On Property Change is called after the changes are applied. The "Changed" event.
        /// </summary>
        List<DataColumn> pendingChanges = new List<DataColumn>();

        protected virtual void Table_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            if (Object.ReferenceEquals(data, e.Row) && e.Row.HasVersion(DataRowVersion.Proposed))
            {
                foreach (DataColumn item in e.Row.Table.Columns)
                {
                    if (String.Equals(e.Row[item, DataRowVersion.Proposed].ToString(), e.Row[item].ToString(), StringComparison.Ordinal))
                    {
                        if (pendingChanges.Contains(item)) { pendingChanges.Remove(item); }
                        else { pendingChanges.Add(item); }
                    }
                }
            }
        }

        protected virtual void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (Object.ReferenceEquals(data, e.Row))
            {
                foreach (DataColumn item in pendingChanges)
                { OnPropertyChanged(item.ColumnName); }
                pendingChanges.Clear();
            }
        }

        protected virtual void Table_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            if (data is DataRow row)
            {
                row.Table.RowChanging -= Table_RowChanging;
                row.Table.RowChanged -= Table_RowChanged;
                row.Table.Disposed -= Table_Disposed;
            }
        }

        protected virtual void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
        { /* throw new NotImplementedException(); */ }

        protected virtual void Table_Disposed(object? sender, EventArgs e)
        {
            if (data is DataRow row)
            {
                row.Table.RowChanging -= Table_RowChanging;
                row.Table.RowChanged -= Table_RowChanged;
                row.Table.Disposed -= Table_Disposed;
            }
        }

        /// <inheritdoc cref="DataRow.RowState"/>
        public virtual DataRowState RowState()
        { if (data is DataRow row) { return row.RowState; } else { return DataRowState.Detached; } }

        /// <inheritdoc cref="DataRow.RowError"/>
        public virtual String GetRowError()
        { if (data is DataRow row) { return row.RowError; } else { return String.Empty; } }

        /// <inheritdoc cref="DataRow.RowError"/>
        public virtual void SetRowError(String value)
        { if (data is DataRow row) { row.RowError = value; } }

        /// <inheritdoc cref="DataRow.ClearErrors"/>
        public virtual void ClearRowErrors()
        { if (data is DataRow row) { row.ClearErrors(); } }

        /// <inheritdoc cref="DataRow.HasErrors"/>
        public virtual Boolean HasRowErrors()
        { if (data is DataRow row) { return row.HasErrors; } else { return false; } }

        /// <inheritdoc cref="DataRow.HasVersion"/>
        public virtual Boolean HasRowVersion(DataRowVersion version)
        { if (data is DataRow row) { return row.HasVersion(version); } else { return false; } }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler is PropertyChangedEventHandler) { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }
        #endregion
    }
}
