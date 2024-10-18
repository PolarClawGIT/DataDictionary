using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Generic Base class for Authorization Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class AuthorizationCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IPrincipalKeyName>
        where TItem : BindingTableRow, IAuthorizationItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, String.Empty); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPrincipalKeyName key)
        { return LoadCommand(connection, key.PrincipalLogin); }

        Command LoadCommand(IConnection connection, String? principalLogin)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetAuthorization]";
            command.AddParameter("@PrincipalLogin", principalLogin);
            return command;
        }
    }
}
