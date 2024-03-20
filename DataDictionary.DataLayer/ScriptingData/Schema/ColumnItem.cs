using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Scripting Schema Column data.
    /// </summary>
    public interface IColumnItem : IColumnKey, IDataItem
    {
        /// <inheritdoc cref="DataColumn.AllowDBNull"/>
        Boolean AllowDBNull { get; }

        /// <inheritdoc cref="DataColumn.DataType"/>
        Type DataType { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema Column data.
    /// </summary>
    /// <remarks>The items are expected to be static.</remarks>
    public class ColumnItem : IColumnItem
    {
        /// <inheritdoc/>
        public String ScopeName { get; init; } = ScopeType.Null.ToScopeName();

        /// <inheritdoc/>
        public String ColumnName { get; init; } = String.Empty;

        /// <inheritdoc/>
        public Boolean AllowDBNull { get; init; } = false;

        /// <inheritdoc/>
        public Type DataType { get; init; } = typeof(object);

        /// <summary>
        /// Constructor for Schema Scripting Schema Column Items
        /// </summary>
        public ColumnItem() : base()
        { }

        /// <summary>
        /// Constructor for Schema Scripting Schema Column Items
        /// </summary>
        public ColumnItem(ScopeType scope, DataColumn source) : this()
        {
            ScopeName = scope.ToScopeName();
            ColumnName = source.ColumnName;
            AllowDBNull = source.AllowDBNull;
            DataType = source.DataType;
        }

        #region IBindingRowState
        /// <summary>
        /// Occurs when and event that can change the RowState occurs.
        /// </summary>
        public event EventHandler? RowStateChanged;
        private DataRowState lastRowState = DataRowState.Detached;

        /// <inheritdoc/>
        protected void OnRowStateChanged()
        {
            if (RowStateChanged is EventHandler hander)
            {
                DataRowState currentState = this.RowState();
                if (currentState != lastRowState)
                {
                    hander(this, EventArgs.Empty);
                    lastRowState = currentState;
                }
            }
        }

        /// <inheritdoc/>
        public DataRowState RowState()
        { return DataRowState.Unchanged; }
        #endregion

        #region INotifyPropertyChanged
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return String.Format("{0} {1}", ScopeName, ColumnName); }


    }
}
