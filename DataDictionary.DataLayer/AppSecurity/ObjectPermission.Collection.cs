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
    /// Generic Base class for Security ObjectPermission Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ObjectPermissionCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IRoleKey>, IReadData<IObjectKey>,
        IWriteData, IWriteData<IRoleKey>, IWriteData<IObjectKey>,
        IRemoveItem<IRoleKey>, IRemoveItem<IObjectKey>
        where TItem : BindingTableRow, IObjectPermissionItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoleKey key)
        { return LoadCommand(connection, (key.RoleId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IObjectKey key)
        { return LoadCommand(connection, (null, key.ObjectId)); }

        Command LoadCommand(IConnection connection, (Guid? roleId, Guid? objectId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetObjectPermission]";
            command.AddParameter("@RoleId", parameters.roleId);
            command.AddParameter("@ObjectId", parameters.objectId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IRoleKey key)
        { return SaveCommand(connection, (key.RoleId, null)); }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IObjectKey key)
        { return SaveCommand(connection, (null, key.ObjectId)); }

        Command SaveCommand(IConnection connection, (Guid? roleId, Guid? objectId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetObjectPermission]";
            command.AddParameter("@RoleId", parameters.roleId);
            command.AddParameter("@ObjectId", parameters.objectId);

            IEnumerable<TItem> data = this.Where(w => (parameters.roleId is null || w.RoleId == parameters.roleId) && (parameters.objectId is null || w.ObjectId == parameters.objectId));
            command.AddParameter("@Data", "[AppSecurity].[typeObjectPermission]", data);
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
        public void Remove(IObjectKey objectKey)
        {
            ObjectKey key = new ObjectKey(objectKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
