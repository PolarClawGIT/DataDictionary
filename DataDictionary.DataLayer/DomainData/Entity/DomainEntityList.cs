﻿using DataDictionary.DataLayer.ApplicationData.Model;
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
    /// List of Domain Entity's
    /// </summary>
    public class DomainEntityList : BindingTable<DomainEntityItem>, IReadData<IModelKey>, IWriteData<IModelKey>, IDeleteData<IDomainEntityKey>, IDeleteData<IModelKey>
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? EntityId, string? EntityTitle, bool? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainEntity]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);
            command.AddParameter("@EntityTitle", parameters.EntityTitle);
            command.AddParameter("@Obsolete", parameters.obsolete);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainEntity]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainEntity]", this);
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
    }
}
