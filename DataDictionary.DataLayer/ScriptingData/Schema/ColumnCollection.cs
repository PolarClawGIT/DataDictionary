using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Generic Base class for Scripting Schema Column Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>This list is expected to be static.</remarks>
    public abstract class ColumnCollection<TItem> : BindingList<TItem>
        where TItem : ColumnItem, IColumnItem, IColumnKey
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
        { return this.Where(w => scope.ToScopeKey().Equals(w) && w is IColumnItem).Select(s => s as IColumnItem); }


        /// <summary>
        /// Loads the Default collection of Scripting Schema Column Items.
        /// </summary>
        /// <remarks>This is loaded from the Data Objects column information.</remarks>
        public void Load()
        {
            Clear();

            Import(ScopeType.ModelAttribute, () => new DomainData.Attribute.DomainAttributeItem());
            Import(ScopeType.ModelAttributeProperty, () => new DomainData.Attribute.DomainAttributePropertyItem());
            //TODO: Add rest of supported scopes

            void Import(ScopeType scope, Func<BindingTableRow> constructor)
            {
                this.AddRange(constructor()
                .ColumnDefinitions()
                .Select(s => new ColumnItem(ScopeType.ModelAttribute, s))
                .Cast<TItem>());
            }
        }
    }

    /// <summary>
    /// Default List/Collection of Scripting Schema Column Items.
    /// </summary>
    public class ColumnCollection : ColumnCollection<ColumnItem>
    {

    }
}
