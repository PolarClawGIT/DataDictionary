using DataDictionary.DataLayer.ApplicationData.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// List of DbExtendedProperty Items.
    /// </summary>
    public class DbExtendedPropertyList : BindingTable<DbExtendedPropertyItem>, IReadData<ModelKey>, IWriteData<ModelKey>
    {
        /// <summary>
        /// Loads the MS SQL Extended Properties to the Application database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public Command LoadCommand(IConnection connection, ModelKey modelId)
        { return LoadCommand(connection, (modelId.ModelId, null, null)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? propertyId, string? catalogName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDatabaseExtendedProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@PropertyId", parameters.propertyId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            return command;
        }

        /// <summary>
        /// Saves the MS SQL Extended Properties to the Application database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="modelId"></param>
        /// <returns></returns>
        public Command SaveCommand(IConnection connection, ModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseExtendedProperty]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseExtendedProperty]", this);
            return command;
        }
    }
}
