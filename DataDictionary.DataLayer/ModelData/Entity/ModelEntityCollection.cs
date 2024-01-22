﻿using System;
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
        IReadData<IModelKey>, IReadData<IModelEntityKey>,
        IWriteData<IModelKey>, IWriteData<IModelEntityKey>,
        IDeleteData<IModelKey>, IDeleteData<IModelEntityKey>,
        IRemoveData<IModelEntityKey>
        where TItem : BindingTableRow, IModelEntityKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key)
        { return LoadCommand(connection, (key.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelEntityKey key)
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

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? EntityId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModelEntity]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeModelEntity]", this);
            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelKey key)
        { return DeleteCommand(connection, (key.ModelId, null)); }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelEntityKey key)
        { return DeleteCommand(connection, (null, key.EntityId)); }

        Command DeleteCommand(IConnection connection, (Guid? modelId, Guid? EntityId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModelSubjectArea]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);

            return command;
        }

        /// <inheritdoc/>
        public void Remove(IModelEntityKey modelEntityItem)
        {
            ModelEntityKey key = new ModelEntityKey(modelEntityItem);

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