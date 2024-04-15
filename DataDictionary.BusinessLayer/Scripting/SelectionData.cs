using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ScriptingData.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Selection
    /// </summary>
    public interface ISelectionData :
        IBindingData<SelectionItem>,
        ILoadData, ILoadData<ISelectionKey>,
        ISaveData, ISaveData<ISelectionKey>
    { }


    class SelectionData : SelectionCollection<SelectionItem>, ISelectionData, INamedScopeData
    {
        /// <summary>
        /// Reference to the containing ScriptingEngine
        /// </summary>
        public required ScriptingEngine Scripting { get; init; }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>

        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, ISelectionKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Selection</remarks>
        public IReadOnlyList<WorkItem> Build(INamedScopeDictionary target)
        {
            //TODO: Need to be reworked
            //throw new NotImplementedException();
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem()
            {
                WorkName = "Build NamedScope Scripting Selection",
                DoWork = () =>
                {
                    if (Scripting.Models.FirstOrDefault() is IModelItem model)
                    {
                        ModelKey key = new ModelKey(model);

                        foreach (SelectionItem item in this)
                        {
                            //ISelectionKey selectionKey = new SelectionKey(item);
                            //target.Remove(new NamedScopeKey(selectionKey));
                            //target.Add(new NamedScopeItem(key, item));
                        }
                    }
                }
            });

            return work;
        }
    }
}
