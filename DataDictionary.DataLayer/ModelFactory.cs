using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer
{
    public static class ModelFactory
    {
        internal static StringComparison CompareString { get; } = StringComparison.CurrentCultureIgnoreCase;

        /// <summary>
        /// Helper method to create BindingTables with the default initialization state.
        /// </summary>
        /// <typeparam name="TBinding"></typeparam>
        /// <returns></returns>
        public static BindingTable<TBinding> Create<TBinding>()
            where TBinding : BindingTableRow, INotifyPropertyChanged, IBindingTableRow, new()
        {
            return new BindingTable<TBinding>() { CompareString = ModelFactory.CompareString };
        }
    }
}
