// Ignore Spelling: Utc

using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.DataLayer.AppScript;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface component for the Scripting Template Element
    /// </summary>
    public interface ITemplateElementData : IBindingData<TemplateElementValue>
    {
        /// <summary>
        /// Given the Element, return the Path of that element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        PathIndex GetPath(ITemplateElementIndex element);
    }

    class TemplateElementData : TemplateElementCollection<TemplateElementValue>, ITemplateElementData,
        ILoadData<ITemplateIndex>, ISaveData<ITemplateIndex>,
        ILoadData<IModelIndex>, ISaveData<IModelIndex>
    {
        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateLoad(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (IModelKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ITemplateIndex dataKey, ITemporalIndex asOfUtcDate)
        { return factory.CreateLoad(this, (ITemplateKey)dataKey, asOfUtcDate).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ITemplateIndex dataKey)
        { return factory.CreateSave(this, (ITemplateKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelIndex dataKey)
        { return factory.CreateSave(this, (IModelKey)dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Delete(ITemplateIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Scripting Template Element", DoWork = () => { Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Scripting Template Element", DoWork = () => { Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelIndex dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public void Remove(ITemplateIndex dataKey)
        { base.Remove(dataKey); }

        /// <inheritdoc/>
        /// <remarks>ScriptingTemplateElement</remarks>
        public void Remove(IModelIndex dataKey)
        { Clear(); }

        /// <inheritdoc/>
        public PathIndex GetPath(ITemplateElementIndex element)
        {   //TODO: Unit Test
            TemplateElementIndex key = new TemplateElementIndex(element);
            PathIndex result = new PathIndex();

            if (this.FirstOrDefault(w => key.Equals(w)) is TemplateElementValue value)
            {
                if (value.ParentElementId is null)
                {
                    ITemplateElementIndex parent = new TemplateElementIndexParent(value);
                    result.Merge(GetPath(parent));
                }
                else
                { result.Merge(new PathIndex(value.ElementName)); }
            }

            return result;
        }
    }
}
