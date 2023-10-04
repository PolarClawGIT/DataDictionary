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
    /// Generic Base class for Database Extended Property Items.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class DbExtendedPropertyCollection<TItem> : BindingTable<TItem>, IReadData<IModelKey>, IWriteData<IModelKey>
        where TItem : DbExtendedPropertyItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelId)
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

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDatabaseExtendedProperty]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDatabaseExtendedProperty]", this);
            return command;
        }
    }

    /// <summary>
    /// List of Database Extended Property Items.
    /// </summary>
    public class DbExtendedPropertyCollection : DbExtendedPropertyCollection<DbExtendedPropertyItem>
    { }
}
