using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ModelData.Attribute
{
    /// <summary>
    /// Generic Base class for Model Attribute
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ModelAttributeCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IModelAttributeKey>,
        IWriteData<IModelKey>, IWriteData<IModelAttributeKey>,
        IRemoveData<IModelAttributeKey>
        where TItem : BindingTableRow, IModelAttributeKey, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key)
        { return LoadCommand(connection, (key.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelAttributeKey key)
        { return LoadCommand(connection, (null, key.AttributeId, null)); }


        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? attributeId, Guid? subjectId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetModelAttribute]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            command.AddParameter("@SubjectAreaId", parameters.subjectId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey key)
        { return SaveCommand(connection, (key.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelAttributeKey key)
        { return SaveCommand(connection, (null, key.AttributeId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? attributeId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModelAttribute]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);

            IEnumerable<TItem> data = this.Where(w => parameters.attributeId is null || w.AttributeId == parameters.attributeId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeModelAttribute]", data);
            return command;
        }

        /// <inheritdoc/>
        public void Remove(IModelAttributeKey modelAttributeItem)
        {
            ModelAttributeKey key = new ModelAttributeKey(modelAttributeItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Model Subject Area
    /// </summary>
    public class ModelAttributeCollection : ModelAttributeCollection<ModelAttributeItem>
    { }
}
