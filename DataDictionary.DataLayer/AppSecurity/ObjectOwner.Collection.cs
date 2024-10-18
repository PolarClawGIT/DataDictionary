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
    /// Generic Base class for Security ObjectOwner Items
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ObjectOwnerCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IPrincipalKey>, IReadData<IObjectKey>,
        IWriteData, IWriteData<IPrincipalKey>, IWriteData<IObjectKey>,
        IRemoveItem<IPrincipalKey>, IRemoveItem<IObjectKey>
        where TItem : BindingTableRow, IObjectOwnerItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IPrincipalKey key)
        { return LoadCommand(connection, (key.PrincipalId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IObjectKey key)
        { return LoadCommand(connection, (null, key.ObjectId)); }

        Command LoadCommand(IConnection connection, (Guid? PrincipalId, Guid? objectId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procGetObjectOwner]";
            command.AddParameter("@PrincipalId", parameters.PrincipalId);
            command.AddParameter("@ObjectId", parameters.objectId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection, (null, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IPrincipalKey key)
        { return SaveCommand(connection, (key.PrincipalId, null)); }


        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IObjectKey key)
        { return SaveCommand(connection, (null, key.ObjectId)); }

        Command SaveCommand(IConnection connection, (Guid? PrincipalId, Guid? objectId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[AppSecurity].[procSetObjectOwner]";
            command.AddParameter("@PrincipalId", parameters.PrincipalId);
            command.AddParameter("@ObjectId", parameters.objectId);

            IEnumerable<TItem> data = this.Where(w => (parameters.PrincipalId is null || w.PrincipalId == parameters.PrincipalId) && (parameters.objectId is null || w.ObjectId == parameters.objectId));
            command.AddParameter("@Data", "[AppSecurity].[typeObjectOwner]", data);
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
        public void Remove(IObjectKey objectKey)
        {
            ObjectKey key = new ObjectKey(objectKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
