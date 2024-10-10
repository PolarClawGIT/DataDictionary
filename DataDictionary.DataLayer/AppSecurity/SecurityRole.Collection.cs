using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Generic Base class for Security Role Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class SecurityRoleCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISecurityRoleKey>,
        IWriteData, IWriteData<ISecurityRoleKey>,
        IRemoveItem<ISecurityRoleKey>
        where TItem : BindingTableRow, ISecurityRoleItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurityRoleKey key)
        { return LoadCommand(connection, (key.RoleId, null)); }

        Command LoadCommand(IConnection connection, (Guid? roleId, String? nothing) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetSecurityRole]";
            command.AddParameter("@RoleId", parameters.roleId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISecurityRoleKey key)
        { return SaveCommand(connection, (key.RoleId, null)); }

        Command SaveCommand(IConnection connection, (Guid? roleId, String? nothing) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetSecurityRole]";
            command.AddParameter("@RoleId", parameters.roleId);

            IEnumerable<TItem> data = this.Where(w => parameters.roleId is null || w.RoleId == parameters.roleId);
            command.AddParameter("@Data", "[AppSecurity].[typeSecurityRole]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISecurityRoleKey roleKey)
        {
            SecurityRoleKey key = new SecurityRoleKey(roleKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
