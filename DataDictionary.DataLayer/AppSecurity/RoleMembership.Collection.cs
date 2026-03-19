using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Generic Base class for Security Membership Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class RoleMembershipCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IPrincipalKey>, IReadData<IRoleKey>,
        IWriteData, IWriteData<IPrincipalKey>, IWriteData<IRoleKey>,
        IRemoveItem<IPrincipalKey>, IRemoveItem<IRoleKey>
        where TItem : BindingTableRow, IRoleMembershipItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, principalId: null, roleId: null); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPrincipalKey key)
        { return LoadCommand(connection, principalId: key.PrincipalId); }

        /// <inheritdoc/>
        Command IReadData<IPrincipalKey>.LoadCommand(IConnection connection, IPrincipalKey key, ITemporalKey asOfUtcDate)
        { throw new NotSupportedException(); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoleKey key)
        { return LoadCommand(connection, roleId: key.RoleId); }

        /// <inheritdoc/>
        Command IReadData<IRoleKey>.LoadCommand(IConnection connection, IRoleKey key, ITemporalKey asOfUtcDate)
        { throw new NotSupportedException(); }

        Command LoadCommand(IConnection connection, Guid? principalId = null, Guid? roleId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = RoleMembership.GetProcedure;
            command.AddParameter(Principal.Identifier, principalId);
            command.AddParameter(Role.Identifier, roleId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, principalId: null, roleId: null); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IPrincipalKey key)
        { return SaveCommand(connection, principalId: key.PrincipalId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IRoleKey key)
        { return SaveCommand(connection, roleId: key.RoleId); }

        Command SaveCommand(IConnection connection, Guid? principalId = null, Guid? roleId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = RoleMembership.SetProcedure;
            command.AddParameter(Principal.Identifier, principalId);
            command.AddParameter(Role.Identifier, roleId);

            IEnumerable<TItem> data = this.Where(w =>
                (principalId is null || w.PrincipalId == principalId)
                && (roleId is null || w.RoleId == roleId)
            );
            command.AddParameter(WriteData.Data, RoleMembership.TableType, data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IPrincipalKey principalKey)
        {
            PrincipalKey key = new PrincipalKey(principalKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
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
