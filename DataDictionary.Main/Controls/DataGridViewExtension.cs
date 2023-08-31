using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Controls
{
    static class DataGridViewExtension
    {
        public static (DataGridViewRow? Row, T? Data) FirstOrDefault<T>(this DataGridView control)
            where T : class
        {
            if (control.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.DataBoundItem is T) is DataGridViewRow row)
            { return (row, row.DataBoundItem as T); }
            else { return (null, null); }
        }

        public static (DataGridViewRow? Row, T? Data) FirstOrDefault<T>(this DataGridView control, Func<T, Boolean> predicate)
            where T : class
        {
            if (control.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.DataBoundItem is T item && predicate(item)) is DataGridViewRow row)
            { return (row, row.DataBoundItem as T); }
            else { return (null, null); }
        }
    }
}
