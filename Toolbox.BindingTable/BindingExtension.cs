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
                data.TableName = source.BindingName;
                data.Load(source.CreateDataReader());
                return data;
            }
        }

        /// <inheritdoc cref="DataTableExtensions.CopyToDataTable"/>
        /// <remarks>Handles enumerations of BindingTableRows</remarks>
        public static DataTable ToDataTable(this IEnumerable<BindingTableRow> data)
        { return data.Select(s => s.GetRow()).CopyToDataTable(); }
    }

    static class BindingPrivateExtension
    {
        /// <summary>
        /// Adds columns to the target table, if the column does not already exist.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="columns"></param>
        /// <param name="allowDBNull"></param>
        public static void AddColumns(this DataTable target, IReadOnlyList<DataColumn> columns, Boolean? allowDBNull = null)
        {
            foreach (DataColumn item in columns)
            {
                using (DataColumn column = new DataColumn(item.ColumnName, item.DataType)
                {
                    AllowDBNull = item.AllowDBNull,
                    Caption = item.Caption,
                    DefaultValue = item.DefaultValue,
                })
                {
                    if (allowDBNull is Boolean value) { column.AllowDBNull = value; }

                    if (!target.Columns.Contains(column.ColumnName)) { target.Columns.Add(column); }
                }
            }
        }
    }
}
