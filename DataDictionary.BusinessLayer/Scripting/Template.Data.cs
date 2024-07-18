using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ScriptingData.Template;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Template
    /// </summary>
    public interface ITemplateData : IBindingData<TemplateValue>
    { }

    class TemplateData : ScriptingTemplateCollection<TemplateValue>, ITemplateData, INamedScopeSource,
        ILoadData<IScriptingTemplateKey>, ISaveData<IScriptingTemplateKey>,
        ILoadData<IModelKey>, ISaveData<IModelKey>
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

        public TemplateData() : base()
        { }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { return Delete((IScriptingTemplateKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(IScriptingTemplateKey dataKey)
        { return new WorkItem() { WorkName = "Remove Template", DoWork = () => { Remove((IScriptingTemplateKey)dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Template", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public void Import(DataSet source)
        {
            if (source.Tables.Contains(this.BindingName)
                && source.Tables[this.BindingName] is DataTable transformTable)
            { this.Load(transformTable.CreateDataReader()); }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<DataTable> Export()
        { return this.ToDataTable().ToList(); }


        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IEnumerable<NamedScopePair> GetNamedScopes()
        {
            return this.Select(s => new NamedScopePair(GetValue(s)));

            NamedScopeValue GetValue(TemplateValue source)
            {
                NamedScopeValue result = new NamedScopeValue(source);
                source.PropertyChanged += Source_PropertyChanged;

                return result;

                void Source_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName is
                        nameof(source.TemplateTitle))
                    { result.TitleChanged(); }
                }
            }
        }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IScriptingTemplateKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IScriptingTemplateKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Template</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }
    }
}
