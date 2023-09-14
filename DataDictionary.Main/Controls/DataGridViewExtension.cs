using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Controls
{
    static class DataGridViewExtension
    {
        /// <summary>
        /// Performs FirstOrDefault on the Rows and returns both the row and the Data of the specific type.
        /// </summary>
        /// <typeparam name="T">The expected Data Type</typeparam>
        /// <param name="control">The DataGridView</param>
        /// <param name="predicate">Where Condition</param>
        /// <returns>Null or the row that matches the condition. If the T also matches, that is returned as part of the tuple.</returns>
        public static (DataGridViewRow Row, T? Data)? FirstOrDefault<T>(this DataGridView control, Func<T, Boolean> predicate)
            where T : class
        {
            if (control.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.DataBoundItem is T item && predicate(item)) is DataGridViewRow row)
            { return (row, row.GetDataBoundItem() as T); }
            else { return null; }
        }

        /// <summary>
        /// Returns the DataBoundItem, if it exists.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        /// <remarks>
        /// DataGridViewRow may be valid (has a row index and so on) but the DataBoundItem does not exist. 
        /// This can cause a IndexOutOfRange error (Microsoft Code?).
        /// Instead, this returns a null.
        /// </remarks>
        public static Object? GetDataBoundItem(this DataGridViewRow control)
        { // For some reason the DataGridViewRow may be valid but the DataBoundItem does not have a value.
            try
            { return control.DataBoundItem; }
            catch (Exception)
            { return null; }
        }

        /// <summary>
        /// This is used to set the Enabled/Disabled colors on a DataGridView. Set the Enabled flag first.
        /// </summary>
        /// <param name="control"></param>
        /// <remarks>This does not work! Bad info from StackTrace.</remarks>
        public static void SetEnabledColors(this DataGridView control)
        {
            foreach (DataGridViewRow row in control.Rows)
            {
                if (control.Enabled)
                { row.HeaderCell.Style = new DataGridViewCellStyle(control.RowHeadersDefaultCellStyle); }
                else
                { row.HeaderCell.Style = new DataGridViewCellStyle(control.RowHeadersDefaultCellStyle) { BackColor = Color.BlueViolet }; }

                foreach (DataGridViewCell item in row.Cells)
                {
                    if (control.Enabled)
                    { item.Style = new DataGridViewCellStyle(control.DefaultCellStyle); }
                    else
                    {
                        //item.DefaultCellStyle = new DataGridViewCellStyle(choiceData.DefaultCellStyle) { BackColor = SystemColors.ControlDark, ForeColor = SystemColors.InactiveCaptionText };
                        item.Style = new DataGridViewCellStyle(control.DefaultCellStyle) { BackColor = Color.DarkRed, ForeColor = SystemColors.ControlLight };
                    }
                }
            }

            foreach (DataGridViewColumn item in control.Columns)
            {
                if (control.Enabled)
                { item.HeaderCell.Style = new DataGridViewCellStyle(control.ColumnHeadersDefaultCellStyle); }
                else
                { item.HeaderCell.Style = new DataGridViewCellStyle(control.ColumnHeadersDefaultCellStyle) { BackColor = Color.Green }; }
            }

            control.Refresh();
        }

        /// <summary>
        /// Validates all the Rows against the DataSource that implements Validation
        /// </summary>
        /// <param name="control"></param>
        public static void ValidateRows<TRow>(this DataGridView control)
            where TRow : class, IBindingTableRow, IValidateItem<TRow>
        {
            IValidateList<TRow>? data = control.DataSource as IValidateList<TRow>;

            if (data is null && control.DataSource is BindingSource binding
                && binding.DataSource is IValidateList<TRow> values)
            { data = values; }

            if (data is IValidateList<TRow>)
            {
                IReadOnlyList<TRow> errors = data.Validate();

                foreach (DataGridViewRow item in control.Rows)
                {
                    if (item.DataBoundItem is TRow row)
                    {
                        if (errors.FirstOrDefault(w => ReferenceEquals(row,w)) is TRow error ) 
                        { item.ErrorText = error.GetRowError(); }
                        else { item.ErrorText = String.Empty; }
                    }
                    else { item.ErrorText = String.Empty; }
                }

            }
        }
    }
}
