using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData.Scope
{
    /// <summary>
    /// Generic Base class for Scope Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    [Obsolete("Not gotten from the Database", true)]
    public abstract class ScopeCollection<TItem> : BindingTable<TItem>, IReadData, IWriteData
        where TItem : ScopeItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationScope]";
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationScope]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationScope]", this);
            return command;
        }
    }

    /// <summary>
    /// Default List/Collection of Scope Items.
    /// </summary>
    [Obsolete("Not gotten from the Database", true)]
    public class ScopeCollection : ScopeCollection<ScopeItem>
    { }
}
