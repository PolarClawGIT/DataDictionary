// Ignore Spelling: Securable

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
    /// Generic Base class for Securable (Security Object) Permission Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class SecurablePermissionCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IRoleKey>, IReadData<ISecurableKey>,
        IWriteData, IWriteData<IRoleKey>, IWriteData<ISecurableKey>,
        IRemoveItem<IRoleKey>, IRemoveItem<ISecurableKey>
        where TItem : BindingTableRow, ISecurablePermissionItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoleKey key)
        { return LoadCommand(connection, (key.RoleId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurableKey key)
        { return LoadCommand(connection, (null, key.SecurableId)); }

        Command LoadCommand(IConnection connection, (Guid? RoleId, Guid? SecurableId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetSecurablePermission]";
            command.AddParameter("@RoleId", parameters.RoleId);
            command.AddParameter("@SecurableId", parameters.SecurableId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IRoleKey key)
        { return SaveCommand(connection, (key.RoleId, null)); }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISecurableKey key)
        { return SaveCommand(connection, (null, key.SecurableId)); }

        Command SaveCommand(IConnection connection, (Guid? RoleId, Guid? SecurableId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetObjectPermission]";
            command.AddParameter("@RoleId", parameters.RoleId);
            command.AddParameter("@SecurableId", parameters.SecurableId);

            IEnumerable<TItem> data = this.Where(w => (parameters.RoleId is null || w.RoleId == parameters.RoleId) && (parameters.SecurableId is null || w.SecurableId == parameters.SecurableId));
            command.AddParameter("@Data", "[AppSecurity].[typeSecurablePermission]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IRoleKey roleKey)
        {
            RoleKey key = new RoleKey(roleKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public void Remove(ISecurableKey objectKey)
        {
            SecurableKey key = new SecurableKey(objectKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
