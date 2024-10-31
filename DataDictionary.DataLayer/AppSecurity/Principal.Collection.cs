using DataDictionary.DataLayer.DatabaseData;
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
    /// Generic Base class for Security Principal Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class PrincipalCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IPrincipalKey>, IReadData<IPrincipalKeyName>,
        IWriteData, IWriteData<IPrincipalKey>,
        IRemoveItem<IPrincipalKey>
        where TItem : BindingTableRow, IPrincipalItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPrincipalKey key)
        { return LoadCommand(connection, (key.PrincipalId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPrincipalKeyName key)
        { return LoadCommand(connection, (null, key.PrincipalLogin, null)); }

        Command LoadCommand(IConnection connection, (Guid? principalId, String? principalLogin, Boolean? isCurrent) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetPrincipal]";
            command.AddParameter("@PrincipalId", parameters.principalId);
            command.AddParameter("@PrincipalLogin", parameters.principalLogin);
            command.AddParameter("@IsCurrent", parameters.isCurrent);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IPrincipalKey key)
        { return SaveCommand(connection, (key.PrincipalId, null)); }

        Command SaveCommand(IConnection connection, (Guid? principalId, String? nothing) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetPrincipal]";
            command.AddParameter("@PrincipalId", parameters.principalId);

            IEnumerable<TItem> data = this.Where(w => parameters.principalId is null || w.PrincipalId == parameters.principalId);
            command.AddParameter("@Data", "[AppSecurity].[typePrincipal]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IPrincipalKey principalKey)
        {
            PrincipalKey key = new PrincipalKey(principalKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
