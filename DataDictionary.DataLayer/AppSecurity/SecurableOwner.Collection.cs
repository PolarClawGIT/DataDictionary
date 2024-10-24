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
    /// Generic Base class for Securable (Security Object) Owner Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class SecurableOwnerCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IPrincipalKey>, IReadData<ISecurableKey>,
        IWriteData, IWriteData<IPrincipalKey>, IWriteData<ISecurableKey>,
        IRemoveItem<IPrincipalKey>, IRemoveItem<ISecurableKey>
        where TItem : BindingTableRow, ISecurableOwnerItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPrincipalKey key)
        { return LoadCommand(connection, (key.PrincipalId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, ISecurableKey key)
        { return LoadCommand(connection, (null, key.SecurableId)); }

        Command LoadCommand(IConnection connection, (Guid? PrincipalId, Guid? SecurableId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetSecurableOwner]";
            command.AddParameter("@PrincipalId", parameters.PrincipalId);
            command.AddParameter("@SecurableId", parameters.SecurableId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IPrincipalKey key)
        { return SaveCommand(connection, (key.PrincipalId, null)); }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, ISecurableKey key)
        { return SaveCommand(connection, (null, key.SecurableId)); }

        Command SaveCommand(IConnection connection, (Guid? PrincipalId, Guid? SecurableId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetSecurableOwner]";
            command.AddParameter("@PrincipalId", parameters.PrincipalId);
            command.AddParameter("@SecurableId", parameters.SecurableId);

            IEnumerable<TItem> data = this.Where(w => (parameters.PrincipalId is null || w.PrincipalId == parameters.PrincipalId) && (parameters.SecurableId is null || w.SecurableId == parameters.SecurableId));
            command.AddParameter("@Data", "[AppSecurity].[typeSecurableOwner]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IPrincipalKey PrincipalKey)
        {
            PrincipalKey key = new PrincipalKey(PrincipalKey);

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
