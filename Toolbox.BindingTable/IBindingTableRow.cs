using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    /// <summary>
    /// BindingRow RowState definition
    /// </summary>
    public interface IBindingRowState
    {
        DataRowState RowState();
        event EventHandler? RowStateChanged;
    }

    /// <summary>
    /// BindingRow column definitions
    /// </summary>
    public interface IBindingRowColumns
    {
        IReadOnlyList<DataColumn> ColumnDefinitions();
    }

    /// <summary>
    /// Base Binding Row
    /// </summary>
    public interface IBindingTableRow : IBindingRowState, IBindingRowColumns, INotifyPropertyChanged
    {
        String GetRowError();
        Boolean HasRowErrors();
        Boolean HasRowVersion(DataRowVersion version);
        void ClearRowErrors();
        void OnPropertyChanged(string propertyName);
        void SetRowError(string value);
        void SetColumnError(String columnName, String? error);
        void AcceptChanges();
        void RejectChanges();
        String? GetColumnError(String columnName);
        String[] GetColumnsInError();
        
    }


}
