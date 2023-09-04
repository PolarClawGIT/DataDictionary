﻿using System;
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

        /// <summary>
        /// Returns the DataBoundItem, if it exists.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <remarks>
        /// DataGridViewRow may be valid (has a row index and so on) but the DataBoundItem does not exist. 
        /// This can cause a IndexOutOfRange error (Microsoft Code?).
        /// Instead, this returns a null.
        /// </remarks>
        public static Object? GetData(this DataGridViewRow source)
        { // For some reason the DataGridViewRow may be valid but the DataBoundItem does not have a value.
            try
            { return source.DataBoundItem; }
            catch (Exception)
            { return null; }
        }
    }
}
