using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ModelData.Entity
{
    /// <summary>
    /// Generic Base class for Model Entity
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ModelEntityCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IModelEntityKey>, IReadData<IDomainEntityKey>,
        IWriteData<IModelKey>, IWriteData<IModelEntityKey>, IWriteData<IDomainEntityKey>,
        IRemoveItem<IModelEntityKey>, IRemoveItem<IDomainEntityKey>
        where TItem : BindingTableRow, IModelEntityKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key)
        { return LoadCommand(connection, (key.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelEntityKey key)
        { return LoadCommand(connection, (null, key.EntityId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IDomainEntityKey key)
        { return LoadCommand(connection, (null, key.EntityId, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? EntityId, Guid? subjectId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetModelEntity]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);
            command.AddParameter("@SubjectAreaId", parameters.subjectId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey key)
        { return SaveCommand(connection, (key.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelEntityKey key)
        { return SaveCommand(connection, (null, key.EntityId)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IDomainEntityKey key)
        { return SaveCommand(connection, (null, key.EntityId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? EntityId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModelEntity]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);

            IEnumerable<TItem> data = this.Where(w => parameters.EntityId is null || w.EntityId == parameters.EntityId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeModelEntity]", data);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IModelEntityKey modelEntityItem)
        {
            ModelEntityKey key = new ModelEntityKey(modelEntityItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }

        /// <inheritdoc/>
        public virtual void Remove(IDomainEntityKey domainEntityItem)
        {
            DomainEntityKey key = new DomainEntityKey(domainEntityItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Model Subject Area
    /// </summary>
    public class ModelEntityCollection : ModelEntityCollection<ModelEntityItem>
    { }
}
