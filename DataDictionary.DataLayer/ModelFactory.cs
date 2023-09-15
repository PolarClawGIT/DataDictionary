using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Helper Class for building BindingTableRow.
    /// </summary>
    [Obsolete("Being replaced with strongly type constructors")]
    public static class ModelFactory
    {
        /// <summary>
        /// Helper method to create BindingTables with the default initialization state.
        /// </summary>
        /// <typeparam name="TBinding"></typeparam>
        /// <returns></returns>
        public static BindingTable<TBinding> Create<TBinding>()
            where TBinding : BindingTableRow, INotifyPropertyChanged, IBindingTableRow, new()
        {
            return new BindingTable<TBinding>() ;
        }
    }
}
