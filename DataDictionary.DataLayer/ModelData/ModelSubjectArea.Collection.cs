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
    /// Generic Base class for Model Subject Area
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ModelSubjectAreaCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IModelSubjectAreaKey>,
        IWriteData<IModelKey>, IWriteData<IModelSubjectAreaKey>,
        IDeleteData<IModelKey>, IDeleteData<IModelSubjectAreaKey>,
        IRemoveItem<IModelSubjectAreaKey>
        where TItem : BindingTableRow, IModelSubjectAreaItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key)
        { return LoadCommand(connection, (key.ModelId, null, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelSubjectAreaKey key)
        { return LoadCommand(connection, (null, key.SubjectAreaId, null)); }


        Command LoadCommand(IConnection connection, (Guid? modelId, Guid? subjectAreaId, string? subjectAreaTitle) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetModelSubjectArea]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SubjectAreaId", parameters.subjectAreaId);
            command.AddParameter("@SubjectAreaTitle", parameters.subjectAreaTitle);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey key)
        { return SaveCommand(connection, (key.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelSubjectAreaKey key)
        { return SaveCommand(connection, (null, key.SubjectAreaId)); }

        Command SaveCommand(IConnection connection, (Guid? modelId, Guid? subjectAreaId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModelSubjectArea]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SubjectAreaId", parameters.subjectAreaId);

            IEnumerable<TItem> data = this.Where(w => parameters.subjectAreaId is null || w.SubjectAreaId == parameters.subjectAreaId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeModelSubjectArea]", this);
            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelKey key)
        { return DeleteCommand(connection, (key.ModelId, null)); }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelSubjectAreaKey key)
        { return DeleteCommand(connection, (null, key.SubjectAreaId)); }

        Command DeleteCommand(IConnection connection, (Guid? modelId, Guid? subjectAreaId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetModelSubjectArea]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@SubjectAreaId", parameters.subjectAreaId);

            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IModelSubjectAreaKey subjectAreaItem)
        {
            ModelSubjectAreaKey key = new ModelSubjectAreaKey(subjectAreaItem);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }

    /// <summary>
    /// Default List/Collection of Model Subject Area
    /// </summary>
    public class ModelSubjectAreaCollection : ModelSubjectAreaCollection<ModelSubjectAreaItem>
    { }
}
