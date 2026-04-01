// Ignore Spelling: Utc

using DataDictionary.DataLayer.AppModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Generic Base class for Scripting Template
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    [Obsolete("replace", true)]
    public abstract class ScriptingNodeCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IScriptingTemplateKey>,
        IWriteData<IModelKey>, IWriteData<IScriptingTemplateKey>,
        IRemoveItem<IScriptingTemplateKey>
        where TItem : BindingTableRow, IScriptingNodeItem, new()
    {
        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey modelKey)
        { return LoadCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IModelKey key, ITemporalKey asOfUtcDate)
        { throw new NotImplementedException(); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IScriptingTemplateKey templateKey)
        { return LoadCommand(connection, templateId: templateKey.TemplateId); }

        /// <inheritdoc/>
        public Command LoadCommand(IConnection connection, IScriptingTemplateKey key, ITemporalKey asOfUtcDate)
        { throw new NotImplementedException(); }

        private Command LoadCommand(IConnection connection, Guid? modelId = null, Guid? templateId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = ScriptingNode.GetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Template.Identifier, templateId);
            return command;
        }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IModelKey modelKey)
        { return SaveCommand(connection, modelId: modelKey.ModelId); }

        /// <inheritdoc/>
        public Command SaveCommand(IConnection connection, IScriptingTemplateKey templateKey)
        { return SaveCommand(connection, templateId: templateKey.TemplateId); }

        private Command SaveCommand(IConnection connection, Guid? modelId = null, Guid? templateId = null)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = ScriptingNode.SetProcedure;
            command.AddParameter(Model.Identifier, modelId);
            command.AddParameter(Template.Identifier, templateId);

            IEnumerable<TItem> data = this.Where(w => templateId is null || w.TemplateId == templateId);
            command.AddParameter(WriteData.Data, ScriptingNode.TableType, data);

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
