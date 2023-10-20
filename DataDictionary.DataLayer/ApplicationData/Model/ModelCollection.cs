using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData.Model
{
    /// <summary>
    /// Generic Base class for Models
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ModelCollection<TItem> : BindingTable<TItem>, IReadData, IReadData<IModelKey>, IWriteData, IValidateList<ModelItem>
        where TItem : ModelItem, new()
    {         /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelIdentifier)
        { return LoadCommand(connection, (modelIdentifier.ModelId, null, true)); }

        /// <summary>
        /// Get all the Models from the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public Command LoadCommand(IConnection connection)
        { return LoadCommand(connection, (null, null, true)); }

        Command LoadCommand(IConnection connection, (Guid? modelId, string? modelTitle, bool? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetModel]";

            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@ModelTitle", parameters.modelTitle);
            command.AddParameter("@Obsolete", parameters.obsolete);

            return command;
        }

        /// <summary>
        /// Saves the Model(s) to the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public Command SaveCommand(IConnection connection)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModel]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeModel]", this);

            return command;
        }

        /// <summary>
        /// Saves a single Model to the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Command SaveCommand(IConnection connection, ModelItem item)
        {
            using (BindingTable<ModelItem> source = new BindingTable<ModelItem>() { item })
            {
                Command command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[App_DataDictionary].[procSetModel]";
                command.AddParameter("@ModelId", item.ModelId);
                command.AddParameter("@Data", "[App_DataDictionary].[typeModel]", source);

                return command;
            }
        }

        /// <summary>
        /// Removes the Model from the Database.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Command DeleteCommand(IConnection connection, IModelKey parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procDeleteModel]";
            command.AddParameter("@ModelId", parameters.ModelId);

            return command;
        }

        /// <inheritdoc/>
        public IReadOnlyList<ModelItem> Validate()
        {
            List<ModelItem> result = new List<ModelItem>();

            foreach (ModelItem item in this)
            {
                item.ClearRowErrors();
                if (!item.Validate())
                { result.Add(item); }
            }

            return result;
        }
    }

    /// <summary>
    /// Default List/Collection of Models
    /// </summary>
    public class ModelCollection: ModelCollection<ModelItem>
    { }
}
