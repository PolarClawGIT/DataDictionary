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
        IReadData, IReadData<IPrincipleKey>, IReadData<IRoleKey>,
        IWriteData, IWriteData<IPrincipleKey>, IWriteData<IRoleKey>,
        IRemoveItem<IPrincipleKey>, IRemoveItem<IRoleKey>
        where TItem : BindingTableRow, IRoleMembershipItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPrincipleKey key)
        { return LoadCommand(connection, (key.PrincipleId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IRoleKey key)
        { return LoadCommand(connection, (null, key.RoleId)); }


        Command LoadCommand(IConnection connection, (Guid? principleId, Guid? roleId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetRoleMembership]";
            command.AddParameter("@PrincipleId", parameters.principleId);
            command.AddParameter("@RoleId", parameters.roleId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IPrincipleKey key)
        { return SaveCommand(connection, (key.PrincipleId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IRoleKey key)
        { return SaveCommand(connection, (null, key.RoleId)); }

        Command SaveCommand(IConnection connection, (Guid? principleId, Guid? roleId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetRoleMembership]";
            command.AddParameter("@PrincipleId", parameters.principleId);
            command.AddParameter("@RoleId", parameters.roleId);

            IEnumerable<TItem> data = this.Where(w =>
                (parameters.principleId is null || w.PrincipleId == parameters.principleId)
                && (parameters.roleId is null || w.RoleId == parameters.roleId)
            );
            command.AddParameter("@Data", "[AppSecurity].[typeRoleMembership]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IPrincipleKey principleKey)
        {
            PrincipleKey key = new PrincipleKey(principleKey);

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
