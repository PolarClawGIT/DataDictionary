using DataDictionary.DataLayer.ModelData;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ScriptingData
{
    /// <summary>
    /// Generic Base class for Scripting Template
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public abstract class ScriptingTemplateCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IScriptingTemplateKey>,
        IWriteData<IModelKey>, IWriteData<IScriptingTemplateKey>,
        IDeleteData<IModelKey>, IDeleteData<IScriptingTemplateKey>,
        IRemoveItem<IScriptingTemplateKey>
        where TItem : BindingTableRow, IScriptingTemplateItem, new()
    {

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, (modelKey.ModelId, null)); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IScriptingTemplateKey templateKey)
        { return LoadCommand(connection, (null, templateKey.TemplateId)); }

        private Command LoadCommand(IConnection connection, (Guid? modelId, Guid? templateId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetScriptingTemplate]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@TemplateId", parameters.templateId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, (modelKey.ModelId, null)); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IScriptingTemplateKey templateKey)
        { return SaveCommand(connection, (null, templateKey.TemplateId)); }

        private Command SaveCommand(IConnection connection, (Guid? modelId, Guid? templateId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingTemplate]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@TemplateId", parameters.templateId);

            IEnumerable<TItem> data = this.Where(w => parameters.templateId is null || w.TemplateId == parameters.templateId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeScriptingTemplate]", data);

            return command;
        }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IScriptingTemplateKey templateKey)
        { return DeleteCommand(connection, (null, templateKey.TemplateId)); }

        /// <inheritdoc/>
        public Command DeleteCommand(IConnection connection, IModelKey modelKey)
        { return DeleteCommand(connection, (modelKey.ModelId, null)); }

        private Command DeleteCommand(IConnection connection, (Guid? modelId, Guid? templateId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetScriptingTemplate]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@TemplateId", parameters.templateId);
            return command;
        }

        /// <inheritdoc/>
        public virtual void Remove(IScriptingTemplateKey templateKey)
        {
            ScriptingTemplateKey key = new ScriptingTemplateKey(templateKey);

            foreach (TItem item in this.Where(w => key.Equals(w)).ToList())
            { base.Remove(item); }
        }
    }
}
