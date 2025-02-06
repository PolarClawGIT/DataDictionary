using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Generic Base class for Security Role Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class RoleCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IRoleKey>,
        IWriteData, IWriteData<IRoleKey>,
        IRemoveItem<IRoleKey>
        where TItem : BindingTableRow, IRoleItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, roleId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoleKey key)
        { return LoadCommand(connection, roleId: key.RoleId); }

        /// <inheritdoc/>
        Command IReadData<IRoleKey>.LoadCommand(IConnection connection, IRoleKey key, ITemporalKey asOfUtcDate)
        { throw new NotSupportedException(); }

        Command LoadCommand(IConnection connection, Guid? roleId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Role.GetProcedure;
            command.AddParameter(Role.RoleId, roleId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, roleId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IRoleKey key)
        { return SaveCommand(connection, roleId: key.RoleId); }

        Command SaveCommand(IConnection connection, Guid? roleId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Role.SetProcedure;
            command.AddParameter(Role.RoleId, roleId);

            IEnumerable<TItem> data = this.Where(w => roleId is null || w.RoleId == roleId);
            command.AddParameter(WriteData.Data, Role.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IRoleKey roleKey)
        {
            RoleKey key = new RoleKey(roleKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }


    }
}
