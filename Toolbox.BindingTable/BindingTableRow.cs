using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Toolbox.BindingTable
{
    public interface IBindingTableRow : INotifyPropertyChanged
    {
        String GetRowError();
        Boolean HasRowErrors();
        Boolean HasRowVersion(DataRowVersion version);
        DataRowState RowState();
        void ClearRowErrors();
        void OnPropertyChanged(string propertyName);
        void SetRowError(string value);
        void SetColumnError(String columnName, String? error);
        void AcceptChanges();
        void RejectChanges();
        String? GetColumnError(String columnName);
        String[] GetColumnsInError();
        IReadOnlyList<DataColumn> ColumnDefinitions();
    }

    /// <summary>
    /// The BindingTableRow is the wrappers around the DataRow used by the BindingTable class.
    /// This wrappers job is to expose the DataRow as a POCO like class while keeping the
    /// DataRow internal to the system.
    /// </summary>
    [Serializable]
    public abstract class BindingTableRow : IBindingTableRow, ISerializable
    {
        private IBindingTable? bindingTable;

        /// <summary>
        /// Reference to the Binding Table that owns this row.
        /// </summary>
        protected internal IBindingTable? GetBindingTable()
        { return bindingTable; }

        /// <summary>
        /// Reference to the Binding Table that owns this row.
        /// </summary>
        protected internal void SetBindingTable(IBindingTable? value)
        { bindingTable = value; }

        /// <summary>
        /// A Column Definition of the underlining table.
        /// </summary>
        /// <remarks>
        /// Implement by creating a Static Readonly list of DataColumns.
        /// Assign that property to this property. 
        /// This is used by BindingTable to construct the table definition but the columns are not bound to the table.
        /// Not all attributes of DataColumn are copied to the table definition.
        /// </remarks>
        public abstract IReadOnlyList<DataColumn> ColumnDefinitions();

        /// <summary>
        /// Internal DataRow being wrapper-ed.
        /// </summary>
        private DataRow data;

        /// <summary>
        /// Base Constructor for the BindingTableRow.
        /// </summary>
        /// <remarks>
        /// The class as part of the constructor creates a temporary DataTable and
        /// the row that goes with it. The Row matches the ColumnDefinitions, but
        /// allows Null for all values.
        /// </remarks>
        protected internal BindingTableRow() : base()
        {
            using (DataTable temp = new DataTable("Init_BindingTableRow"))
            {
                temp.AddColumns(this.ColumnDefinitions(), true);

                data = temp.NewRow();
                temp.Rows.Add(data);

                data.Table.RowChanging += Table_RowChanging;
                data.Table.RowChanged += Table_RowChanged;
                data.Table.RowDeleting += Table_RowDeleting;
                data.Table.RowDeleted += Table_RowDeleted;
                data.Table.Disposed += Table_Disposed;
                data.Table.ColumnChanged += Table_ColumnChanged;
            }
        }



        /// <summary>
        /// Constructor that loaded the DataRow.
        /// </summary>
        /// <param name="row"></param>
        //protected BindingTableRow(DataRow row) : this() {ImportRow(row); }

        /// <summary>
        /// Used to assign a data row to the class.
        /// </summary>
        /// <param name="row"></param>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual internal void SetRow(DataRow row)
        {
            data.Table.RowChanging -= Table_RowChanging;
            data.Table.RowChanged -= Table_RowChanged;
            data.Table.RowDeleting -= Table_RowDeleting;
            data.Table.RowDeleted -= Table_RowDeleted;
            data.Table.Disposed -= Table_Disposed;
            data.Table.ColumnChanged -= Table_ColumnChanged;

            row.Table.RowChanging += Table_RowChanging;
            row.Table.RowChanged += Table_RowChanged;
            row.Table.RowDeleting += Table_RowDeleting;
            row.Table.RowDeleted += Table_RowDeleted;
            row.Table.Disposed += Table_Disposed;
            row.Table.ColumnChanged += Table_ColumnChanged;
            data = row;
        }

        /// <summary>
        /// Gives access to the Internal DataRow.
        /// </summary>
        /// <returns></returns>
        protected internal DataRow GetRow()
        { return data; }

        /// <summary>
        /// Used to Get a Value from the DataRow and return it as a specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <remarks>Many data types have there own TryParse functions. These can be handled generically.</remarks>
        protected virtual Nullable<T> GetValue<T>(String columnName)
            where T : struct, IParsable<T>
        {
            if (data is not DataRow row || row.RowState == DataRowState.Detached)
            {
                Exception error = new InvalidOperationException("Internal DataRow is not defined");
                if (GetBindingTable() is not null) { error.Data.Add(nameof(GetBindingTable), GetBindingTable()); }
                error.Data.Add(nameof(columnName), columnName);
                throw error;
            }

            if (!row.Table.Columns.Contains(columnName))
            { throw new ArgumentOutOfRangeException(String.Format("{0} not in list of Columns", columnName)); }

            Object? baseValue = null;
            if (data.RowState == DataRowState.Deleted)
            { baseValue = row[columnName, DataRowVersion.Original]; }
            else { baseValue = row[columnName]; }

            if (baseValue == DBNull.Value) { return new Nullable<T>(); }

            if (String.IsNullOrWhiteSpace(baseValue.ToString()))
            { return new Nullable<T>(); }

            // Generic handling
            if (T.TryParse(row[columnName].ToString(), null, out T value))
            { return new Nullable<T>(value); }

            // Parsing failed
            throw new InvalidCastException(String.Format("{0} is not a {1}, actual type {2}", columnName, typeof(T).Name, row[columnName].GetType().Name));
        }

        /// <summary>
        /// Used to Get a Value from the DataRow and return it as a String type.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <remarks>String are a Microsoft special class and needs different handling</remarks>
        protected virtual String? GetValue(String columnName)
        {
            if (data is not DataRow row || row.RowState == DataRowState.Detached)
            {
                Exception error = new InvalidOperationException("Internal DataRow is not defined");
                if (GetBindingTable() is not null) { error.Data.Add(nameof(GetBindingTable), GetBindingTable()); }
                error.Data.Add(nameof(columnName), columnName);
                throw error;
            }

            if (!row.Table.Columns.Contains(columnName))
            { throw new ArgumentOutOfRangeException(String.Format("{0} not in list of Columns", columnName)); }

            Object? baseValue = null;
            if (data.RowState == DataRowState.Deleted)
            { baseValue = row[columnName, DataRowVersion.Original]; }
            else { baseValue = row[columnName]; }

            if (baseValue == DBNull.Value) { return null; }

            if (baseValue is String stringValue)
            {
                if (string.IsNullOrEmpty(stringValue)) { return null; }
                else { return stringValue; }
            }

            // Parsing failed
            throw new InvalidCastException(String.Format("{0} is not a {1}, actual type {2}", columnName, typeof(String).Name, row[columnName].GetType().Name));
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
        /// <param name="parseFunction"></param>
        /// <returns></returns>
        /// <remarks>
        /// Some data types do not implement IParsable (like Boolean) or a specialized version of TryParse is needed.
        /// This allows a TryParse function to be passed in.
        /// </remarks>
        protected virtual Nullable<T> GetValue<T>(String columnName, tryParseDelegate<T> parseFunction)
            where T : struct
        {
            if (String.IsNullOrWhiteSpace(columnName)) { throw new ArgumentNullException(nameof(columnName)); }
            if (parseFunction is null) { throw new ArgumentNullException(nameof(parseFunction)); }
            if (data is not DataRow row) { throw new InvalidOperationException("Internal DataRow is not defined"); }
            if (!row.Table.Columns.Contains(columnName)) { throw new ArgumentOutOfRangeException(String.Format("{0} not in list of Columns", columnName)); }

            Object? baseValue = null;
            if (data.RowState == DataRowState.Deleted)
            { baseValue = row[columnName, DataRowVersion.Original]; }
            else { baseValue = row[columnName]; }

            if (baseValue == DBNull.Value) { return new Nullable<T>(); }

            if (baseValue.ToString() is String stringValue &&
                parseFunction(stringValue, out T result))
            { return new Nullable<T>(result); }

            // Parsing failed
            throw new InvalidCastException(String.Format("{0} is not a {1}, actual type {2}", columnName, typeof(String).Name, row[columnName].GetType().Name));
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
            if (data is DataRow row
                && row.Table.Columns.Contains(columnName)
                && data.RowState is DataRowState.Added or DataRowState.Unchanged or DataRowState.Modified)
            {
                if (value is null) { row[columnName] = DBNull.Value; }
                else { row[columnName] = value.Value; }
            }
        }

        /// <summary>
        /// Used to Set a value in the DataRow with the String type.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        protected virtual void SetValue(String columnName, String? value)
        {
            if (data is DataRow row
                && row.Table.Columns.Contains(columnName)
                && data.RowState is DataRowState.Added or DataRowState.Unchanged or DataRowState.Modified)
            {
                if (String.IsNullOrWhiteSpace(value)) { row[columnName] = DBNull.Value; }
                else { row[columnName] = value; }
            }
        }

        #region DataRow
        protected virtual void Table_RowChanging(object sender, DataRowChangeEventArgs e)
        { }

        protected virtual void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        { }

        protected virtual void Table_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            if (data is DataRow row)
            {
                data.Table.RowChanging -= Table_RowChanging;
                data.Table.RowChanged -= Table_RowChanged;
                data.Table.RowDeleting -= Table_RowDeleting;
                data.Table.RowDeleted -= Table_RowDeleted;
                data.Table.Disposed -= Table_Disposed;
                data.Table.ColumnChanged -= Table_ColumnChanged;
            }
        }

        protected virtual void Table_RowDeleting(object sender, DataRowChangeEventArgs e)
        { /* throw new NotImplementedException(); */ }

        protected virtual void Table_Disposed(object? sender, EventArgs e)
        {
            data.Table.RowChanging -= Table_RowChanging;
            data.Table.RowChanged -= Table_RowChanged;
            data.Table.RowDeleting -= Table_RowDeleting;
            data.Table.RowDeleted -= Table_RowDeleted;
            data.Table.Disposed -= Table_Disposed;
            data.Table.ColumnChanged -= Table_ColumnChanged;
        }

        protected virtual void Table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        { // Assumes that Attributes and the Columns have identical names. No way found to insure this.
            if (ReferenceEquals(data, e.Row)
                && e.Row.RowState != DataRowState.Detached
                && e.Column is not null)
            { OnPropertyChanged(e.Column.ColumnName); }
        }

        /// <inheritdoc cref="DataRow.RowState"/>
        public virtual DataRowState RowState()
        {
            if (data is DataRow row)
            {
                // For whatever reason, the row state can be unchanged but their is a Proposed row.
                if (data.HasVersion(DataRowVersion.Proposed) && row.RowState == DataRowState.Unchanged)
                { return DataRowState.Modified; }

                return row.RowState;

            }
            else { return DataRowState.Detached; }
        }

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

        /// <inheritdoc cref="DataRow.GetColumnError"/>
        public virtual String? GetColumnError(String columnName)
        {
            if (data is DataRow row) { return row.GetColumnError(columnName); }
            else { return null; }
        }

        /// <inheritdoc cref="DataRow.SetColumnError"/>
        public virtual void SetColumnError(String columnName, String? error)
        { if (data is DataRow row) { row.SetColumnError(columnName, error); } }

        /// <inheritdoc cref="DataRow.GetColumnsInError"/>
        public virtual String[] GetColumnsInError()
        {
            if (data is DataRow row) { return row.GetColumnsInError().Cast<DataColumn>().Select(s => s.ColumnName).ToArray(); }
            else { return new String[0]; }
        }

        /// <inheritdoc cref="DataRow.HasVersion"/>
        public virtual Boolean HasRowVersion(DataRowVersion version)
        { if (data is DataRow row) { return row.HasVersion(version); } else { return false; } }

        /// <inheritdoc cref="DataRow.AcceptChanges"/>
        public virtual void AcceptChanges()
        {
            if (data is DataRow row)
            { row.AcceptChanges(); }
        }

        /// <inheritdoc cref="DataRow.RejectChanges"/>
        public virtual void RejectChanges()
        {
            if (data is DataRow row)
            { row.RejectChanges(); }
        }
        #endregion

        #region INotifyPropertyChanged
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        /// <remarks>
        /// The method assumes that the Property Name and the Column Name are identical.
        /// The actual property changed event is raised on the Column change event, not the Set method.
        /// This allows changes made directly to the table row to be captured.
        /// </remarks>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #endregion

        #region ISerializable
        /// <inheritdoc/>
        /// <remarks>
        /// The Column values are written to the SerializationInfo.
        /// </remarks>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            foreach (DataColumn item in data.Table.Columns)
            { info.AddValue(item.ColumnName, this.data[item]); }
        }

        /// <summary>
        /// Serialization version of the constructor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <remarks>The Columns of the DataRow are read from the SerializationInfo.</remarks>
        protected BindingTableRow(SerializationInfo info, StreamingContext context) : this()
        {
            foreach (DataColumn item in data.Table.Columns)
            {
                var value = info.GetValue(item.ColumnName, item.DataType);
                if (value is DBNull) { this.data[item] = null; }
                else { this.data[item] = info.GetValue(item.ColumnName, item.DataType); }
            }
        }
        #endregion
    }
}
