// Ignore Spelling: Securable Utc

using System.Data;
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
        { return LoadCommand(connection, roleId: null, securableId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoleKey key)
        { return LoadCommand(connection, roleId: key.RoleId); }

        /// <inheritdoc/>
        Command IReadData<IRoleKey>.LoadCommand(IConnection connection, IRoleKey key, DateTime asOfUtcDate)
        { throw new NotSupportedException(); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurableKey key)
        { return LoadCommand(connection, securableId: key.SecurableId); }

        /// <inheritdoc/>
        Command IReadData<ISecurableKey>.LoadCommand(IConnection connection, ISecurableKey key, DateTime asOfUtcDate)
        { throw new NotSupportedException(); }

        Command LoadCommand(IConnection connection, Guid? roleId = null, Guid? securableId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SecurablePermission.GetProcedure;
            command.AddParameter(Role.RoleId, roleId);
            command.AddParameter(Securable.SecurableId, securableId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, roleId: null, securableId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IRoleKey key)
        { return SaveCommand(connection, roleId : key.RoleId); }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISecurableKey key)
        { return SaveCommand(connection, securableId: key.SecurableId); }

        Command SaveCommand(IConnection connection, Guid? roleId = null, Guid? securableId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = SecurablePermission.SetProcedure;
            command.AddParameter(Role.RoleId, roleId);
            command.AddParameter(Securable.SecurableId, securableId);

            IEnumerable<TItem> data = this.Where(w => (roleId is null || w.RoleId == roleId) && (securableId is null || w.SecurableId == securableId));
            command.AddParameter(WriteData.Data, SecurablePermission.TableType, data);
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
