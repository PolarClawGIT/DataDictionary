using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.BindingTable
{
    public static class BindingExtension
    {
        public static void AddRange<T>(this BindingList<T> target, IEnumerable<T> source)
        {
            foreach (T item in source.ToList())
            { target.Add(item); }
        }

        /// <summary>
        /// Clones the data into a Data Table using CreateReader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IBindingTable source)
        {
            using (DataTable data = new DataTable())
            {
                data.TableName = source.BindingTableName;
                data.Load(source.CreateDataReader());
                return data;
            }
        }
    }
}
