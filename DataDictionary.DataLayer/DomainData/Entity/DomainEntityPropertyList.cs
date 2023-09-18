using DataDictionary.DataLayer.ApplicationData.Model;
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
    /// List for Domain Entity Properties
    /// </summary>
    public class DomainEntityPropertyList : BindingTable<DomainEntityPropertyItem>, IReadData<IModelKey>, IWriteData<IModelKey>
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null)); }

        /// <inheritdoc/>
        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? EntityId, string? propertyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainEntityProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@EntityId", parameters.EntityId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainEntityProperty]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainEntityProperty]", this);
            return command;
        }
    }
}