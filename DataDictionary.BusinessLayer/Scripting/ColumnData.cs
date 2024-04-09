using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Column
    /// </summary>
    public interface IColumnData : IEnumerable<ColumnItem>
    {
        /// <summary>
        /// Default Load of the class.
        /// </summary>
        void Load();
    }

    /// <summary>
    /// Implementation component for the Scripting Engine Column
    /// </summary>
    public class ColumnData : Collection<ColumnItem>, IColumnData
    {
        /// <summary>
        /// Returns a list of Scopes that have column lists.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ScopeType> GetScopes()
        { return this.GroupBy(g => new ScopeKey(g).Scope).Select(s => s.Key); }

        /// <summary>
        /// Returns a list of Columns for a given scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IEnumerable<IColumnItem> GetColumns(ScopeType scope)
        { return this.Where(w => scope.Equals(w) && w is IColumnItem).Select(s => s as IColumnItem); }

        /// <summary>
        /// Default Load of the class.
        /// </summary>
        public void Load()
        {
            Load(new DataLayer.DomainData.Attribute.DomainAttributeItem());
            Load(new DataLayer.DomainData.Attribute.DomainAttributePropertyItem());
        }

        /// <summary>
        /// Loads the collection using the Column Definitions of a BindingTableRow.
        /// </summary>
        /// <typeparam name="TRow"></typeparam>
        /// <param name="source"></param>
        public void Load<TRow>(TRow source)
            where TRow : IBindingTableRow, IScopeKey
        {
            foreach (DataColumn item in source.ColumnDefinitions())
            {
                ColumnItem newItem = new()
                {
                    Scope = source.Scope,
                    ColumnName = item.ColumnName,
                    AllowDBNull = item.AllowDBNull,
                    DataType = item.DataType,
                };

                this.Add(newItem);
            }
        }



    }
}
