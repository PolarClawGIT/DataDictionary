using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Entity
{
    /// <summary>
    /// Generic Base class for Domain Entity's
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DomainEntityCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IDomainEntityKey>,
        IWriteData<IModelKey>, IWriteData<IDomainEntityKey>,
        IDeleteData<IDomainEntityKey>, IDeleteData<IModelKey>,
        IRemoveItem<IDomainEntityKey>
        where TItem : BindingTableRow, IDomainEntityItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDomainEntityKey entityKey)
        { return LoadCommand(connection, (null, entityKey.EntityId)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? EntityId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainEntity]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        { return SaveCommand(connection, (modelId.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDomainEntityKey entityKey)
        { return SaveCommand(connection, (null, entityKey.EntityId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? EntityId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainEntity]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);

            IEnumerable<TItem> data = this.Where(w => parameters.EntityId is null || w.EntityId == parameters.EntityId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainEntity]", data);
            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IDomainEntityKey parameters)
        { return DeleteCommand(connection, (null, parameters.EntityId)); }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelKey parameters)
        { return DeleteCommand(connection, (null, parameters.ModelId)); }

        Command DeleteCommand(IConnection connection, (Guid? modelId, Guid? EntityId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procDeleteDomainEntity]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IDomainEntityKey entityItem)
        {
            DomainEntityKey key = new DomainEntityKey(entityItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
