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
    public abstract class SecurityMembershipCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<ISecurityPrincipleKey>, IReadData<ISecurityRoleKey>,
        IWriteData, IWriteData<ISecurityPrincipleKey>, IWriteData<ISecurityRoleKey>,
        IRemoveItem<ISecurityPrincipleKey>, IRemoveItem<ISecurityRoleKey>
        where TItem : BindingTableRow, ISecurityMembershipItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurityPrincipleKey key)
        { return LoadCommand(connection, (key.PrincipleId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurityRoleKey key)
        { return LoadCommand(connection, (null, key.RoleId)); }


        Command LoadCommand(IConnection connection, (Guid? principleId, Guid? roleId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetSecurityMembership]";
            command.AddParameter("@PrincipleId", parameters.principleId);
            command.AddParameter("@RoleId", parameters.roleId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISecurityPrincipleKey key)
        { return SaveCommand(connection, (key.PrincipleId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISecurityRoleKey key)
        { return SaveCommand(connection, (null, key.RoleId)); }

        Command SaveCommand(IConnection connection, (Guid? principleId, Guid? roleId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetSecurityMembership]";
            command.AddParameter("@PrincipleId", parameters.principleId);
            command.AddParameter("@RoleId", parameters.roleId);

            IEnumerable<TItem> data = this.Where(w =>
                (parameters.principleId is null || w.PrincipleId == parameters.principleId)
                && (parameters.roleId is null || w.RoleId == parameters.roleId)
            );
            command.AddParameter("@Data", "[AppSecurity].[typeSecurityMembership]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(ISecurityPrincipleKey principleKey)
        {
            SecurityPrincipleKey key = new SecurityPrincipleKey(principleKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
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
