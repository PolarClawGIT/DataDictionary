// Ignore Spelling: Utc

using DataDictionary.DataLayer.AppModel;
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
    public abstract class ScriptingAttributeCollection<TItem> : BindingTable<TItem>,
        IReadData<IModelKey>, IReadData<IScriptingTemplateKey>,
        IWriteData<IModelKey>, IWriteData<IScriptingTemplateKey>,
        IRemoveItem<IScriptingTemplateKey>
        where TItem : BindingTableRow, IScriptingAttributeItem, new()
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
            command.CommandText = ScriptingAttribute.GetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(ScriptingTemplate.TemplateId, templateId);
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
            command.CommandText = ScriptingAttribute.SetProcedure;
            command.AddParameter(Model.ModelId, modelId);
            command.AddParameter(ScriptingTemplate.TemplateId, templateId);

            IEnumerable<TItem> data = this.Where(w => templateId is null || w.TemplateId == templateId);
            command.AddParameter(WriteData.Data, ScriptingAttribute.TableType, data);

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
