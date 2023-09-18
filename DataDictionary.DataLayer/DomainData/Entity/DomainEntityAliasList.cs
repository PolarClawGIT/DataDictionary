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
    /// List of Domain Entity Aliases
    /// </summary>
    public class DomainEntityAliasList : BindingTable<DomainEntityAliasItem>, IReadData<IModelKey>, IWriteData<IModelKey>
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null, null, null)); }

        /// <inheritdoc/>
        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? EntityId, string? catalogName, string? schemaName, string? objectName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainEntityAlias]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@ObjectName", parameters.objectName);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainEntityAlias]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainEntityAlias]", this);
            return command;
        }
    }
}
