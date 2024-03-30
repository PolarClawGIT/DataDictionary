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
        /// Copies BindingTableRows to a new Table using CopyToDataTable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <see cref="DataTableExtensions.CopyToDataTable"/>
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
            where T : BindingTableRow, new()
        {
            DataTable result;
            DataColumn[] dataColumns = new T().ColumnDefinitions().ToArray();

            IList<DataRow> values = data.Select(s => s.GetRow()).ToList();

            if (values.Count == 0)
            {
                // CopyToDataTable returns a Invalid Operation Exception if there are no rows in the list.
                // Return an empty table in that scenario.
                using (result = new DataTable())
                {
                    if (data is IBindingTable source)
                    { result.TableName = source.BindingName; }

                    result.AddColumns(dataColumns.ToArray()); 
                }
            }
            else
            {
                result = values.CopyToDataTable();

                if (data is IBindingTable source)
                { result.TableName = source.BindingName; }
            }


            return result;
        }
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
