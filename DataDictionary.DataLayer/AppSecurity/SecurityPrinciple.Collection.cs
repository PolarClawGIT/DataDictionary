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
    /// Generic Base class for Security Principle Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class SecurityPrincipleCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISecurityPrincipleKey>, IReadData<ISecurityPrincipleKeyName>,
        IWriteData, IWriteData<ISecurityPrincipleKey>,
        IRemoveItem<ISecurityPrincipleKey>
        where TItem : BindingTableRow, ISecurityPrincipleItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurityPrincipleKey key)
        { return LoadCommand(connection, (key.PrincipleId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurityPrincipleKeyName key)
        { return LoadCommand(connection, (null, key.PrincipleLogin, null)); }

        Command LoadCommand(IConnection connection, (Guid? principleId, String? principleLogin, Boolean? isCurrent) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetSecurityPrinciple]";
            command.AddParameter("@PrincipleId", parameters.principleId);
            command.AddParameter("@PrincipleLogin", parameters.principleLogin);
            command.AddParameter("@IsCurrent", parameters.isCurrent);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISecurityPrincipleKey key)
        { return SaveCommand(connection, (key.PrincipleId, null)); }

        Command SaveCommand(IConnection connection, (Guid? principleId, String? nothing) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetSecurityPrinciple]";
            command.AddParameter("@PrincipleId", parameters.principleId);

            IEnumerable<TItem> data = this.Where(w => parameters.principleId is null || w.PrincipleId == parameters.principleId);
            command.AddParameter("@Data", "[AppSecurity].[typeSecurityPrinciple]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISecurityPrincipleKey principleKey)
        {
            SecurityPrincipleKey key = new SecurityPrincipleKey(principleKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
