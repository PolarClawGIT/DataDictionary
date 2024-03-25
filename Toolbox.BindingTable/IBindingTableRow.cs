using System.Data;

namespace Toolbox.BindingTable
{
    public class RowStateEventArgs : EventArgs
    {
        public DataRowState RowState { get; } = DataRowState.Detached;

        public RowStateEventArgs(DataRowState rowState)
        { RowState = rowState; }
    }

    /// <summary>
    /// BindingRow RowState definition
    /// </summary>
    public interface IBindingRowState
    {
        DataRowState RowState();
        event EventHandler<RowStateEventArgs>? RowStateChanged;

        /// <summary>
        /// Invoke the RowStateChanged event handler
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="priorState"></param>
        /// <example>
        /// IBindingRowState.OnRowStateChanged(this, RowStateChanged, ref lastRowState);
        /// </example>
        static void OnRowStateChanged(IBindingRowState sender, EventHandler<RowStateEventArgs>? eventHandler, ref DataRowState priorState)
        {
            if (eventHandler is EventHandler<RowStateEventArgs> handler)
            {
                DataRowState currentState = sender.RowState();
                if (currentState != priorState)
                {
                    handler(sender, new RowStateEventArgs(currentState));
                    priorState = currentState;
                }
            }
        }
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
    public interface IBindingTableRow : IBindingRowState, IBindingRowColumns, IBindingPropertyChanged
    {
        String GetRowError();
        Boolean HasRowErrors();
        Boolean HasRowVersion(DataRowVersion version);
        void ClearRowErrors();
        void SetRowError(string value);
        void SetColumnError(String columnName, String? error);
        void AcceptChanges();
        void RejectChanges();
        String? GetColumnError(String columnName);
        String[] GetColumnsInError();
    }


}
