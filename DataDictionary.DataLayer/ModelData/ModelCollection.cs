using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ModelData
{
    /// <summary>
    /// Generic Base class for Models
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ModelCollection<TItem> : BindingTable<TItem>,
        IReadData, IReadData<IModelKey>,
        IWriteData, IWriteData<IModelKey>,
        IDeleteData<IModelKey>, IValidateList<ModelItem>,
        IRemoveItem<IModelKey>
        where TItem : ModelItem, new()
    {         
        /// <inheritdoc/>
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

            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection)
        { return SaveCommand(connection); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey key)
        { return SaveCommand(connection, key.ModelId); }

        Command SaveCommand(IConnection connection, Guid? modelId)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModel]";
            command.AddParameter("@ModelId", modelId);

            IEnumerable<TItem> data = this.Where(w => modelId is null || w.ModelId == modelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeModel]", data);

            return command;
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
        public virtual void Remove(IModelKey modelItem)
        {
            ModelKey key = new ModelKey(modelItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
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
    public class ModelCollection : ModelCollection<ModelItem>
    { }
}
