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
    /// Interface component for the Scripting Engine Node Property
    /// </summary>
    public interface INodePropertyData : IEnumerable<NodePropertyValue>
    {
        /// <summary>
        /// Default Load of the class.
        /// </summary>
        void Load();
    }

    /// <summary>
    /// Implementation component for the Scripting Engine Node Property
    /// </summary>
    public class NodePropertyData : Collection<NodePropertyValue>, INodePropertyData
    {
        /// <summary>
        /// Returns a list of Columns for a given scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IEnumerable<INodePropertyValue> GetColumns(ScopeType scope)
        { return this.Where(w => scope.Equals(w) && w is INodePropertyValue).Select(s => s as INodePropertyValue); }

        /// <summary>
        /// Default Load of the class.
        /// </summary>
        public void Load()
        {
            Load(Domain.AttributeValue.GetXColumns());
            Load(Domain.AttributePropertyValue.GetXColumns());
            Load(Domain.AttributeAliasValue.GetXColumns());
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
                NodePropertyValue newItem = new()
                {
                    PropertyScope = source.Scope,
                    PropertyName = item.ColumnName,
                    AllowDBNull = item.AllowDBNull,
                    DataType = item.DataType,
                };

                this.Add(newItem);
            }
        }

        /// <summary>
        /// Loads the collection using a list of Column Items.
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>Because a Collection does not have AddRange</remarks>
        public void Load(IEnumerable<NodePropertyValue> source)
        { source.ToList().ForEach(i => Add(i)); }

    }
}
